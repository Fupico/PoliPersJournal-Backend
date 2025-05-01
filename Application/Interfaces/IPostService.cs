using Application.DTOs.ArticleAuthorDTOs;
using Application.DTOs.LatestCardDTOs;
using Application.DTOs.PostContentDTOs;
using Application.DTOs.PostDTOs;
using Application.DTOs.PostSidebarDTOs;
using Application.DTOs.RelatedArticleDTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPostService
    {
      
        Task<List<PostDto>> GetPostsByCategorySlugAsync(string categorySlug, string languageCode);
        Task<GetPostHeaderDto?> GetPostHeaderAsync(string slug, string languageCode);
        Task<List<RelatedArticleDto>> GetRelatedArticlesAsync(string slug, string lang);
        Task<List<ArticleAuthorDto>> GetArticleAuthorsAsync(string slug, string lang);
        Task<GetPostSidebarDto?> GetPostSidebarAsync(string slug, string languageCode);
        Task<GetPostContentDto?> GetPostContentAsync(string slug, string lang);
        Task IncrementViewAsync(string slug, string lang);
        Task IncrementDownloadAsync(string slug, string lang);

        Task<List<LatestArticleDto>> GetLatestArticlesAsync(string lang);
 




        //Task<PostAuthorDto> GetAuthorByPostSlugAsync(string slug, string lang);
        Task<PdfFileDto?> GetPostPdfByLanguageAsync(int postId, string lang);
 

        //Task<List<PostMiniDto>> GetRelatedPostsAsync(string slug, string lang);
        //Task<PostSidebarDto> GetSidebarDataAsync(string slug, string lang);

    }
}
