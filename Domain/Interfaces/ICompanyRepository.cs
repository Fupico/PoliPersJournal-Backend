using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICompanyRepository
    {
        Task<Company> GetCompanyAsync();
    }
}
