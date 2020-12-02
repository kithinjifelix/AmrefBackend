using PG.SharedKernel.Model;
using System.Collections.Generic;

namespace PG.Management.Core.Domain.Consultation.Models
{
    public class ProjectObjective : Entity<System.Guid>
    {
        public string Name { get; set; }
        public System.Guid ProjectId { get; set; }
        public ICollection<KeyPerfomanceIndicator> KeyPerfomanceIndicators { get; set; } = new List<KeyPerfomanceIndicator>();
    }
}