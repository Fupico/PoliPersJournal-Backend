using Shared.Responses;
using Microsoft.AspNetCore.Http; // IFormFile


namespace Domain.Interfaces
{
    public interface IFileService
    {
        Task<ApiResponse<string>> UploadFileAsync(IFormFile file, string userId);
        Task<ApiResponse<NoDataDto>> DeleteFileAsync(string filePath);
        Task<ApiResponse<List<UserFileItemDto>>> GetUserFilesAsync(string userId);

    }

}
