namespace Domain.Entities
{
    public class PostAuthor
    {
        public int PostId { get; set; }
        public Post Post { get; set; } = null!;

        public string AuthorId { get; set; } = string.Empty;
        public ApplicationUser Author { get; set; } = null!;
    }

}
