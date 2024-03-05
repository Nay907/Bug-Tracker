using capstone_backend.Models;

namespace capstone_backend.Services.Interfaces
{
    public interface IBugService
    {
        /*List<Bugs> GetBugs();
        Bugs GetBugsById(int BugsId);
        Bugs CreateBugs(int BugsId);*/
        Task<IEnumerable<Bugs>> GetAllBugsAsync();
        Task<List<Bugs>> GetBugByIdAsync(int id);
        Task<Bugs> AddBugAsync(Bugs bugs);
        Task<Bugs> UpdateBugAsync(Bugs bugs);
        Task DeleteBugAsync(int id);
        Task<int> GetTotalBugCount();
        Task<int> GetBugCountBySeverity(string severity);
    Task<int> GetBugCountByLowSeverity();
    Task<int> GetBugCountByMediumSeverity();
    Task<int> GetBugCountByHighSeverity();
  }
}
