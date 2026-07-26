using SocialCircle.Models;

namespace SocialCircle.BLL.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) used to package a user's profile information, 
    /// their posts, follow counts, and follow status from the BLL to the UI layer.
    /// </summary>
    public class UserProfileDto
    {
        public User User { get; set; }
        public List<Post> Posts { get; set; }
        public int FollowersCount { get; set; } = 0;
        public int FollowingCount { get; set; } = 0;
        public bool IsCurrentlyFollowing { get; set; }
    }
}
