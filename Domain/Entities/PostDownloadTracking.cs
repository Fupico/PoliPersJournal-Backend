using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PostDownloadTracking
    {
        public int Id { get; set; }  // **Benzersiz kimlik**
        public int PostTranslationId { get; set; } // **İlişkili PostTranslation**
        public DateTime DownloadedAt { get; set; } = DateTime.UtcNow; // **İndirme zamanı**
        public string UserIp { get; set; } = string.Empty; // **İndiren kişinin IP adresi**
        public string? UserAgent { get; set; } // **Tarayıcı bilgisi**

        // **İlişki**
        public virtual PostTranslation PostTranslation { get; set; } = null!;
    }
}
