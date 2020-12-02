using System;
using System.Collections.Generic;
using PG.SharedKernel.Model;

namespace PG.Management.Core.Domain.Consultation.Models
{
    public class ProjectManager : Entity<Guid>
    {
        public string ProjectManagerName { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}