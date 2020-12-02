using System;
using PG.Management.Core.Domain.Consultation;
using PG.Management.Core.Domain.Consultation.Models;
using PG.SharedKernel.Infrastructure.Persistence;

namespace PG.Management.Infrastructure.Persistence
{
    public class ProjectObjectiveRepository : BaseRepository<ProjectObjective, Guid>, IProjectObjectiveRepository
    {
        public ProjectObjectiveRepository(ProjectContext context) : base(context)
        {
            
        }
    }
}