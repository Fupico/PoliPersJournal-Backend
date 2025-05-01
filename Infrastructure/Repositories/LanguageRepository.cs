using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly AppDbContext _context;

        public LanguageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Language>> GetActiveLanguagesAsync()
        {
            return await _context.Languages
                .Where(lang => lang.IsActive) // ✅ Sadece aktif dilleri al
                .ToListAsync();
        }
    }
}
