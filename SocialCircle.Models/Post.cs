namespace SocialCircle.Models;

public partial class Post
{
    public int PostId { get; set; }

    public string PostText { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<PostLike> PostLikes { get; set; } = new List<PostLike>();

    public virtual User User { get; set; } = null!;
}
