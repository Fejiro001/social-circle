namespace SocialCircle.Models;

public partial class PostLike
{
    public int UserId { get; set; }

    public int PostId { get; set; }

    public DateTime LikeDateTime { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
