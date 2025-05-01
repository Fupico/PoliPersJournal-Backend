using Domain.Entities;

namespace Domain.Interfaces
{
  public interface   ITranslationRepository
    {
        Task<List<Translation>> GetTranslationsByLanguageCode(string languageCode);
    }
}
