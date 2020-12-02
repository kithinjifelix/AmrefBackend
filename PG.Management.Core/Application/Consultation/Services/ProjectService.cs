using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PG.Management.Core.Domain.Consultation;
using PG.Management.Core.Domain.Consultation.Models;
using PG.Management.Core.Domain.Consultation.Services;
using PG.SharedKernel.Custom;

namespace PG.Management.Core.Application.Consultation.Services
{
    public class ProjectService : IProjectService
    {
        public readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<(bool IsSuccess, IEnumerable<Project> projects, string ErrorMessage)> LoadProjects()
        {
            try
            {
                var projects = await _projectRepository.GetAll().ToListAsync();
                if (projects == null)
                    return (false, new List<Project>(), "No record found.");
                return (true, projects: projects, "Projects loaded successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public (bool IsSuccess, Project project, string ErrorMessage) GetProject(Guid id)
        {
            try
            {
                var project = _projectRepository.GetById(id);
                if (project == null)
                    return (false, null, "Project not found.");
                return (true, project, "Project loaded successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return (false, null, $"{e.Message}");
            }
        }

        public async Task<(bool IsSuccess, Guid id, string ErrorMessage)> DeleteProject(Guid id)
        {
            try
            {

                var project = await _projectRepository.GetAll(x => x.Id == id).FirstOrDefaultAsync();
                if (project == null)
                    return (false, id, "No record found.");
                _projectRepository.Delete(project);

                return (false, id, "Record deleted successfully.");
            }
            catch (Exception e)
            {
                return (false, id, $"{e.Message}");
            }
        }

        public async Task<(bool IsSuccess, Project project, string ErrorMEssage)> AddProject(Project project)
        {
            try
            {
                if (project == null)
                    return (false, project, "No project found");
                if (project.Id.IsNullOrEmpty())
                {
                    _projectRepository.Create(project);
                }
                else
                {
                    _projectRepository.Update(project);
                }
                await _projectRepository.Save();

                return (true, project, "Project created successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Project> projects, string ErrorMEssage)> GetProjectsByProjectManager(Guid projectManagerId)
        {
            try
            {
                var projects = await _projectRepository.GetAll(x => x.ProjectManagerId == projectManagerId).ToListAsync();
                return (true, projects, "Projects loaded successfully.");
            }
            catch (Exception e)
            {
                return (false, new List<Project>(), $"{e.Message}");
            }
        }
    }
}