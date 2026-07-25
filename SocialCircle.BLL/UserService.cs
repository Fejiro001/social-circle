using SocialCircle.DAL;
using SocialCircle.Models;

namespace SocialCircle.BLL
{
    public class UserService
    {
        private readonly UserRepo _userRepo;
        public UserService(UserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public List<User> GetAllUsers() => _userRepo.GetAllUsers();

        public User GetUserById(int userId) => _userRepo.GetUserById(userId);
    }
}
