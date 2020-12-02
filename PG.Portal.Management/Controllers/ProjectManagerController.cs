using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PG.Management.Core.Domain.Consultation.Services;
using Serilog;

namespace PG.Portal.Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectManagerController : ControllerBase
    {
        private readonly IProjectManagerService _projectManagerService;

        public ProjectManagerController(IProjectManagerService projectManagerService)
        {
            _projectManagerService = projectManagerService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _projectManagerService.LoadProjectManagers();
                if (results.IsSuccess)
                    return Ok(results.projectManagers);
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
                var results = _projectManagerService.GetProjectManager(id);
                if (results.IsSuccess)
                    return Ok(results.projectManager);
                return NotFound(results);
            }
            catch (Exception e)
            {
                var msg = $"Error loading ";
                Log.Error(e, msg);
                return StatusCode(500, $"{msg} {e.Message}");
            }
        }
    }
}
