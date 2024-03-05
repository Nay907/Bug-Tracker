using capstone_backend.Models;

namespace capstone_backend.Repository.interfaces
{
    public interface IProjectRepo
    {
        Task<IEnumerable<Project>> GetAllProjects();
        Task<Project> GetProjectById(int id);
        Task<Project> AddProject(Project project);
        Task<Project> UpdateProject(Project project);
        Task DeleteProject(int id);
    }
}
