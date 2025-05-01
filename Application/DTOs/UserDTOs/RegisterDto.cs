using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.UserDTOs
{
    public class RegisterDto
    {
        /// <summary>📛 Kullanıcının adı (zorunlu değil)</summary>
        public string? Name { get; set; }

        /// <summary>📛 Kullanıcının soyadı (zorunlu değil)</summary>
        public string? Surname { get; set; }

        /// <summary>🔑 Kullanıcı adı (giriş yöntemine göre zorunlu olabilir)</summary>
        public string? UserName { get; set; }

        /// <summary>📧 E-posta adresi (giriş yöntemine göre zorunlu olabilir)</summary>
        public string? Email { get; set; }

        /// <summary>📱 Telefon numarası (giriş yöntemine göre zorunlu olabilir)</summary>
        public string? PhoneNumber { get; set; }

        /// <summary>🔒 Şifre (zorunlu)</summary>
        [Required(ErrorMessage = "Şifre zorunludur.")]
        public required string Password { get; set; }

        /// <summary>📌 TC Kimlik No (11 haneli, opsiyonel)</summary>
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik Numarası 11 haneli olmalıdır.")]
        public string? TC { get; set; }
    }


}
