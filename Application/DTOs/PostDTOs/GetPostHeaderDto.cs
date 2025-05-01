namespace Application.DTOs.PostDTOs
{
    public class GetPostHeaderDto
    {
        public string Title { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty; // yyyy-MM-dd
        public string Category { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public int? ReadingTime { get; set; } // dakika
        public string CoverImageUrl { get; set; } = string.Empty;
    }
}
