namespace SocialCircle.Models
{
    public class ChatHistoryViewModel
    {
        public int CurrentUserId { get; set; }
        public int TargetUserId { get; set; }
        public required string TargetUserName { get; set; }
        public string TargetAvatar { get; set; }
        public List<DirectMessage> Messages { get; set; } = new List<DirectMessage>();
    }
}
