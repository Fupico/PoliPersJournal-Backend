namespace Domain.Entities
{
    public class Translation
    {
        public int Id { get; set; }
        public string LanguageCode { get; set; } = string.Empty; // "tr", "en", "de"
        public string Key { get; set; } = string.Empty; // Örneğin "home", "contact"
        public string Value { get; set; } = string.Empty; // Örneğin "Ana Sayfa", "İletişim"
    }


}
