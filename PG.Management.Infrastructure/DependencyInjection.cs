using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PG.Management.Core.Domain.Consultation;
using PG.Management.Infrastructure.Persistence;

namespace PG.Management.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration, bool initDb = true)
        {
            if (initDb)
                services.AddDbContext<ProjectContext>(o => o.UseNpgsql(
                    configuration.GetConnectionString("DatabaseConnection"), x =>
                        x.MigrationsAssembly(typeof(ProjectContext).Assembly.FullName)));

            services.AddScoped<IProjectRepository, ProjectsRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IProjectManagerRepository, ProjectManagerRepository>();
            services.AddScoped<IProjectObjectiveRepository, ProjectObjectiveRepository>();
            return services;
        }
    }
}
