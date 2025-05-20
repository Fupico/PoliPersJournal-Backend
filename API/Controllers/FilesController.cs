using Application.DTOs.FileDTOs;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Responses;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        /// <summary>
        /// 📌 Dosya yükleme işlemi (PDF, Word, Excel, PNG, JPG)
        /// 🗓️ 2025-05-20 | ✍️ Devrim Mehmet Pattabanoğlu
        /// </summary>
        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
    Summary = "Dosya yükle",
    Description = "PDF, Word, Excel, PNG, JPG gibi dosyaları yükler."
)]
        public async Task<IActionResult> Upload([FromForm] UploadFileRequestDto request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized(ApiResponse<string>.FailResponse("Kullanıcı bilgisi alınamadı."));

            var result = await _fileService.UploadFileAsync(request.File, userId);
            return StatusCode(result.StatusCode, result);
        }
        /// <summary>
        /// 📂 Kullanıcının yüklediği dosyaların listesini getirir
        /// 🗓️ 2025-05-20 | ✍️ Devrim Mehmet Pattabanoğlu
        /// </summary>
        [HttpGet("list")]
        [ProducesResponseType(typeof(ApiResponse<List<string>>), StatusCodes.Status200OK)]
        [SwaggerOperation(
            Summary = "Kullanıcının tüm dosyalarını getir",
            Description = "JWT içindeki kimlik bilgisine göre kullanıcının yüklediği tüm dosyaları listeler."
        )]
        [Authorize]
        public async Task<IActionResult> GetMyFiles()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized(ApiResponse<List<string>>.FailResponse("Kullanıcı bilgisi alınamadı."));

            var result = await _fileService.GetUserFilesAsync(userId);
            return StatusCode(result.StatusCode, result);
        }



        /// <summary>
        /// ❌ Dosya silme işlemi
        /// 🗓️ 2025-05-20 | ✍️ Devrim Mehmet Pattabanoğlu
        /// </summary>
        [SwaggerOperation(
            Summary = "Dosya sil",
            Description = "Sistemde daha önce yüklenmiş bir dosyayı siler.\n\n" +
                          "- 📄 `filePath` parametresi örnek: `uploads/{userId}/{tarih}/dosya.pdf`\n" +
                          "- 🗑️ Dosya yoksa 404 döner, başarıyla silinirse 200 döner."
        )]
        [ProducesResponseType(typeof(ApiResponse<NoDataDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<NoDataDto>), StatusCodes.Status404NotFound)]
        [HttpDelete("delete")]
        [Authorize]
        public async Task<IActionResult> Delete([FromQuery] string filePath)
        {
            var result = await _fileService.DeleteFileAsync(filePath);
            return StatusCode(result.StatusCode, result);
        }
    }
}
