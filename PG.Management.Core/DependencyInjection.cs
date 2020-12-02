using System.Collections.Generic;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PG.Management.Core.Application.Consultation.Commands;
using PG.Management.Core.Application.Consultation.Services;
using PG.Management.Core.Domain.Consultation.Services;

namespace PG.Management.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, List<Assembly> others = null)
        {
            // Add scoped resources
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IProjectManagerService, ProjectManagerService>();
            services.AddScoped<IProjectObjectiveService, ProjectObjectiveService>();

            if (null != others)
            {
                others.Add(typeof(SampleCommandHandler).Assembly);
                services.AddMediatR(others.ToArray());
            }
            else
            {
                services.AddMediatR(typeof(SampleCommandHandler).Assembly);
            }

            return services;
        }
    }
}
