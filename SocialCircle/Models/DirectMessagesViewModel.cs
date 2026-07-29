namespace SocialCircle.Models
{
    public class DirectMessagesViewModel
    {
        public IEnumerable<InboxViewModel> Inbox { get; set; }
        public ChatHistoryViewModel ActiveChat { get; set; }
    }
}
