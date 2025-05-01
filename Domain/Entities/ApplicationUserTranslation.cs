using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ApplicationUserTranslation
    {
        public int Id { get; set; }
        public string LanguageCode { get; set; } = "tr";

        public string? Bio { get; set; }             // ✅ Çok dilli biyografi
        public string? University { get; set; }      // ✅ Çok dilli üniversite adı
        public string? DisplayName { get; set; }     // ✅ Çok dilli görünen ad

        public string UserId { get; set; } = "";
        public ApplicationUser? User { get; set; }
    }


}
