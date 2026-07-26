namespace SocialCircle.Models
{
    public class ChatHistoryViewModel
    {
        public int CurrentUserId { get; set; }
        public int TargetUserId { get; set; }
        public string TargetUserName { get; set; } = string.Empty;
        public List<DirectMessage> Messages { get; set; } = new List<DirectMessage>();
    }
}
