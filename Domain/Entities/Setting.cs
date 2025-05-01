using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Setting
    {
        public int Id { get; set; }
        public int CompanyId { get; set; } // 🔗 Şirket ile ilişkilendirme
        public Company? Company { get; set; }

        public bool RequireEmailConfirmation { get; set; } = false;
        public bool RequirePhoneConfirmation { get; set; } = false;
        public bool RequireAdminApproval { get; set; } = false;
        public bool RequireTCApproval { get; set; } = false;
        public bool AllowUserRegistration { get; set; } = true;
        // 📌 Kullanıcıların hangi yöntemlerle giriş yapabileceğini belirler (tinyint)
        [Column(TypeName = "TINYINT")]
        public LoginMethod AllowedLoginMethods { get; set; } = LoginMethod.Username;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
