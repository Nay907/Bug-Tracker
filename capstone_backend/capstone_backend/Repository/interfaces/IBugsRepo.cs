using capstone_backend.Models;

namespace capstone_backend.Repository.interfaces
{
    public interface IBugsRepo
    {
        /*IEnumerable<Bugs> GetAllBugs();
        Bugs GetBugById(Bugs bug);
        Bugs CreateBug(Bugs bug);*/
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
