using PG.SharedKernel.Model;
using System;

namespace PG.Management.Core.Domain.Consultation.Models
{
    public class KeyPerfomanceIndicator : Entity<Guid>
    {
        public string Name { get; set; }
        public Guid ProjectObjectiveId { get; set; }
    }
}