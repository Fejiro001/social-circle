namespace SocialCircle.Models
{
    public class StoryViewModel
    {
        public int StoryId { get; set; }
        public string StoryContent { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public string AuthorAvatar { get; set; } = string.Empty;
    }
}
