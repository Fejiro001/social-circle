namespace SocialCircle.Models;

public partial class Story
{
    public int StoryId { get; set; }

    public string StoryContent { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public DateTime? ExpirationTime { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<StoryView> StoryViews { get; set; } = new List<StoryView>();

    public virtual User User { get; set; } = null!;
}
