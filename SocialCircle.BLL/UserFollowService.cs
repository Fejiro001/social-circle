using SocialCircle.DAL;
using SocialCircle.Models;

namespace SocialCircle.BLL
{
    public class UserFollowService
    {
        private readonly UserFollowRepo _followRepo;
        public UserFollowService(UserFollowRepo followRepo)
        {
            _followRepo = followRepo;
        }

        public List<UserFollow> GetFollowers(int userId)
        {
            return _followRepo.GetFollowers(userId);
        }

        public List<UserFollow> GetFollowing(int userId)
        {
            return _followRepo.GetFollowing(userId);
        }

        public bool IsFollowing(int followerId, int followingId)
        {
            return _followRepo.IsFollowing(followerId, followingId);
        }

        public void FollowUser(UserFollow follow)
        {
            follow.FollowDateTime = DateTime.Now;

            _followRepo.AddFollow(follow);
            _followRepo.Save();
        }

        public void UnfollowUser(int followerId, int followingId)
        {
            _followRepo.RemoveFollow(followerId, followingId);
            _followRepo.Save();
        }
    }
}
