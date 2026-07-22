namespace SocialCircle.Models;

public partial class StoryView
{
    public int UserId { get; set; }

    public int StoryId { get; set; }

    public DateTime ViewDateTime { get; set; }

    public virtual Story Story { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
