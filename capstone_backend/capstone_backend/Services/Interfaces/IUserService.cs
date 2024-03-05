using capstone_backend.Models;

namespace capstone_backend.Services.Interfaces
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User GetUserById(int id);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
        string LoginUser(User user);
    }
}
