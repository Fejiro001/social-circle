using System.ComponentModel.DataAnnotations;

namespace SocialCircle.Models
{
    public class CreateCommentViewModel
    {
        public int PostId { get; set; }

        [Required]
        public string CommentText { get; set; }
    }
}