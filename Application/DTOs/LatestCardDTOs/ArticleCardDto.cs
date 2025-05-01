using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.LatestCardDTOs
{
    public class ArticleCardDto
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string CoverImageUrl { get; set; }
        public string Category { get; set; }
        public string Date { get; set; }
    }

}
