using SocialCircle.Models;

namespace SocialCircle.DAL
{
    public class UserFollowRepo
    {
        private readonly SocialCircleDbContext _context;
        public UserFollowRepo(SocialCircleDbContext context)
        {
            _context = context;
        }

        // Get everyone who follows a specific user
        public List<UserFollow> GetFollowers(int userId)
        {
            return _context.UserFollows.Where(f => f.FollowingId == userId).ToList();
        }

        // Get everyone that a specific user is following
        public List<UserFollow> GetFollowing(int userId)
        {
            return _context.UserFollows.Where(f => f.FollowerId == userId).ToList();
        }

        // Checks if a following relationship exists
        public bool IsFollowing(int followerId, int followingId)
        {
            return _context.UserFollows.Any(f => f.FollowerId == followerId && f.FollowingId == followingId);
        }

        public void AddFollow(UserFollow follow)
        {
            _context.UserFollows.Add(follow);
        }

        public void RemoveFollow(int followerId, int followingId)
        {
            UserFollow follow = _context.UserFollows.FirstOrDefault(f => f.FollowerId == followerId && f.FollowingId == followingId);

            if (follow != null)
            {
                _context.UserFollows.Remove(follow);
            }
        }

        public void Save() => _context.SaveChanges();
    }
}
