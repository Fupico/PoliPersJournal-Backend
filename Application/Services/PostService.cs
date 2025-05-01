using Application.DTOs.ArticleAuthorDTOs;
using Application.DTOs.LatestCardDTOs;
using Application.DTOs.PostContentDTOs;
using Application.DTOs.PostDTOs;
using Application.DTOs.PostSidebarDTOs;
using Application.DTOs.RelatedArticleDTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostService(IPostRepository postRepository, IHttpContextAccessor httpContextAccessor)
        {
            _postRepository = postRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<PostDto>> GetPostsByCategorySlugAsync(string categorySlug, string languageCode)
        {
            var posts = await _postRepository.GetPostsByCategorySlugAsync(categorySlug, languageCode);

            return posts.Select(post => new PostDto
            {
                Id = post.Id,
                Title = post.PostTranslations.FirstOrDefault()?.Title ,
                Slug = post.Slug,
                Content = post.PostTranslations.FirstOrDefault()?.Content ,
                CreatedAt = post.CreatedAt
            }).ToList();
        }

        public async Task<GetPostHeaderDto?> GetPostHeaderAsync(string slug, string languageCode)
        {
            var post = await _postRepository.GetPostBySlugAsync(slug);
            if (post == null) return null;

            var translation = post.PostTranslations
            .FirstOrDefault(t => t.LanguageCode == languageCode)
            ?? post.PostTranslations.FirstOrDefault(); // varsayılan dil fallback


            if (translation == null) return null;

            var category = post.PostCategories
                .FirstOrDefault()?.Category?.Translations
                .FirstOrDefault(c => c.LanguageCode == languageCode)?.Name ?? "Kategori Yok";

            return new GetPostHeaderDto
            {
                Title = translation.Title,
                Date = post.CreatedAt.ToString("yyyy-MM-dd"),
                Category = category,
                Language = languageCode,
                ReadingTime = translation.ReadingTime,
                CoverImageUrl = post.CoverImageUrl
            };
        }

        public async Task<List<RelatedArticleDto>> GetRelatedArticlesAsync(string slug, string lang)
        {
            var post = await _postRepository.GetPostBySlugAsync(slug);
            if (post == null)
                return new List<RelatedArticleDto>();

            var relatedPosts = await _postRepository.GetRelatedPostsAsync(post);

            return relatedPosts.Select(p =>
            {
                var translation = p.PostTranslations.FirstOrDefault(t => t.LanguageCode == lang)
                                  ?? p.PostTranslations.FirstOrDefault();

                return new RelatedArticleDto
                {
                    Id = p.Id,
                    Title = translation?.Title ?? "(Başlık yok)",
                    Date = p.CreatedAt.ToString("yyyy-MM-dd"),
                    Slug = p.Slug // ✅ Yönlendirme için eklendi
                };
            }).ToList();
        }

        public async Task<List<ArticleAuthorDto>> GetArticleAuthorsAsync(string slug, string lang)
        {
            var post = await _postRepository.GetPostWithAuthorsAsync(slug);
            if (post == null || post.Authors == null)
                return new List<ArticleAuthorDto>();

            return post.Authors.Select(pa =>
            {
                var author = pa.Author;

                var translation = author.Translations.FirstOrDefault(t => t.LanguageCode == lang)
                               ?? author.Translations.FirstOrDefault();

                return new ArticleAuthorDto
                {
                    Id = author.Id,
                    Name = translation?.DisplayName ?? $"{author.Name} {author.Surname}".Trim(),
                    University = translation?.University ?? "Bilinmeyen Üniversite",
                    Avatar = string.IsNullOrWhiteSpace(author.ProfilePictureUrl)
                        ? "https://cdn-icons-png.flaticon.com/512/149/149071.png"
                        : author.ProfilePictureUrl,
                    Bio = translation?.Bio ?? "Yazar hakkında bilgi bulunmamaktadır.",
                    ProfileLink = !string.IsNullOrWhiteSpace(author.ProfileLink)
                        ? author.ProfileLink
                        : GenerateSlug(author.Name, author.Surname) // ✅ Hatalı satır buraya düzeltildi
                };
            }).ToList();
        }
        public static string GenerateSlug(string name, string surname)
        {
            var fullName = $"{name} {surname}".ToLowerInvariant();

            // Türkçe karakter dönüşümleri
            fullName = fullName
                .Replace("ş", "s").Replace("ç", "c").Replace("ğ", "g")
                .Replace("ü", "u").Replace("ö", "o").Replace("ı", "i");

            fullName = Regex.Replace(fullName, @"[^a-z0-9\s-]", ""); // özel karakterleri temizle
            fullName = Regex.Replace(fullName, @"\s+", "-").Trim('-'); // boşlukları tireye çevir
            return fullName;
        }


        public async Task<GetPostSidebarDto?> GetPostSidebarAsync(string slug, string languageCode)
        {
            var post = await _postRepository.GetPostWithKeywordsAsync(slug);
            if (post == null) return null;

            // 🌍 Aktif dile ait çeviri
            var translation = post.PostTranslations
                .FirstOrDefault(t => t.LanguageCode == languageCode)
                ?? post.PostTranslations.FirstOrDefault();

            // 🧮 Tüm çevirilerin toplam View/Download sayısı
            var totalViews = post.PostTranslations.Sum(pt => pt.ViewCount);
            var totalDownloads = post.PostTranslations.Sum(pt => pt.DownloadCount);

            return new GetPostSidebarDto
            {
                Id = post.Id,
                Title = translation?.Title ?? "Başlık Yok",
                Date = post.CreatedAt.ToString("yyyy-MM-dd"),

                // 🌍 Dile özel istatistikler
                LanguageViews = translation?.ViewCount ?? 0,
                LanguageDownloads = translation?.DownloadCount ?? 0,

                // 🔢 Tüm dillerin toplamları
                TotalViews = totalViews,
                TotalDownloads = totalDownloads,

                // 📄 PDF dosyası (dile özel)
                PdfUrl = translation?.PdfUrl ?? "",

                // 🏷️ Etiketler (dile göre)
                Keywords = post.Tags
                    .Select(t =>
                        t.Tag?.Translations
                            .FirstOrDefault(tt => tt.LanguageCode == languageCode)?.Name
                        ?? t.Tag?.Translations.FirstOrDefault()?.Name
                        ?? "")
                    .Where(name => !string.IsNullOrWhiteSpace(name))
                    .ToList()
            };
        }

        public async Task<GetPostContentDto?> GetPostContentAsync(string slug, string lang)
        {
            var post = await _postRepository.GetPostWithContentAsync(slug);
            if (post == null) return null;

            var translation = post.PostTranslations
                .FirstOrDefault(t => t.LanguageCode == lang)
                ?? post.PostTranslations.FirstOrDefault();

            if (translation == null) return null;

            return new GetPostContentDto
            {
                Abstract = translation.Summary ?? "", // veya Abstract alanı kullanıyorsan ona göre değiştir
                Sections = translation.Sections
                    .OrderBy(s => s.Order) // sıralı gelmesini sağla
                    .Select(s => new SectionDto
                    {
                        Title = s.Title,
                        Content = s.Content
                    })
                    .ToList()
            };
        }
        public async Task IncrementViewAsync(string slug, string lang)
        {
            await _postRepository.IncrementViewAsync(slug, lang);
        }

        public async Task IncrementDownloadAsync(string slug, string lang)
        {
            var ip = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "unknown";
            var agent = _httpContextAccessor.HttpContext?.Request?.Headers["User-Agent"].ToString() ?? "unknown";

            await _postRepository.IncrementDownloadAsync(slug, lang, ip, agent);
        }
        public async Task<List<LatestArticleDto>> GetLatestArticlesAsync(string lang)
        {
            var posts = await _postRepository.GetLatestPostsAsync(); // sadece yayınlananlardan son 10 tanesi falan

            return posts.Select(post =>
            {
                var translation = post.PostTranslations
                    .FirstOrDefault(t => t.LanguageCode == lang)
                    ?? post.PostTranslations.FirstOrDefault();

                var category = post.PostCategories
                    .FirstOrDefault()?.Category?.Translations
                    .FirstOrDefault(ct => ct.LanguageCode == lang)?.Name ?? "";

                var authors = post.Authors
                    .Select(pa => pa.Author.Translations
                        .FirstOrDefault(t => t.LanguageCode == lang)?.DisplayName
                        ?? $"{pa.Author.Name} {pa.Author.Surname}")
                    .ToList();

                return new LatestArticleDto
                {
                    Id = post.Id,
                    Slug = post.Slug,
                    Title = translation?.Title ?? "No Title",
                    Summary = translation?.Summary ?? "", // ✅ Summary burada veriliyor
                    CoverImageUrl = post.CoverImageUrl,
                    Category = category,
                    Authors = authors,
                    Date = post.CreatedAt.ToString("yyyy-MM-dd")
                };
            }).ToList();
        }




        public async Task<PdfFileDto?> GetPostPdfByLanguageAsync(int postId, string lang)
        {
            // Örnek: pdf dosyası yolu "wwwroot/pdfs/{lang}/post_{id}.pdf"
            var filePath = Path.Combine("wwwroot", "pdfs", lang.ToLower(), $"post_{postId}.pdf");

            if (!File.Exists(filePath))
                return null;

            var content = await File.ReadAllBytesAsync(filePath);

            return new PdfFileDto
            {
                Content = content,
                ContentType = "application/pdf",
                FileName = $"makale_{postId}_{lang}.pdf"
            };
        }
      



    }

}
