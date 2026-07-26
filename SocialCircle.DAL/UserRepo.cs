using SocialCircle.Models;

namespace SocialCircle.DAL
{
    public class UserRepo
    {
        private readonly SocialCircleDbContext _context;
        public UserRepo(SocialCircleDbContext context)
        {
            _context = context;
        }

        public List<User> GetAllUsers() => _context.Users.ToList();

        public User GetUserById(int userId) => _context.Users.Find(userId);
    }
}
