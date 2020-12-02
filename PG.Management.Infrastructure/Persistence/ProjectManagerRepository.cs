using System;
using Microsoft.EntityFrameworkCore;
using PG.Management.Core.Domain.Consultation;
using PG.Management.Core.Domain.Consultation.Models;
using PG.SharedKernel.Infrastructure.Persistence;

namespace PG.Management.Infrastructure.Persistence
{
    public class ProjectManagerRepository : BaseRepository<ProjectManager, Guid>, IProjectManagerRepository
    {
        public ProjectManagerRepository(ProjectContext context) : base(context)
        {
        }
    }
}