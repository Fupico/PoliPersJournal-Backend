using Application.DTOs.UserDTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var result = await _userService.RegisterUserAsync(model);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// ✍️ Devrim Mehmet Pattabanoğlu | 🗓️ 2025-05-01
        /// 📌 Kullanıcının sisteme giriş yapmasını sağlar.
        /// </summary>
        [SwaggerOperation(
            Summary = "Kullanıcı Girişi",
            Description = "Username, Email veya Telefon ile giriş yapılır. Şifre zorunludur."
        )]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var result = await _userService.LoginAsync(model);
            return result.Success ? Ok(result) : Unauthorized(result);
        }

    }
}
