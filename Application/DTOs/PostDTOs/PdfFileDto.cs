namespace Application.DTOs.PostDTOs
{
    public class PdfFileDto
    {
        public byte[] Content { get; set; }
        public string ContentType { get; set; } = "application/pdf";
        public string FileName { get; set; }
    }
}
