using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/translations")]
    public class TranslationController : ControllerBase
    {
        private readonly ITranslationService _translationService;

        public TranslationController(ITranslationService translationService)
        {
            _translationService = translationService;
        }

        [HttpGet("{languageCode}")]
        public async Task<IActionResult> GetTranslations(string languageCode)
        {
            var translations = await _translationService.GetTranslationsAsync(languageCode);
            return Ok(translations);
        }
    }
}
