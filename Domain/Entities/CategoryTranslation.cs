namespace Domain.Entities
{
    public class CategoryTranslation
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string LanguageCode { get; set; }
        public string Name { get; set; } // Kategorinin adı
        public string Title { get; set; } // ✅ Yeni Alan
        public string Subtitle { get; set; } // ✅ Yeni Alan
        public virtual Category Category { get; set; } // Kategori ilişkisi
    }


}
