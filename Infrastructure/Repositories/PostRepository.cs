using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _context;

        public PostRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetPostsByCategorySlugAsync(string categorySlug, string languageCode)
        {
            return await _context.Posts
                .Where(p => p.PostCategories.Any(pc => pc.Category.Slug == categorySlug))
                .Include(p => p.PostTranslations.Where(t => t.LanguageCode == languageCode)) // 🌍 Dil Filtresi
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }
        public async Task<Post?> GetPostBySlugAsync(string slug)
        {
            return await _context.Posts
                .Include(p => p.PostTranslations)
                .Include(p => p.PostCategories)
                    .ThenInclude(pc => pc.Category)
                        .ThenInclude(c => c.Translations)
                .FirstOrDefaultAsync(p => p.Slug == slug);
        }
        public async Task<Post?> GetPostWithAuthorsAsync(string slug)
        {
            return await _context.Posts
                .Include(p => p.Authors)
                    .ThenInclude(pa => pa.Author)
                        .ThenInclude(a => a.Translations)
                .FirstOrDefaultAsync(p => p.Slug == slug);
        }
        public async Task<List<Post>> GetRelatedPostsAsync(Post post)
        {
            var categoryIds = post.PostCategories.Select(pc => pc.CategoryId).ToList();

            return await _context.Posts
                .Where(p => p.Id != post.Id &&
                            p.IsPublished &&
                            p.PostCategories.Any(pc => categoryIds.Contains(pc.CategoryId)))
                .OrderByDescending(p => p.CreatedAt)
                .Take(5)
                .Include(p => p.PostTranslations)
                .ToListAsync();
        }
        public async Task<Post?> GetPostWithKeywordsAsync(string slug)
        {
            return await _context.Posts
                .Include(p => p.Tags)
                    .ThenInclude(pt => pt.Tag)
                        .ThenInclude(t => t.Translations)
                .Include(p => p.DownloadTrackings) // ✅ Bu satırı ekle!
                .Include(p => p.PostTranslations)  // ✅ Title için gerekli
                .FirstOrDefaultAsync(p => p.Slug == slug);
        }
        public async Task<Post?> GetPostWithContentAsync(string slug)
        {
            return await _context.Posts
                .Include(p => p.PostTranslations)
                .ThenInclude(t => t.Sections)
                .FirstOrDefaultAsync(p => p.Slug == slug);
        }
        public async Task IncrementViewAsync(string slug, string lang)
        {
            var translation = await _context.PostTranslations
                .Include(pt => pt.Post)
                .FirstOrDefaultAsync(pt => pt.Post.Slug == slug && pt.LanguageCode == lang);

            if (translation != null)
            {
                translation.ViewCount += 1;
                await _context.SaveChangesAsync();
            }
        }



        public async Task IncrementDownloadAsync(string slug, string lang, string userIp, string userAgent)
        {
            var translation = await _context.PostTranslations
                .Include(pt => pt.Post)
                .FirstOrDefaultAsync(pt => pt.Post.Slug == slug && pt.LanguageCode == lang);

            if (translation != null)
            {
                translation.DownloadCount += 1;

                translation.Post.DownloadTrackings.Add(new PostDownloadTracking
                {
                    PostTranslationId = translation.Id,
                    UserIp = userIp,
                    UserAgent = userAgent
                });

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Post>> GetLatestPostsAsync()
        {
            return await _context.Posts
                .Where(p => p.IsPublished)
                .Include(p => p.PostTranslations)
                .Include(p => p.PostCategories).ThenInclude(pc => pc.Category).ThenInclude(c => c.Translations)
                .Include(p => p.Authors).ThenInclude(a => a.Author).ThenInclude(a => a.Translations)
                .OrderByDescending(p => p.CreatedAt)
                .Take(10)
                .ToListAsync();
        }













        public async Task<Post?> GetPostWithTranslationsAndSectionsAsync(string slug)
        {
            return await _context.Posts
                .Include(p => p.PostTranslations)
                    .ThenInclude(t => t.Sections)
                .FirstOrDefaultAsync(p => p.Slug == slug);
        }

        public Task<ApplicationUser> GetAuthorByPostSlugAsync(string slug)
        {
            throw new NotImplementedException();
        }
    }
}
