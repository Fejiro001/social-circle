namespace SocialCircle.Models
{
    public enum ListType
    {
        Following,
        Followers
    }
    public class FollowListViewModel
    {
        public User User { get; set; }
        public ListType ListType { get; set; }
        public List<FollowUserCardViewModel> Users { get; set; } = new List<FollowUserCardViewModel>();
    }
}
