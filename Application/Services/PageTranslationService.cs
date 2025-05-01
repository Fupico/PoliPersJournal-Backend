using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class PageTranslationService : IPageTranslationService
    {
        private readonly IPageTranslationRepository _pageTranslationRepository;

        public PageTranslationService(IPageTranslationRepository pageTranslationRepository)
        {
            _pageTranslationRepository = pageTranslationRepository;
        }

        public async Task<PageTranslation?> GetPageTranslationAsync(string pageKey, string languageCode)
        {
            return await _pageTranslationRepository.GetPageTranslationAsync(pageKey, languageCode);
        }
    }
}
