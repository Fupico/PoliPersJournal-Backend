namespace Domain.Entities
{
    public class TagTranslation
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public string LanguageCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty; // Etiket adı
        public virtual Tag Tag { get; set; } = null!; // İlgili etiket
    }


}
