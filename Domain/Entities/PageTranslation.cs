namespace Domain.Entities
{
    public class PageTranslation
    {
        public int Id { get; set; }  // ✅ Benzersiz ID
        public string PageKey { get; set; }  // ✅ "categories", "post-detail", "about" gibi sayfa kimliği
        public string LanguageCode { get; set; } // ✅ "tr", "en", "de"
        public string Title { get; set; }  // ✅ Sayfa Başlığı
        public string Subtitle { get; set; }  // ✅ Sayfa Açıklaması
    }
}
