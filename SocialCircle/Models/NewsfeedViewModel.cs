using System.ComponentModel.DataAnnotations;

namespace SocialCircle.Models
{
    public class NewsfeedViewModel
    {
        public List<FeedPostViewModel> Posts { get; set; } = new List<FeedPostViewModel>();
        public required User CurrentUser { get; set; }
        [Required(ErrorMessage = "Post cannot be empty.")]
        public string PostText { get; set; }
    }
}
