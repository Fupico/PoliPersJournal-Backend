using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICategoryRepository
    {
        //Task<List<Category>> GetAllCategoriesWithTranslationsAsync(string languageCode);
        Task<List<Category>> GetAllCategoriesWithTranslationsAsync();
        Task<int> GetArticleCountByCategoryIdAsync(int categoryId); // ✅ Kategoriye ait makale sayısını getir

        Task<List<Post>> GetPostsByCategoryAsync(string categorySlug);
        Task<List<Post>> GetRelatedPostsByCategoryAsync(string categorySlug);


    }
}
