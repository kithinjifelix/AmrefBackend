using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PG.Management.Core.Domain.Consultation.Models;

namespace PG.Management.Core.Domain.Consultation.Services
{
    public interface IProjectService
    {
        Task<(bool IsSuccess, IEnumerable<Project> projects, string ErrorMessage)> LoadProjects();
        (bool IsSuccess, Project project, string ErrorMessage) GetProject(Guid id);
        Task<(bool IsSuccess, Guid id, string ErrorMessage)> DeleteProject(Guid id);
        Task<(bool IsSuccess, Project project, string ErrorMEssage)> AddProject(Project project);
        Task<(bool IsSuccess, IEnumerable<Project> projects, string ErrorMEssage)> GetProjectsByProjectManager(Guid projectManagerId);
    }
}