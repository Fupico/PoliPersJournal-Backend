using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.PostDTOs
{
    public class PostAuthorDto
    {
        public string Id { get; set; } = string.Empty;           // Identity string
        public string Name { get; set; } = string.Empty;         // Full name
        public string University { get; set; } = string.Empty;   // Üniversite
        public string Avatar { get; set; } = string.Empty;       // Profil görseli
        public string BioKey { get; set; } = string.Empty;       // Çeviri için anahtar (i18n destekli)
    }

}
