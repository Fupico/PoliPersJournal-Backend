using Application.DTOs.CategoryDTOs;
using Application.DTOs.PostDTOs;

namespace Application.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetCategoriesAsync(string languageCode);

        Task<List<PostListDto>> GetPostsByCategoryAsync(string categorySlug, string lang);
        Task<List<PostListDto>> GetRelatedPostsByCategoryAsync(string categorySlug, string lang);

    }
}
