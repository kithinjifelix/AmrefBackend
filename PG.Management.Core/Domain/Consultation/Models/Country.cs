using System;
using System.Collections.Generic;
using PG.SharedKernel.Model;

namespace PG.Management.Core.Domain.Consultation.Models
{
    public class Country : Entity<Guid>
    {
        public string CountryName { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}