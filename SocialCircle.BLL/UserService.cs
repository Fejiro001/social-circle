using SocialCircle.BLL.DTOs;
using SocialCircle.DAL;
using SocialCircle.Models;

namespace SocialCircle.BLL
{
    public class UserService
    {
        private readonly UserRepo _userRepo;
        private readonly PostRepo _postRepo;
        private readonly UserFollowRepo _followRepo;
        public UserService(UserRepo userRepo, PostRepo postRepo, UserFollowRepo followRepo)
        {
            _userRepo = userRepo;
            _postRepo = postRepo;
            _followRepo = followRepo;
        }

        public List<User> GetAllUsers() => _userRepo.GetAllUsers();

        public User GetUserById(int userId) => _userRepo.GetUserById(userId);

        // Combines and returns all necessary data required to render a user's profile page.
        public UserProfileDto GetProfileData(int targetUserId, int currentUserId)
        {
            User user = _userRepo.GetUserById(targetUserId);
            if (user == null) return null;

            return new UserProfileDto
            {
                User = user,
                Posts = _postRepo.GetUsersPosts(targetUserId),
                FollowersCount = _followRepo.GetFollowers(targetUserId).Count,
                FollowingCount = _followRepo.GetFollowing(targetUserId).Count,
                IsCurrentlyFollowing = _followRepo.IsFollowing(currentUserId, targetUserId)
            };
        }

        // Retrieves a curated list of suggested users for the currently logged-in user to follow.
        // Filters out the current user and anyone they already follow.
        public List<User> GetSuggestedUsers(int currentUserId)
        {
            List<User> allUsers = GetAllUsers();
            List<UserFollow> followingList = _followRepo.GetFollowing(currentUserId);
            List<int> followingIds = followingList.Select(f => f.FollowingId).ToList();

            return allUsers
                    .Where(u => u.UserId != currentUserId && !followingIds.Contains(u.UserId))
                    .Take(5)
                    .ToList();
        }
    }
}
