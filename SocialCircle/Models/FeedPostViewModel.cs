using System.ComponentModel.DataAnnotations;

namespace SocialCircle.Models
{
    public class FeedPostViewModel
    {
        public int PostId { get; set; }
        public string PostText { get; set; }
        public DateTime Timestamp { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string ProfilePicUrl { get; set; }
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
        public bool HasCurrentUserLiked { get; set; }
    }
}
