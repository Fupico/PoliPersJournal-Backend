using Application.DTOs.UserDTOs;
using Shared.Responses;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse<object>> RegisterUserAsync(RegisterDto model);
        Task<ApiResponse<object>> LoginAsync(LoginRequestDto request);  // 🔥 Login metodu eklendi


    }

}
