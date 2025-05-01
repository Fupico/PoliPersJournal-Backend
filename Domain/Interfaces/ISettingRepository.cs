using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ISettingRepository
    {
        Task<Setting?> GetSettingsByCompanyIdAsync(int companyId); // 🔍 Şirkete özel ayarları getir
    }
}
