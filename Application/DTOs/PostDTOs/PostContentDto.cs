namespace Application.DTOs.PostDTOs
{
    public class PostContentDto
    {
        public string Abstract { get; set; } = string.Empty;

        public string? Content { get; set; } // HTML olarak tam içerik

        public List<PostContentSectionDto>? Sections { get; set; } // Çoklu bölümler
    }
}
