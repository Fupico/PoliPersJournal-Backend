namespace Domain.Entities
{
    public class PostCategory
    {
        public int PostId { get; set; } // Makale ID
        public int CategoryId { get; set; } // Kategori ID

        public virtual Post Post { get; set; } // Post ile ilişki
        public virtual Category Category { get; set; } // Kategori ile ilişki
    }

}
