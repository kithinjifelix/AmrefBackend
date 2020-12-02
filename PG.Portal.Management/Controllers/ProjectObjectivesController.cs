using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PG.Management.Core.Domain.Consultation.Models;
using PG.Management.Core.Domain.Consultation.Services;
using Serilog;

namespace PG.Portal.Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectObjectivesController : ControllerBase
    {
        private readonly IProjectObjectiveService _projectObjectiveService;

        public ProjectObjectivesController(IProjectObjectiveService projectObjectiveService)
        {
            _projectObjectiveService = projectObjectiveService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _projectObjectiveService.LoadProjectObjectives();
                if (results.IsSuccess)
                    return Ok(results.projectObjectives);
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
                var results = _projectObjectiveService.GetProjectObjective(id);
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

        [HttpGet("getProjectObjectives/{projectId}")]
        public async Task<IActionResult> GetProjectObjectives(Guid projectId)
        {
            try
            {
                var results = await _projectObjectiveService.GetProjectObjectives(projectId);
                if (results.IsSuccess)
                    return Ok(results.projectObjectives);
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
        public async Task<IActionResult> AddProjectObjectives([FromBody] ProjectObjective projectObjective)
        {
            try
            {
                var results = await _projectObjectiveService.AddProjectObjective(projectObjective);
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
                var results = await _projectObjectiveService.DeleteProjectObjective(id);
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
