using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Interfaces
{
    public interface IPostRepository
    {
        Task<List<Post>> GetPostsByCategorySlugAsync(string categorySlug, string languageCode);
        Task<List<Post>> GetRelatedPostsAsync(Post post);
        Task<Post?> GetPostWithAuthorsAsync(string slug);
        Task<Post?> GetPostWithKeywordsAsync(string slug);
        Task<Post?> GetPostWithContentAsync(string slug);
        Task IncrementViewAsync(string slug, string lang);
        Task IncrementDownloadAsync(string slug, string lang, string userIp, string userAgent);

        Task<List<Post>> GetLatestPostsAsync();
     





        Task<Post?> GetPostBySlugAsync(string slug);
      
        Task<Post?> GetPostWithTranslationsAndSectionsAsync(string slug);

    }
}
