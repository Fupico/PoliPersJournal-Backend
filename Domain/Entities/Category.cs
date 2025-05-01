namespace Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }  // Kategori için benzersiz ID
        public string Slug { get; set; } // SEO uyumlu slug
        public string Icon { get; set; } // ✅ Kategoriye özel ikon (örnek: "school")
        public virtual ICollection<CategoryTranslation> Translations { get; set; } = new List<CategoryTranslation>(); // Çeviriler
        public virtual ICollection<PostCategory> PostCategories { get; set; } = new List<PostCategory>(); // ✅ Güncellendi

    }


}
