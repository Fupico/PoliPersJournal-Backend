using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SettingRepository : ISettingRepository
    {
        private readonly AppDbContext _context;

        public SettingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Setting?> GetSettingsByCompanyIdAsync(int companyId)
        {
            return await _context.Settings.FirstOrDefaultAsync(s => s.CompanyId == companyId);
        }
    }
}
