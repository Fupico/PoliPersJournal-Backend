using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.UserDTOs
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Okul ID zorunludur.")]
        public int CompanyId { get; set; } // Kullanıcının giriş yapacağı Şirket ID

        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur.")]
        public string Password { get; set; }
    }

}
