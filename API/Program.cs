using API.Middlewares;
using Application.Extensions;  // Application servislerini eklemek için
using Infrastructure.Extensions;  // Infrastructure servislerini eklemek için
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ✅ 1️⃣ Logging Ayarları
builder.Logging.ClearProviders();
builder.Logging.AddConsole(); // 🔥 Konsola log bas
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://localhost:9000", "https://localhost:9001", "http://localhost:9000", "http://localhost:9000", "https://test.polipersjournal.com", "http://test.polipersjournal.com")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});
// ✅ 2️⃣ Servisleri API'ye ekle
builder.Services.AddControllers();
builder.Services.AddApplicationServices(); // Application servisleri
builder.Services.AddInfrastructureServices(builder.Configuration); // Infrastructure servisleri

// ✅ 3️⃣ JWT Authentication Ayarları
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"])),
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
builder.Services.AddOpenApi();

var app = builder.Build();
app.UseStaticFiles(); // wwwroot varsayılan olarak buraya bağlanır

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
