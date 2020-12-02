using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PG.Management.Core.Domain.Consultation.Models;

namespace PG.Management.Core.Domain.Consultation.Services
{
    public interface ICountryService
    {
        Task<(bool IsSuccess, IEnumerable<Country> countries, string ErrorMessage)> LoadCountries();
        (bool IsSuccess, Country country, string ErrorMessage) GetCountry(Guid id);
    }
}