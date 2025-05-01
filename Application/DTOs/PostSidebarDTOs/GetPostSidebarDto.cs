using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.PostSidebarDTOs
{
    public class GetPostSidebarDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Date { get; set; } = "";

        // 🟡 Tüm dillerin toplam sayıları
        public int TotalViews { get; set; }
        public int TotalDownloads { get; set; }

        // 🌍 Aktif dile özel sayılar
        public int LanguageViews { get; set; }
        public int LanguageDownloads { get; set; }

        // 🏷️ Etiketler
        public List<string> Keywords { get; set; } = new();

        // 📄 PDF bağlantısı
        public string PdfUrl { get; set; } = "";
    }


}
