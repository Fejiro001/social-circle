using SocialCircle.DAL;
using SocialCircle.Models;

namespace SocialCircle.BLL
{
    public class UserService
    {
        private readonly UserRepo _repo;
        public UserService(UserRepo repo)
        {
            _repo = repo;
        }

        public List<User> GetAllUsers() => _repo.GetAllUsers();

        public User GetUserById(int userId) => _repo.GetUserById(userId);
    }
}
