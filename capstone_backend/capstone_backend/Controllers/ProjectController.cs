using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using capstone_backend.Models;
using capstone_backend.Services.Interfaces;

namespace capstone_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Project>>> GetAllProjects()
        {
            var projects = await _projectService.GetAllProjects();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public ActionResult<Project> GetProjectById(int id)
        {
            var project = _projectService.GetProjectById(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        [HttpPost("/addProject")]
         public IActionResult AddProject([FromBody]Project project)
         {
             _projectService.AddProject(project);
             return Ok(new { message = "Project Created!" });
         }

         /*[HttpPost]
        public IActionResult AddProject([FromBody]Project project)
        {
            _projectService.AddProject(project);
            return Ok("Project Created!");
        }*/

        [HttpPut("{id}")]
        public IActionResult UpdateProject(int id, Project project)
        {
            if (id != project.ProjectId)
            {
                return BadRequest();
            }
            _projectService.UpdateProject(project);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProject(int id)
        {
            _projectService.DeleteProject(id);
            return NoContent();
        }
    }
}
