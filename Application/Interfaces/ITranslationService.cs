using Application.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
 
     public interface ITranslationService
    {
        Task<Dictionary<string, string>> GetTranslationsAsync(string languageCode);

    }
}
