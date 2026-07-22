namespace SocialCircle.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public string CommentText { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public int UserId { get; set; }

    public int PostId { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
