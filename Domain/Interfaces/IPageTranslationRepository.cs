using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPageTranslationRepository
    {
        Task<PageTranslation?> GetPageTranslationAsync(string pageKey, string languageCode);
    }
}
