using Microsoft.EntityFrameworkCore;
using SocialCircle.Models;

namespace SocialCircle.DAL
{
    public class StoryRepo
    {
        private readonly SocialCircleDbContext _context;

        public StoryRepo(SocialCircleDbContext context)
        {
            _context = context;
        }

        public void AddStory(Story story)
        {
            _context.Stories.Add(story);
            _context.SaveChanges();
        }

        public List<Story> FetchActiveStories()
        {
            return _context.Stories
                .Include(s => s.User)
                .Where(s => s.ExpirationTime == null || s.ExpirationTime > DateTime.Now)
                .ToList();
        }
    }
}
