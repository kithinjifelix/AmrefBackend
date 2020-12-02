using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PG.Management.Core.Domain.Consultation.Models;

namespace PG.Management.Core.Domain.Consultation.Services
{
    public interface IProjectManagerService
    {
        Task<(bool IsSuccess, IEnumerable<ProjectManager> projectManagers, string ErrorMessage)> LoadProjectManagers();
        (bool IsSuccess, ProjectManager projectManager, string ErrorMessage) GetProjectManager(Guid id);
    }
}