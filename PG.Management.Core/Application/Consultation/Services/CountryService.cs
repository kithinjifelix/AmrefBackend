using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PG.Management.Core.Domain.Consultation;
using PG.Management.Core.Domain.Consultation.Models;
using PG.Management.Core.Domain.Consultation.Services;

namespace PG.Management.Core.Application.Consultation.Services
{
    public class CountryService : ICountryService
    {
        public readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<(bool IsSuccess, IEnumerable<Country> countries, string ErrorMessage)> LoadCountries()
        {
            try
            {
                var countries = await _countryRepository.GetAll().ToListAsync();
                if (countries == null)
                    return (false, new List<Country>(), "No record found.");
                return (true, countries, "Countries loaded successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        (bool IsSuccess, Country country, string ErrorMessage) ICountryService.GetCountry(Guid id)
        {
            try
            {
                var country = _countryRepository.GetById(id);
                if (country == null)
                    return (false, null, "Country not found.");
                return (true, country, "Country loaded successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return (false, null, $"{e.Message}");
            }
        }
    }
}