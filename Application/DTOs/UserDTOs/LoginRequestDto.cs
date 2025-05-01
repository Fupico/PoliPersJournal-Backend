using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.UserDTOs
{
    public class LoginRequestDto
    {
        public string? UserName { get; set; } // Kullanıcı adıyla giriş yapılabilir
        public string? Email { get; set; } // E-posta ile giriş yapılabilir
        public string? PhoneNumber { get; set; } // Telefon numarasıyla giriş yapılabilir

        [Required(ErrorMessage = "Şifre zorunludur.")]
        public string Password { get; set; } // Şifre zorunludur
    }

}
