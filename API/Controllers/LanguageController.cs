using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageService _languageService;

        public LanguageController(ILanguageService languageService)
        {
            _languageService = languageService;
        }


        [HttpGet]
        public async Task<ActionResult> GetLanguages()
        {
            var languages = await _languageService.GetActiveLanguagesAsync();
            return Ok(languages);
        }
    }
}
