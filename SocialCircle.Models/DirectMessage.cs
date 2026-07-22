namespace SocialCircle.Models;

public partial class DirectMessage
{
    public int MessageId { get; set; }

    public string MessageText { get; set; } = null!;

    public bool IsRead { get; set; }

    public DateTime Timestamp { get; set; }

    public int SenderId { get; set; }

    public int ReceiverId { get; set; }

    public virtual User Receiver { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;
}
