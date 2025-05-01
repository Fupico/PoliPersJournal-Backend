namespace Application.DTOs.ArticleAuthorDTOs
{
    public class ArticleAuthorDto
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string University { get; set; } = "";
        public string Avatar { get; set; } = "";
        public string Bio { get; set; } = "";
        public string? ProfileLink { get; set; } // ✅ slug için kullanılacak

    }

}
