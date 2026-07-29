namespace SocialCircle.Models
{
    public class FollowUserCardViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Bio { get; set; } = "Anonymous";
        public string ProfilePicUrl { get; set; }
        public bool IsFollowing { get; set; }
        public bool IsSelf { get; set; }
    }
}
