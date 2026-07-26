using System.ComponentModel.DataAnnotations;

namespace SocialCircle.Models
{
    public class SendMessageViewModel
    {
        [Required]
        public int ReceiverId { get; set; }

        [Required(ErrorMessage = "Cannot send an empty message.")]
        [StringLength(1000, ErrorMessage = "Message must not exceed 1000 characters.")]
        public string MessageText { get; set; } = string.Empty;
    }
}
