using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.PostDTOs
{
    public class PostAuthorDto
    {
        public string Id { get; set; }           // Identity string
        public string Name { get; set; }         // Full name
        public string University { get; set; }   // Üniversite
        public string Avatar { get; set; }       // Profil görseli
        public string BioKey { get; set; }       // Çeviri için anahtar (i18n destekli)
    }

}
