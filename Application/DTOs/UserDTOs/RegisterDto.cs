using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.UserDTOs
{
    public class RegisterDto
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur.")]
        public string Password { get; set; }

        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Company ID zorunludur.")]
        public int CompanyId { get; set; } = 1;

        public string? TC { get; set; }
    }

}
