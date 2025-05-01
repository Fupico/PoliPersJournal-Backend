using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> GetUserByEmailAsync(string email);
        Task<Setting?> GetCompanySettingsAsync(int schoolId);
        Task<bool> IsRegistrationAllowedAsync(int schoolId);
        Task<bool> CreateUserAsync(ApplicationUser user, string password);
    }
}
