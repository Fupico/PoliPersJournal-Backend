using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TranslationRepository : ITranslationRepository
    {
        private readonly AppDbContext _context;
        public TranslationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Translation>> GetTranslationsByLanguageCode(string languageCode)
        {
            return await _context.Translations
                .Where(t => t.LanguageCode == languageCode)
                .ToListAsync();
        }
    }
}
