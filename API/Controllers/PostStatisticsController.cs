using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/posts/{slug}/stats")]
    public class PostStatisticsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostStatisticsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost("view")] // ✔ SADECE EK YOL
        public async Task<IActionResult> IncrementView(string slug, [FromQuery] string lang)
        {
            await _postService.IncrementViewAsync(slug, lang);
            return Ok();
        }

        [HttpPost("download")] // ✔ SADECE EK YOL
        public async Task<IActionResult> IncrementDownload(string slug, [FromQuery] string lang)
        {
            await _postService.IncrementDownloadAsync(slug, lang);
            return Ok();
        }
    }


}
