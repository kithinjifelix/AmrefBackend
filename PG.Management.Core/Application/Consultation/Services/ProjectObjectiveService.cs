using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PG.Management.Core.Domain.Consultation;
using PG.Management.Core.Domain.Consultation.Models;
using PG.Management.Core.Domain.Consultation.Services;

namespace PG.Management.Core.Application.Consultation.Services
{
    public class ProjectObjectiveService : IProjectObjectiveService
    {
        public readonly IProjectObjectiveRepository _projectObjectiveRepository;

        public ProjectObjectiveService(IProjectObjectiveRepository projectObjectiveRepository)
        {
            _projectObjectiveRepository = projectObjectiveRepository;
        }

        public async Task<(bool IsSuccess, IEnumerable<ProjectObjective> projectObjectives, string ErrorMessage)> LoadProjectObjectives()
        {
            try
            {
                var projectObjectives = await _projectObjectiveRepository.GetAll().Include(z => z.KeyPerfomanceIndicators).ToListAsync();
                if (projectObjectives == null)
                    return (false, new List<ProjectObjective>(), "No record found.");
                return (true, projectObjectives, "Projects Objectives loaded successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public (bool IsSuccess, ProjectObjective project, string ErrorMessage) GetProjectObjective(Guid id)
        {
            try
            {
                var projectObjective = _projectObjectiveRepository.GetById(id);
                if (projectObjective == null)
                    return (false, null, "Project Objective not found.");
                return (true, projectObjective, "Project Objective loaded successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return (false, null, $"{e.Message}");
            }
        }

        public async Task<(bool IsSuccess, Guid id, string ErrorMessage)> DeleteProjectObjective(Guid id)
        {
            try
            {

                var project = await _projectObjectiveRepository.GetAll(x => x.Id == id).FirstOrDefaultAsync();
                if (project == null)
                    return (false, id, "No record found.");
                _projectObjectiveRepository.Delete(project);

                return (false, id, "Record deleted successfully.");
            }
            catch (Exception e)
            {
                return (false, id, $"{e.Message}");
            }
        }

        public async Task<(bool IsSuccess, ProjectObjective project, string ErrorMEssage)> AddProjectObjective(ProjectObjective projectObjective)
        {
            try
            {
                if (projectObjective == null)
                    return (false, projectObjective, "No project objective found");
                if (projectObjective.Id == Guid.Empty)
                {
                    _projectObjectiveRepository.Create(projectObjective);
                }
                else
                {
                    _projectObjectiveRepository.Update(projectObjective);
                }
                await _projectObjectiveRepository.Save();

                return (true, projectObjective, "Project created successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<ProjectObjective> projectObjectives, string ErrorMEssage)> GetProjectObjectives(Guid projectId)
        {
            try
            {
                var projectObjectives = await _projectObjectiveRepository.GetAll(y => y.ProjectId == projectId).Include(z => z.KeyPerfomanceIndicators).ToListAsync();
                if (projectObjectives == null)
                    return (false, new List<ProjectObjective>(), "No record found.");
                return (true, projectObjectives, "Projects Objectives loaded successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}