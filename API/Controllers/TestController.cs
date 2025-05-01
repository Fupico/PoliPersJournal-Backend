using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("public")]
        public IActionResult PublicEndpoint()
        {
            Console.WriteLine("Public endpoint çağrıldı.");
            return Ok("Bu herkese açık bir endpoint.");
        }

        [HttpGet("private")]
        [Authorize]  // 🔥 Yetkilendirme gerekli
        public IActionResult PrivateEndpoint()
        {
            Console.WriteLine("Private endpoint çağrıldı.");
            return Ok("Bu yalnızca yetkilendirilmiş kullanıcılar içindir.");
        }

        // 🔥 **Sadece Admin erişebilir**
        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public IActionResult AdminEndpoint()
        {
            return Ok(new { Message = "Bu endpoint sadece Admin rolüne sahip kullanıcılar içindir." });
        }

        // 🔥 **Admin veya Öğretmen erişebilir**
        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("admin-or-teacher")]
        public IActionResult AdminOrTeacherEndpoint()
        {
            return Ok(new { Message = "Bu endpoint sadece Admin veya Teacher rolüne sahip kullanıcılar içindir." });
        }
    }
}
