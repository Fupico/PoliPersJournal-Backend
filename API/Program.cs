using API.Middlewares;
using Application.Extensions;  // Application servislerini eklemek için
using Domain.Interfaces;
using Infrastructure.Extensions;  // Infrastructure servislerini eklemek için
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

LoadDotEnv();
var persistentWebRootPath = ResolveWebRootPath();

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    WebRootPath = persistentWebRootPath
});

// ✅ 1️⃣ Logging Ayarları
builder.Logging.ClearProviders();
builder.Logging.AddConsole(); // 🔥 Konsola log bas
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://localhost:9000", "https://localhost:9001", "http://localhost:9000", "http://localhost:9000", "https://test.polipersjournal.com", "http://test.polipersjournal.com", "https://polipersjournal.com", "http://polipersjournal.com", "https://polipersjournal.devrimmehmet.com", "polipersjournal.devrimmehmet.com")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});



// ✅ 2️⃣ Servisleri API'ye ekle
builder.Services.AddControllers();
builder.Services.AddApplicationServices(); // Application servisleri
builder.Services.AddInfrastructureServices(builder.Configuration); // Infrastructure servisleri
builder.Services.AddHttpContextAccessor(); // IHttpContextAccessor için

// ✅ 3️⃣ JWT Authentication Ayarları
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var jwtSecret = jwtSettings["Secret"] ?? throw new InvalidOperationException("JwtSettings:Secret is not configured.");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        ClockSkew = TimeSpan.Zero
    };
});

// ✅ 4️⃣ Swagger Ayarları
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations(); // 📌 SwaggerOperation desteği için gerekli

    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fupitech API", Version = "v1" });

    // 🔥 Swagger için JWT Yetkilendirme Tanımlama
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Token'ınızı giriniz. (Örn: Bearer {token})"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

    // XML yorumlarını aktif etmek için aşağıyı ekleyelim:
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

});
var app = builder.Build();

EnsureWebRootDirectory(app.Environment.WebRootPath);

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(app.Environment.WebRootPath),
    RequestPath = string.Empty
});
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        Console.WriteLine("🔥 Startup Exception: " + ex.Message);
        throw;
    }
});

// ✅ 5️⃣ Middleware Sırası

app.UseRouting(); // 🔹 1️⃣ Rotaları yükle
app.UseCors("AllowFrontend");
app.UseAuthentication(); // 🔹 2️⃣ Önce kimlik doğrulama (JWT)
app.UseAuthorization(); // 🔹 3️⃣ Sonra yetkilendirme

app.UseMiddleware<ExceptionMiddleware>(); // 🔹 4️⃣ Özel hata yönetimi middleware'i

if (app.Environment.IsDevelopment()) // 🔹 5️⃣ Geliştirme ortamı için Swagger
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers(); // 🔹 6️⃣ Controller'ları API'ye kaydet

app.Run();

static void LoadDotEnv()
{
    foreach (var candidate in GetEnvCandidates())
    {
        if (!File.Exists(candidate))
        {
            continue;
        }

        foreach (var rawLine in File.ReadAllLines(candidate))
        {
            var line = rawLine.Trim();
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
            {
                continue;
            }

            var separatorIndex = line.IndexOf('=');
            if (separatorIndex <= 0)
            {
                continue;
            }

            var key = line[..separatorIndex].Trim();
            var value = line[(separatorIndex + 1)..].Trim().Trim('"');

            if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable(key)))
            {
                Environment.SetEnvironmentVariable(key, value);
            }
        }

        break;
    }
}

static IEnumerable<string> GetEnvCandidates()
{
    var currentDirectory = Directory.GetCurrentDirectory();
    yield return Path.Combine(currentDirectory, ".env");

    var parentDirectory = Directory.GetParent(currentDirectory);
    if (parentDirectory is not null)
    {
        yield return Path.Combine(parentDirectory.FullName, ".env");
    }
}

static string ResolveWebRootPath()
{
    var configuredPath = Environment.GetEnvironmentVariable("APP_WWWROOT_PATH");
    if (!string.IsNullOrWhiteSpace(configuredPath))
    {
        return Path.GetFullPath(configuredPath);
    }

    if (OperatingSystem.IsLinux())
    {
        return "/data/polipersjournal";
    }

    return Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "data", "polipersjournal"));
}

static void EnsureWebRootDirectory(string webRootPath)
{
    if (!Directory.Exists(webRootPath))
    {
        Directory.CreateDirectory(webRootPath);
        Console.WriteLine($"Static file root created: {webRootPath}");
    }
    else
    {
        Console.WriteLine($"Static file root already exists: {webRootPath}");
    }

    if (!OperatingSystem.IsLinux())
    {
        return;
    }

    try
    {
        var chmod = System.Diagnostics.Process.Start("chmod", $"-R 777 {webRootPath}");
        chmod?.WaitForExit();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"chmod failed for static file root: {ex.Message}");
    }
}
