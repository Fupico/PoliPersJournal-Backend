using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CategoryDTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; } // Çeviri ile alınacak
        public string Icon { get; set; } // ✅ Backend'den gelen ikon
        public int ArticleCount { get; set; } // ✅ Dinamik olarak hesaplanacak
    }


}
