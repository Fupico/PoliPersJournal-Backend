using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<Setting?> GetCompanySettingsAsync(int companyId)
        {
            return await _context.Settings.FirstOrDefaultAsync(s => s.CompanyId == companyId);
        }

        public async Task<bool> IsRegistrationAllowedAsync(int schoolId)
        {
            var settings = await GetCompanySettingsAsync(schoolId);
            return settings != null && settings.AllowUserRegistration;
        }

        public async Task<bool> CreateUserAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }
    }
}
