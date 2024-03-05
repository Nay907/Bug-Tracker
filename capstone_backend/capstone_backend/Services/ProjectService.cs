using capstone_backend.Models;
using capstone_backend.Repository.interfaces;
using capstone_backend.Services.Interfaces;

namespace capstone_backend.Services
{
    public class ProjectService: IProjectService
    {
        private readonly IProjectRepo _projectRepo;

        public ProjectService(IProjectRepo projectRepo)
        {
            _projectRepo = projectRepo;
        }

        public async Task<List<Project>> GetAllProjects()
        {
            var projects = await _projectRepo.GetAllProjects();
            var projectsList = projects.ToList();
            return projectsList;
        }

        public async Task<Project> GetProjectById(int id)
        {
            return await _projectRepo.GetProjectById(id);
        }

        public async Task<Project> AddProject(Project project)
        {
            return await _projectRepo.AddProject(project);
        }

        public async Task<Project> UpdateProject(Project project)
        {
            return await _projectRepo.UpdateProject(project);
        }

        public async Task DeleteProject(int id)
        {
            await _projectRepo.DeleteProject(id);
        }

    }
}
