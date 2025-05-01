using Application.DTOs.UserDTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Responses;
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

        /// <summary>
        /// 📌 Kullanıcının sisteme giriş yapmasını sağlar.
        /// 🗓️ 2025-05-01 | ✍️ Devrim Mehmet Pattabanoğlu
        /// </summary>
        [SwaggerOperation(
            Summary = "Kullanıcı Girişi",
            Description = "Username, Email veya Telefon ile giriş yapılır. Şifre zorunludur. Giriş başarılı olursa JWT token döner.\n\n" +
                          "Ancak sistem politikalarına göre aşağıdaki koşullar sağlanmadan giriş yapılamaz:\n" +
                          "- 📧 E-posta adresi doğrulanmamışsa giriş engellenir.\n" +
                          "- 📱 Telefon numarası doğrulanmamışsa giriş engellenir.\n" +
                          "- 🛡️ Hesap yönetici tarafından onaylanmamışsa giriş engellenir.\n\n" +
                          "Bu kontroller sistem ayarlarına göre yapılır ve kullanıcıya detaylı hata mesajı döner."
        )]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var result = await _userService.LoginAsync(model);
            return result.Success ? Ok(result) : Unauthorized(result);
        }



        /// <summary>
        /// 📌 Yeni kullanıcı kaydı oluşturur.
        /// 🗓️ 2025-05-01 | ✍️ Devrim Mehmet Pattabanoğlu
        /// </summary>
        [SwaggerOperation(
            Summary = "Yeni kullanıcı kaydı",
            Description = "Sistem ayarlarına göre kullanıcı adı, e-posta veya telefon bilgisi zorunlu olabilir. Şifre zorunludur.\n\n" +
                          "Kayıt olduktan sonra:\n" +
                          "- Eğer e-posta doğrulama gerekiyorsa e-posta onayı yapılmalıdır,\n" +
                          "- Eğer telefon doğrulama gerekiyorsa SMS onayı yapılmalıdır,\n" +
                          "- Eğer yönetici onayı gerekiyorsa kullanıcı sisteme giriş yapamaz.\n\n" +
                          "Tüm bu ayarlar sistemde tanımlı giriş politikalarına göre otomatik belirlenir."
        )]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var result = await _userService.RegisterUserAsync(model);
            return result.Success ? Ok(result) : BadRequest(result);
        }


    }
}
