namespace SocialCircle.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Bio { get; set; }

    public string? ProfilePicUrl { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Location { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<DirectMessage> DirectMessageReceivers { get; set; } = new List<DirectMessage>();

    public virtual ICollection<DirectMessage> DirectMessageSenders { get; set; } = new List<DirectMessage>();

    public virtual ICollection<PostLike> PostLikes { get; set; } = new List<PostLike>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<Story> Stories { get; set; } = new List<Story>();

    public virtual ICollection<StoryView> StoryViews { get; set; } = new List<StoryView>();

    public virtual ICollection<UserFollow> UserFollowFollowers { get; set; } = new List<UserFollow>();

    public virtual ICollection<UserFollow> UserFollowFollowings { get; set; } = new List<UserFollow>();
}
