using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
   
        public class ApplicationUser : IdentityUser<string>
        {
            // 👤 Kişisel Bilgiler
            public string Name { get; set; } = string.Empty;
            public string Surname { get; set; } = string.Empty;
            public DateTime? DateOfBirth { get; set; }
            public string? ProfilePictureUrl { get; set; } // 🖼️ Profil fotoğrafı
            public string? ProfileLink { get; set; } // 🖼️ Profil Linki


        // 📍 Adres
        public string? Address { get; set; }

            // 📌 TC Kimlik Numarası (SHA-256 ile şifrelenmiş)
            [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik Numarası 11 haneli olmalıdır.")]
            [Column(TypeName = "varchar(256)")]
            public string? TC { get; set; }

            // 🏢 Şirket & Lokasyon
            public int? CompanyId { get; set; }
            public Company? Company { get; set; }

            public int? CityId { get; set; }
            public City? City { get; set; }

            public int? DistrictId { get; set; }
            public District? District { get; set; }

            // 📅 Kayıt Zamanları
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
            public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

            // 🔒 Kullanıcı Durumu
            [Column(TypeName = "TINYINT")]
            public byte Invalidated { get; set; } = 0;

            // 🔐 Varsayılan ID oluşturma
            public ApplicationUser()
            {
                Id = Guid.NewGuid().ToString();
            }

            // 📝 Yazarlık ilişkisi (çoklu post - çoklu yazar)
            public ICollection<PostAuthor> AuthoredPosts { get; set; } = new List<PostAuthor>();
        public virtual ICollection<ApplicationUserTranslation> Translations { get; set; } = new List<ApplicationUserTranslation>();

    }

}
