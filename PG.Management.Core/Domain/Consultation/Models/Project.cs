using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using PG.SharedKernel.Model;

namespace PG.Management.Core.Domain.Consultation.Models
{
    public class Project : Entity<Guid>
    {
        public string Title { get; set; }
        public int Budget { get; set; }

        public Guid CountryId { get; set; }
        public Guid ProjectManagerId { get; set; }

        public ICollection<ProjectObjective> ProjectObjectives { get; set; } = new List<ProjectObjective>();
    }
}
