using capstone_backend.Models;
using capstone_backend.Repository.interfaces;
using capstone_backend.Services.Interfaces;

namespace capstone_backend.Services
{
    public class UserService : IUserService
    {
        private IUserRepo _userRepo;

        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public List<User> GetAllUsers()
        {
            return _userRepo.GetAllUsers();
        }

        public User GetUserById(int id)
        {
            return _userRepo.GetUserById(id);
        }

        public void AddUser(User user)
        {
            _userRepo.AddUser(user);
        }

        public void UpdateUser(User user)
        {
            _userRepo.UpdateUser(user);
        }

        public void DeleteUser(int id)
        {
            _userRepo.DeleteUser(id);
        }

        public string LoginUser(User user)
        {
            return _userRepo.LoginUser(user);
        }
    }
}
