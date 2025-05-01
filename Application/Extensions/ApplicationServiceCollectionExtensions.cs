using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IUserService, UserService>(); // 🔥 `UserService`'i DI'a ekle
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITranslationService, TranslationService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<IPageTranslationService, PageTranslationService>();
            services.AddScoped<IPostService, PostService>();
            return services;
        }
    }
}
