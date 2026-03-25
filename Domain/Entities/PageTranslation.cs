namespace Domain.Entities
{
    public class PageTranslation
    {
        public int Id { get; set; }  // ✅ Benzersiz ID
        public string PageKey { get; set; } = string.Empty;  // ✅ "categories", "post-detail", "about" gibi sayfa kimliği
        public string LanguageCode { get; set; } = string.Empty; // ✅ "tr", "en", "de"
        public string Title { get; set; } = string.Empty;  // ✅ Sayfa Başlığı
        public string Subtitle { get; set; } = string.Empty;  // ✅ Sayfa Açıklaması
    }
}
