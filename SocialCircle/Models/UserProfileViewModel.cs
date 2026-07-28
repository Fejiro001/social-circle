namespace SocialCircle.Models
{
    public class UserProfileViewModel
    {
        public User User { get; set; }
        public List<FeedPostViewModel>? Posts { get; set; }
        public int FollowersCount { get; set; } = 0;
        public int FollowingCount { get; set; } = 0;
        public bool IsCurrentlyFollowing { get; set; }
        public bool IsSelf { get; set; }
    }
}
