using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

      

        public async Task<List<Category>> GetAllCategoriesWithTranslationsAsync()
        {
            return await _context.Categories
                .Include(c => c.Translations)
                .Include(c => c.PostCategories) // Makale ilişkilerini yükle
                .ToListAsync();
        }

        public async Task<int> GetArticleCountByCategoryIdAsync(int categoryId)
        {
            return await _context.PostCategories.CountAsync(a => a.CategoryId == categoryId);
        }
        public async Task<List<Post>> GetPostsByCategoryAsync(string categorySlug)
        {
            return await _context.PostCategories
                .Where(pc => pc.Category.Slug == categorySlug)
                .Include(pc => pc.Post) // Önce Post'u Include et
                    .ThenInclude(p => p.PostTranslations) // Sonra Post'un çevirilerini al
                .Select(pc => pc.Post) // Son olarak Post'u çek
                .ToListAsync();
        }



        public async Task<List<Post>> GetRelatedPostsByCategoryAsync(string categorySlug)
        {
            return await _context.PostCategories
                .Where(pc => pc.Category.Slug != categorySlug) // Mevcut kategori hariç benzer kategoriler
                .Include(pc => pc.Post) // Önce Post'u Include et
                    .ThenInclude(p => p.PostTranslations) // Sonra Post'un çevirilerini dahil et
                .OrderBy(x => Guid.NewGuid()) // Rastgele sırala
                .Take(5) // Maksimum 5 adet
                .Select(pc => pc.Post) // Son olarak Post nesnesini çek
                .ToListAsync();
        }


    }
}
