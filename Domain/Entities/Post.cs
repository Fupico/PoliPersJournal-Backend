
namespace Domain.Entities
{
    public class Post
    {
        public int Id { get; set; }  // Blog yazısının benzersiz kimliği
        public string Slug { get; set; } // SEO uyumlu URL için benzersiz bir slug
        public bool IsPublished { get; set; } // Yayınlanma durumu
        public DateTime CreatedAt { get; set; } // Oluşturulma zamanı
        public DateTime? UpdatedAt { get; set; } // Güncellenme zamanı
        public string CreatedBy { get; set; } // Yazıyı oluşturan kullanıcının ID'si

        // 📌 SEO Alanları
        public string MetaTitle { get; set; } = string.Empty;  // SEO için başlık
        public string MetaDescription { get; set; } = string.Empty;  // SEO için açıklama
        public string MetaKeywords { get; set; } = string.Empty;  // Anahtar kelimeler

        // 📌 İçerik Yönetimi
        public string CoverImageUrl { get; set; } = string.Empty; // Öne çıkan görsel
        public bool AllowComments { get; set; } = true; // Yorumlara izin verilip verilmediği

        // 📌 İlişkisel Alanlar
        public virtual ICollection<PostTranslation> PostTranslations { get; set; } = new List<PostTranslation>(); // Çeviriler
        public virtual ICollection<PostCategory> PostCategories { get; set; } = new List<PostCategory>(); // Kategorilerle ilişki
        public virtual ICollection<PostTag> Tags { get; set; } = new List<PostTag>(); // Etiketlerle ilişki
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>(); // Yorumlar
        public virtual ICollection<PostDownloadTracking> DownloadTrackings { get; set; } = new List<PostDownloadTracking>(); // 📥 İndirme takibi
        public virtual ApplicationUser CreatedByUser { get; set; } // Navigation YAZAR

        // ✅ Yeni: Çok yazarlı destek
        public virtual ICollection<PostAuthor> Authors { get; set; } = new List<PostAuthor>();
    }

}
