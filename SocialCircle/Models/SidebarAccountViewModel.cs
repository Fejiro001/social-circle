namespace SocialCircle.Models
{
    public class SidebarAccountViewModel
    {
        public string DisplayName { get; set; } = "My Account";
        public string Handle { get; set; } = "@user";
        public string? ProfilePicUrl { get; set; }
        public string Initial { get; set; } = "A";
    }
}
