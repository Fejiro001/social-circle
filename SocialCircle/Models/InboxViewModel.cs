namespace SocialCircle.Models
{
    public class InboxViewModel
    {
        public int TargetUserId { get; set; }
        public required string TargetUserName { get; set; }
        public string TargetAvatar { get; set; }
        public string LastMessageText { get; set; }
        public DateTime LastMessageTimestamp { get; set; }
        public bool IsUnread { get; set; }
    }
}
