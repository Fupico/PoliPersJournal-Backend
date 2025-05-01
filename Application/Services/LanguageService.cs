using Application.DTOs.LanguageDTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly ILanguageRepository _languageRepository;

        public LanguageService(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public async Task<List<GetLanguageListDto>> GetActiveLanguagesAsync()
        {
            var languages = await _languageRepository.GetActiveLanguagesAsync();

            return languages.Select(lang => new GetLanguageListDto
            {
                Code = lang.Code,
                Name = lang.Name
            }).ToList();
        }
    }
}
