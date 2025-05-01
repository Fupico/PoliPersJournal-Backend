namespace Domain.Entities
{
    public class Translation
    {
        public int Id { get; set; }
        public string LanguageCode { get; set; } // "tr", "en", "de"
        public string Key { get; set; } // Örneğin "home", "contact"
        public string Value { get; set; } // Örneğin "Ana Sayfa", "İletişim"
    }


}
