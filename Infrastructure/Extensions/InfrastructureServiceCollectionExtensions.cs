using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Security;

namespace Infrastructure.Extensions
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Identity Konfigürasyonu
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();





            services.AddScoped<JwtService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISettingRepository, SettingRepository>();
            // 📌 Servis ve repository bağımlılıklarını kaydet
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITranslationRepository, TranslationRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<IPageTranslationRepository, PageTranslationRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            return services;
        }
    }
}
