using System;
using Microsoft.EntityFrameworkCore;
using PG.Management.Core.Domain.Consultation;
using PG.Management.Core.Domain.Consultation.Models;
using PG.SharedKernel.Infrastructure.Persistence;

namespace PG.Management.Infrastructure.Persistence
{
    public class ProjectsRepository : BaseRepository<Project, Guid>, IProjectRepository
    {
        public ProjectsRepository(ProjectContext context) : base(context)
        {
        }
    }
}