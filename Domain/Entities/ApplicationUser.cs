using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{

    public class ApplicationUser : IdentityUser<string>
    {
        // 👤 Ad-Soyad (Gerekli)
        public string Name { get; set; } = string.Empty; // ✅ Gerekli
        public string Surname { get; set; } = string.Empty; // ✅ Gerekli

        // 🎂 Doğum Tarihi
        public DateTime? DateOfBirth { get; set; } // 🔁 Opsiyonel (ileride sağlık/sigorta gibi modüllerde gerekebilir)

        // 🖼️ Profil Görseli
        public string? ProfilePictureUrl { get; set; } // 🔁 Opsiyonel
        public string? ProfileLink { get; set; } // Özelleştirilmiş Profil Sayfasının Linki

        // 📍 Adres
        public string? Address { get; set; }

        // 📌 TC Kimlik Numarası (SHA-256 ile şifrelenmiş)
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik Numarası 11 haneli olmalıdır.")]
        [Column(TypeName = "varchar(256)")]
        public string? TC { get; set; }

        // 🏢 Şirket & Lokasyon
        public int? CompanyId { get; set; }
        public Company? Company { get; set; }

        // 📍 Şehir/İlçe
        public int? CityId { get; set; } // 🔁 Opsiyonel  İl (adres detayında gerekebilir)
        public City? City { get; set; }

        public int? DistrictId { get; set; } // 🔁 Opsiyonel İlçe
        public District? District { get; set; }

        // 📅 Kayıt Zamanları
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // Profilin Açıldığı Tarih
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow; // Güncelleme Zamanı

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
