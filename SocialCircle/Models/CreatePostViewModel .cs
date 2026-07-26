using System.ComponentModel.DataAnnotations;

namespace SocialCircle.Models
{
    public class CreatePostViewModel
    {
        [Required(ErrorMessage = "You can't post an empty message!")]
        public string PostText { get; set; }
    }
}
