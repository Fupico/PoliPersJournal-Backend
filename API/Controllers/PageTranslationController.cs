using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageTranslationController : ControllerBase
    {
        private readonly IPageTranslationService _pageTranslationService;

        public PageTranslationController(IPageTranslationService pageTranslationService)
        {
            _pageTranslationService = pageTranslationService;
        }

        [HttpGet("page-info")]
        public async Task<ActionResult<PageTranslation>> GetCategoryPageInfo([FromQuery] string lang, string pageKey)
        {
            var pageData = await _pageTranslationService.GetPageTranslationAsync(pageKey, lang);
            if (pageData == null) return NotFound("Translation not found.");
            return Ok(pageData);
        }
    }
}
