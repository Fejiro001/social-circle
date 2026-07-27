using SocialCircle.Models;

namespace SocialCircle.DAL
{
    public class StoryViewRepo
    {
        private readonly SocialCircleDbContext _context;

        public StoryViewRepo(SocialCircleDbContext context)
        {
            _context = context;
        }

        public void InsertStoryView(StoryView view)
        {
            _context.StoryViews.Add(view);
            _context.SaveChanges();
        }

        public List<StoryView> FetchStoryViewers(int storyId)
        {
            return _context.StoryViews.Where(v => v.StoryId == storyId).ToList();
        }
    }
}
