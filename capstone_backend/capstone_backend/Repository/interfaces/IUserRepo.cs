using capstone_backend.Models;

namespace capstone_backend.Repository.interfaces
{
    public interface IUserRepo
    {
        List<User> GetAllUsers();
        User GetUserById(int id);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
        string LoginUser(User user);
    }
}
