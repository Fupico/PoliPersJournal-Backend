using Application.DTOs.CategoryDTOs;
using Application.DTOs.PostDTOs;
using Application.Interfaces;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryDto>> GetCategoriesAsync(string languageCode)
        {
            var categories = await _categoryRepository.GetAllCategoriesWithTranslationsAsync();

            var categoryDtos = new List<CategoryDto>();

            foreach (var category in categories)
            {
                int articleCount = await _categoryRepository.GetArticleCountByCategoryIdAsync(category.Id);

                categoryDtos.Add(new CategoryDto
                {
                    Id = category.Id,
                    Slug = category.Slug,
                    Name = category.Translations.FirstOrDefault(t => t.LanguageCode == languageCode)?.Name ?? category.Slug,
                    Icon = category.Icon, // ✅ Backend'den gelen ikon
                    ArticleCount = articleCount // ✅ Makale sayısını repository'den al
                });
            }

            return categoryDtos;
        }

        public async Task<List<PostListDto>> GetPostsByCategoryAsync(string categorySlug, string lang)
        {
            var posts = await _categoryRepository.GetPostsByCategoryAsync(categorySlug);
            return posts.Select(post => new PostListDto
            {
                Slug = post.Slug,
                Title = post.PostTranslations.FirstOrDefault(pt => pt.LanguageCode == lang)?.Title ?? "No Title",
                Summary = post.PostTranslations.FirstOrDefault(pt => pt.LanguageCode == lang)?.Summary ?? "No Summary",
                CoverImageUrl = post.CoverImageUrl,
                ViewCount = post.PostTranslations.FirstOrDefault(pt => pt.LanguageCode == lang)?.ViewCount ?? 0
            }).ToList();
        }


        public async Task<List<PostListDto>> GetRelatedPostsByCategoryAsync(string categorySlug, string lang)
        {
            var posts = await _categoryRepository.GetRelatedPostsByCategoryAsync(categorySlug);
            return posts.Select(post => new PostListDto
            {
                Slug = post.Slug,
                Title = post.PostTranslations.FirstOrDefault(pt => pt.LanguageCode == lang)?.Title ?? "No Title",
                Summary = post.PostTranslations.FirstOrDefault(pt => pt.LanguageCode == lang)?.Summary ?? "No Summary",
                CoverImageUrl = post.CoverImageUrl,
                ViewCount = post.PostTranslations.FirstOrDefault(pt => pt.LanguageCode == lang)?.ViewCount ?? 0
            }).ToList();
        }

    }
}
