using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICompanyService
    {
        Task<Company> GetCompanyAsync();
    }
}
