using System;
using Microsoft.EntityFrameworkCore;
using PG.Management.Core.Domain.Consultation;
using PG.Management.Core.Domain.Consultation.Models;
using PG.SharedKernel.Infrastructure.Persistence;

namespace PG.Management.Infrastructure.Persistence
{
    public class CountryRepository : BaseRepository<Country, Guid>, ICountryRepository
    {
        public CountryRepository(ProjectContext context) : base(context)
        {
        }
    }
}