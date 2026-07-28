namespace SocialCircle.Models
{
    public class PostInteractionsViewModel
    {
        public int TotalLikes { get; set; }

        public bool HasCurrentUserLiked { get; set; }

        public List<CommentViewModel> Comments { get; set; }
            = new List<CommentViewModel>();
    }
}