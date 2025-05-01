using Application.Interfaces;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TranslationService : ITranslationService
    {
        private readonly ITranslationRepository _translationRepository;

        public TranslationService(ITranslationRepository translationRepository)
        {
            _translationRepository = translationRepository;
        }

        public async Task<Dictionary<string, string>> GetTranslationsAsync(string languageCode)
        {
            var translations = await _translationRepository.GetTranslationsByLanguageCode(languageCode);
            return translations.ToDictionary(t => t.Key, t => t.Value);
        }
    }
}
