using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PG.Management.Core.Domain.Consultation;
using PG.Management.Core.Domain.Consultation.Models;
using PG.Management.Core.Domain.Consultation.Services;

namespace PG.Management.Core.Application.Consultation.Services
{
    public class ProjectManagerService : IProjectManagerService
    {
        public readonly IProjectManagerRepository _projectManagerRepository;

        public ProjectManagerService(IProjectManagerRepository projectManagerRepository)
        {
            _projectManagerRepository = projectManagerRepository;
        }

        public async Task<(bool IsSuccess, IEnumerable<ProjectManager> projectManagers, string ErrorMessage)> LoadProjectManagers()
        {
            try
            {
                var projectManagers = await _projectManagerRepository.GetAll().ToListAsync();
                if (projectManagers == null)
                    return (false, new List<ProjectManager>(), "No record found.");
                return (true, projectManagers, "Project Managers loaded successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        (bool IsSuccess, ProjectManager projectManager, string ErrorMessage) IProjectManagerService.GetProjectManager(Guid id)
        {
            try
            {
                var projectManager = _projectManagerRepository.GetById(id);
                if (projectManager == null)
                    return (false, null, "Project Manager not found.");
                return (true, projectManager, "Project Manager loaded successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return (false, null, $"{e.Message}");
            }
        }
    }
}