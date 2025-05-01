namespace Domain.Entities
{
    public class TagTranslation
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public string LanguageCode { get; set; }
        public string Name { get; set; } // Etiket adı
        public virtual Tag Tag { get; set; } // İlgili etiket
    }


}
