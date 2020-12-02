using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PG.Management.Core.Domain.Consultation.Models;
using PG.Management.Core.Domain.Consultation.Services;
using Serilog;

namespace PG.Portal.Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _projectService.LoadProjects();
                if (results.IsSuccess)
                    return Ok(results.projects);
                return NotFound(results);
            }
            catch (Exception e)
            {
                var msg = $"Error loading ";
                Log.Error(e, msg);
                return StatusCode(500, $"{msg} {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var results = _projectService.GetProject(id);
                if (results.IsSuccess)
                    return Ok(results.project);
                return NotFound(results);
            }
            catch (Exception e)
            {
                var msg = $"Error loading ";
                Log.Error(e, msg);
                return StatusCode(500, $"{msg} {e.Message}");
            }
        }

        [HttpGet("getProjectsByProjectManager/{projectManagerId}")]
        public async Task<IActionResult> GetProjectsByProjectManager(Guid projectManagerId)
        {
            try
            {
                var results = await _projectService.GetProjectsByProjectManager(projectManagerId);
                if (results.IsSuccess)
                    return Ok(results.projects);
                return NotFound(results);
            }
            catch (Exception e)
            {
                var msg = $"Error loading ";
                Log.Error(e, msg);
                return StatusCode(500, $"{msg} {e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProject([FromBody] Project project)
        {
            try
            {
                var results = await _projectService.AddProject(project);
                if (results.IsSuccess)
                    return Ok(results.project);
                return NotFound(results.ErrorMEssage);
            }
            catch (Exception e)
            {
                var msg = $"Error saving ";
                Log.Error(e, msg);
                return StatusCode(500, $"{msg} {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var results = await _projectService.DeleteProject(id);
                if (results.IsSuccess)
                    return Ok(results);
                return NotFound(results);
            }
            catch (Exception e)
            {
                var msg = $"Error deleting ";
                Log.Error(e, msg);
                return StatusCode(500, $"{msg} {e.Message}");
            }
        }
    }
}