using capstone_backend.Models;

namespace capstone_backend.Services.Interfaces
{
    public interface IProjectService
    {
        Task<List<Project>> GetAllProjects();
        Task<Project> GetProjectById(int id);
        Task<Project> AddProject(Project project);
        Task<Project> UpdateProject(Project project);
        Task DeleteProject(int id);
    }
}
