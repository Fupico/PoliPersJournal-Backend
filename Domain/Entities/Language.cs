using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Language
    {
        [Key] // Birincil anahtar olarak belirleniyor
        public string Code { get; set; } = string.Empty; // "tr", "en", "de"
        public string Name { get; set; } = string.Empty; // "Türkçe", "English", "Deutsch"
        public bool IsActive { get; set; } // Kullanılabilir mi?
    }


}
