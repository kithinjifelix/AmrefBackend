using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PG.Management.Core.Domain.Consultation.Models;
using PG.SharedKernel.Infrastructure.Persistence;
using PG.SharedKernel.Utility;

namespace PG.Management.Infrastructure.Persistence
{
    public class ProjectContext : BaseContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<KeyPerfomanceIndicator> KeyPerfomanceIndicators { get; set; }
        public DbSet<ProjectManager> ProjectManagers { get; set; }
        public DbSet<ProjectObjective> ProjectObjectives { get; set; }

        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {
        }

        public override void EnsureSeeded()
        {
            /* add seeding data here */

            if (!Countries.Any())
            {
                var data = SeedDataReader.ReadCsv<Country>(typeof(ProjectContext).Assembly);
                AddRange(data);
            }

            if (!ProjectManagers.Any())
            {
                var data = SeedDataReader.ReadCsv<ProjectManager>(typeof(ProjectContext).Assembly);
                AddRange(data);
            }

            SaveChanges();
        }
    }
}