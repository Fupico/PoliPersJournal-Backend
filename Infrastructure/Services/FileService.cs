using Microsoft.AspNetCore.Hosting;          // IWebHostEnvironment için
using Microsoft.AspNetCore.Http;             // IFormFile için
using Microsoft.Extensions.Logging;          // ILogger için
using System.IO;                             // FileStream, Path, Directory vs.
using System.Linq;                           // .Sum() için
using System.Threading.Tasks;                // Task için

using Domain.Interfaces;                     // IFileService için
using Shared.Responses;                      // ApiResponse<T> ve NoDataDto için


namespace Infrastructure.Services
{
    // =========================================================================================================
    // 📁 FileService - Dosya Sistemi İşlemleri Servisi
    //
    // 🔹 Bu servis, fiziksel dosya işlemlerini (yükleme, silme) gerçekleştirdiği için doğrudan sistem kaynaklarına
    //     (dosya sistemi, klasör yapısı, web root gibi) erişim gerektirir.
    // 🔹 Bu sebeple Clean Architecture kurallarına göre bu servis "Infrastructure" katmanında yer almalıdır.
    // 🔹 Domain ve Application katmanları, sistem kaynaklarına bağımlılık içeremez; bu tür işlemler buraya aktarılır.
    // 🔹 Bu servis IFileService arayüzünü uygular ve bağımlılık enjeksiyonu ile üst katmanlara enjekte edilir.
    //
    // 📁 FileService - File System Operations Service
    //
    // 🔹 This service performs direct file system operations such as uploading and deleting files,
    //     which require access to system-level resources like the web root and physical folders.
    // 🔹 According to Clean Architecture principles, such implementation-specific services should reside
    //     in the "Infrastructure" layer.
    // 🔹 The Domain and Application layers must remain free of framework or system dependencies.
    // 🔹 This service implements the IFileService interface and is injected into higher layers via dependency injection.
    // =========================================================================================================

    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<FileService> _logger;

        public FileService(
            IWebHostEnvironment env,
            IHttpContextAccessor httpContextAccessor,
            ILogger<FileService> logger)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<ApiResponse<string>> UploadFileAsync(IFormFile file, string userId)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();
            var allowedExtensions = new[] { ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".png", ".jpg", ".jpeg" };

            if (!allowedExtensions.Contains(extension))
                return ApiResponse<string>.FailResponse("Unsupported file format.", 400);

            var maxAllowedBytes = 1024L * 1024 * 1024; // 1 GB
            var currentSize = GetUserDirectorySize(userId);
            if (currentSize + file.Length > maxAllowedBytes)
                return ApiResponse<string>.FailResponse("Storage limit (1GB) exceeded.", 400);

            var date = DateTime.Now.ToString("yyyy-MM-dd");
            var folderPath = Path.Combine("uploads", userId, date);
            var fullPath = Path.Combine(_env.WebRootPath, folderPath);

            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);

            var uniqueFileName = $"{Guid.NewGuid()}{extension}";
            var savePath = Path.Combine(fullPath, uniqueFileName);

            await using (var stream = new FileStream(savePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var request = _httpContextAccessor.HttpContext.Request;
            var fileUrl = $"{request.Scheme}://{request.Host}/uploads/{userId}/{date}/{uniqueFileName}";

            return ApiResponse<string>.SuccessResponse(fileUrl, "File uploaded successfully.");
        }

        public Task<ApiResponse<NoDataDto>> DeleteFileAsync(string filePath)
        {
            var fullPath = Path.Combine(_env.WebRootPath, filePath);
            if (!File.Exists(fullPath))
                return Task.FromResult(ApiResponse<NoDataDto>.FailResponse("File not found.", 404));

            File.Delete(fullPath);
            return Task.FromResult(ApiResponse<NoDataDto>.SuccessResponse(null, "File deleted."));
        }
        public async Task<ApiResponse<List<UserFileItemDto>>> GetUserFilesAsync(string userId)
        {
            var basePath = Path.Combine(_env.WebRootPath, "uploads", userId);
            var fileList = new List<UserFileItemDto>();

            if (!Directory.Exists(basePath))
                return ApiResponse<List<UserFileItemDto>>.SuccessResponse(fileList, "Henüz yüklenmiş dosya yok.");

            var files = Directory.GetFiles(basePath, "*.*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var info = new FileInfo(file);

                var relativePath = Path.GetRelativePath(_env.WebRootPath, file).Replace("\\", "/");
                var url = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/{relativePath}";

                var uploadDate = Directory.GetParent(file)?.Name; // yyyy-MM-dd formatlı klasör adı

                fileList.Add(new UserFileItemDto
                {
                    FileName = info.Name,
                    FileSize = info.Length,
                    UploadDate = DateTime.TryParse(uploadDate, out var parsed) ? parsed : info.CreationTime,
                    Url = url
                });
            }

            return ApiResponse<List<UserFileItemDto>>.SuccessResponse(fileList, "Dosyalar listelendi.");
        }


        private long GetUserDirectorySize(string userId)
        {
            var userFolder = Path.Combine(_env.WebRootPath, "uploads", userId);
            if (!Directory.Exists(userFolder)) return 0;

            return Directory.GetFiles(userFolder, "*", SearchOption.AllDirectories)
                .Sum(f => new FileInfo(f).Length);
        }
    }
}
