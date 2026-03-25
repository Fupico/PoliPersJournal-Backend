namespace Domain.Entities
{
    public class CategoryTranslation
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string LanguageCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty; // Kategorinin adı
        public string Title { get; set; } = string.Empty; // ✅ Yeni Alan
        public string Subtitle { get; set; } = string.Empty; // ✅ Yeni Alan
        public virtual Category Category { get; set; } = null!; // Kategori ilişkisi
    }


}
