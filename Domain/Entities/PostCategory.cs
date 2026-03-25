namespace Domain.Entities
{
    public class PostCategory
    {
        public int PostId { get; set; } // Makale ID
        public int CategoryId { get; set; } // Kategori ID

        public virtual Post Post { get; set; } = null!; // Post ile ilişki
        public virtual Category Category { get; set; } = null!; // Kategori ile ilişki
    }

}
