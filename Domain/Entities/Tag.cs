namespace Domain.Entities
{
    public class Tag
    {
        public int Id { get; set; }  // Etiket için benzersiz ID
        public string Slug { get; set; } // SEO uyumlu slug
        public virtual ICollection<TagTranslation> Translations { get; set; } = new List<TagTranslation>(); // Çeviriler
    }

}
