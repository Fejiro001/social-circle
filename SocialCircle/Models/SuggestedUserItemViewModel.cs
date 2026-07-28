namespace SocialCircle.Models
{
    public class SuggestedUserItemViewModel
    {
        public int UserId { get; set; }
        public required string UserName { get; set; }
        public string? Bio { get; set; } = "Anonymous";
        public string? ProfilePicUrl { get; set; }
        public bool IsFollowing { get; set; }
    }
}
