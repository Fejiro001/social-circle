namespace SocialCircle.Models
{
    public class InboxViewModel
    {
        public int TargetUserId { get; set; }
        public string TargetUserName { get; set; } = string.Empty;
        public string TargetAvatar { get; set; } = string.Empty;
        public string LastMessageText { get; set; } = string.Empty;
        public DateTime LastMessageTimestamp { get; set; }
        public bool IsUnread { get; set; }
    }
}
