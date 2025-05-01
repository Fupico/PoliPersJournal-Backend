using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IPostService _postService;

        public CategoryController(ICategoryService categoryService, IPostService postService)
        {
            _categoryService = categoryService;
            _postService = postService;
        }

        // 📌 GET api/categories?lang=tr
        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] string lang = "en")
        {
            var categories = await _categoryService.GetCategoriesAsync(lang);
            return Ok(categories);
        }
        // ✅ Belirli bir kategoriye ait yazıları getir
        [HttpGet("{slug}/posts")]
        public async Task<IActionResult> GetPostsByCategory(string slug, [FromQuery] string lang = "en")
        {
            var posts = await _categoryService.GetPostsByCategoryAsync(slug, lang);
            return Ok(posts);
        }

        // ✅ Benzer kategorilerdeki yazıları getir
        [HttpGet("{slug}/related-posts")]
        public async Task<IActionResult> GetRelatedPostsByCategory(string slug, [FromQuery] string lang = "en")
        {
            var relatedPosts = await _categoryService.GetRelatedPostsByCategoryAsync(slug, lang);
            return Ok(relatedPosts);
        }
    }
}
