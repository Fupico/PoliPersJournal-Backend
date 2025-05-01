using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PageTranslationRepository : IPageTranslationRepository
    {
        private readonly AppDbContext _context;

        public PageTranslationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PageTranslation?> GetPageTranslationAsync(string pageKey, string languageCode)
        {
            return await _context.PageTranslations
                .FirstOrDefaultAsync(t => t.PageKey == pageKey && t.LanguageCode == languageCode)
                ?? await _context.PageTranslations.FirstOrDefaultAsync(t => t.PageKey == pageKey && t.LanguageCode == "en");
        }
    }
}
