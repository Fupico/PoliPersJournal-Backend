using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.PostDTOs
{
    public class PostListDto
    {
        public string Slug { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Summary { get; set; }
        public string CoverImageUrl { get; set; } = string.Empty;
        public int ViewCount { get; set; }
    }
}
