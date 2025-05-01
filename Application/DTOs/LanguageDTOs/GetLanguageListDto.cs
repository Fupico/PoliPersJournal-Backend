using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.LanguageDTOs
{
    public class GetLanguageListDto
    {
        public string Code { get; set; } = string.Empty; // "tr", "en", "de"
        public string Name { get; set; } = string.Empty; // "Türkçe", "English", "Deutsch"
    }
}
