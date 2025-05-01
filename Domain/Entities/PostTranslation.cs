namespace Domain.Entities
{
    public class PostTranslation
    {
        public int Id { get; set; }  // Çeviri için benzersiz ID
        public int PostId { get; set; } // İlişkili blog yazısı ID'si
        public string LanguageCode { get; set; } = string.Empty; // "tr", "en", "de" gibi dil kodları

        // 📌 **Temel İçerik Alanları**
        public string Title { get; set; } = string.Empty; // Başlık
        public string Content { get; set; } = string.Empty; // İçerik
        public string? Summary { get; set; } // **Özet** (SEO & listeler için)
        public int? ReadingTime { get; set; } // örn: "5" "dakika"


        // 📌 **SEO Alanları**
        public string MetaTitle { get; set; } = string.Empty;  // **SEO Başlığı**
        public string MetaDescription { get; set; } = string.Empty;  // **SEO Açıklaması**
        public string MetaKeywords { get; set; } = string.Empty;  // **Anahtar kelimeler**

        // 📌 **Medya Desteği**
        public string? PdfUrl { get; set; } // **İndirilebilir PDF dosyası**

        // 📌 **Ekstra Bilgiler**
        public bool IsPublished { get; set; } = true; // **Çeviri yayında mı?**
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // **Oluşturulma tarihi**
        public DateTime? UpdatedAt { get; set; } // **Güncellenme tarihi**

        // ✅ GÖRÜNTÜLENME & İNDİRME SAYACI
        public int ViewCount { get; set; } = 0;
        public int DownloadCount { get; set; } = 0;
        // 📌 **İlişkiler**
        public virtual Post Post { get; set; } = null!;
        public virtual ICollection<PostDownloadTracking> DownloadTrackings { get; set; } = new List<PostDownloadTracking>(); // **İndirme takibi**
        public virtual ICollection<PostSection> Sections { get; set; } = new List<PostSection>();

    }

}
