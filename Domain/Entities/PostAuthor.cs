namespace Domain.Entities
{
    public class PostAuthor
    {
        public int PostId { get; set; }
        public Post Post { get; set; }

        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }
    }

}
