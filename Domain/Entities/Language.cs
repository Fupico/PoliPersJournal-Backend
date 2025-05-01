using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Language
    {
        [Key] // Birincil anahtar olarak belirleniyor
        public string Code { get; set; } // "tr", "en", "de"
        public string Name { get; set; } // "Türkçe", "English", "Deutsch"
        public bool IsActive { get; set; } // Kullanılabilir mi?
    }


}
