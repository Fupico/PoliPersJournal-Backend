﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.RelatedArticleDTOs
{
    public class RelatedArticleDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty; // ✅ eklendi
    }


}
