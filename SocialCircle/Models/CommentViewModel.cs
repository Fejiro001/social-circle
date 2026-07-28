namespace SocialCircle.Models
{
    public class CommentViewModel
    {
        public int CommentId { get; set; }

        public string CommentText { get; set; }

        public string AuthorName { get; set; }

        public DateTime Timestamp { get; set; }
    }
}