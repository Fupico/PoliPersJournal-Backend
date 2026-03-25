namespace Application.DTOs.PostDTOs
{
    public class PdfFileDto
    {
        public byte[] Content { get; set; } = Array.Empty<byte>();
        public string ContentType { get; set; } = "application/pdf";
        public string FileName { get; set; } = string.Empty;
    }
}
