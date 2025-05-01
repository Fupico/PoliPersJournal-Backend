using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PostSection
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Content { get; set; } = ""; // HTML olarak
        public int Order { get; set; } = 0;

        public int PostTranslationId { get; set; }
        public PostTranslation PostTranslation { get; set; }
    }


}
