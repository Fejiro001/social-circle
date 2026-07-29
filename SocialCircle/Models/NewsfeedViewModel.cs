namespace SocialCircle.Models
{
    public class NewsfeedViewModel
    {
        public List<FeedPostViewModel> Posts { get; set; } = new List<FeedPostViewModel>();
        public required User CurrentUser { get; set; }
    }
}
