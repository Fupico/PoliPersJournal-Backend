using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.LatestCardDTOs
{
    public class LatestArticleDto
    {
        public int Id { get; set; }
        public string Slug { get; set; } = "";
        public string Title { get; set; } = "";
        public string Summary { get; set; } = ""; // ✅ Yeni alan
        public string CoverImageUrl { get; set; } = "";
        public string Category { get; set; } = "";
        public List<string> Authors { get; set; } = new();
        public string Date { get; set; } = "";
    }

}
