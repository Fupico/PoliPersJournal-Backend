using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ILanguageRepository
    {
        Task<List<Language>> GetActiveLanguagesAsync();
    }
}
