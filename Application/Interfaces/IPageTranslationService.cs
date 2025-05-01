using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPageTranslationService
    {
        Task<PageTranslation?> GetPageTranslationAsync(string pageKey, string languageCode);
    }

}
