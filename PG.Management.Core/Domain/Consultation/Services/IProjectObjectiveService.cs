using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PG.Management.Core.Domain.Consultation.Models;

namespace PG.Management.Core.Domain.Consultation.Services
{
    public interface IProjectObjectiveService
    {
        Task<(bool IsSuccess, IEnumerable<ProjectObjective> projectObjectives, string ErrorMessage)> LoadProjectObjectives();
        (bool IsSuccess, ProjectObjective project, string ErrorMessage) GetProjectObjective(Guid id);
        Task<(bool IsSuccess, Guid id, string ErrorMessage)> DeleteProjectObjective(Guid id);
        Task<(bool IsSuccess, ProjectObjective project, string ErrorMEssage)> AddProjectObjective(ProjectObjective projectObjective);
        Task<(bool IsSuccess, IEnumerable<ProjectObjective> projectObjectives, string ErrorMEssage)> GetProjectObjectives(Guid projectId);
    }
}