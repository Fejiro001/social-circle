namespace SocialCircle.Models
{
    public class NewsfeedViewModel
    {
        public List<FeedPostViewModel> Posts { get; set; } = new();
        public User CurrentUser { get; set; }
    }
}
