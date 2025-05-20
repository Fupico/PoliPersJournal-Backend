using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.FileDTOs
{
    public class UploadFileRequestDto
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
