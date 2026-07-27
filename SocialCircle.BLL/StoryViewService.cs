using SocialCircle.DAL;
using SocialCircle.Models;

namespace SocialCircle.BLL
{
    public class StoryViewService
    {
        private readonly StoryViewRepo _storyViewRepo;

        public StoryViewService(StoryViewRepo storyViewRepo)
        {
            _storyViewRepo = storyViewRepo;
        }

        public void RecordStoryView(int storyId, int userId)
        {
            // Verify if this user has already logged a view for this specific story
            var existingViews = _storyViewRepo.FetchStoryViewers(storyId);
            bool alreadyViewed = existingViews.Any(v => v.UserId == userId);

            if (!alreadyViewed)
            {
                var newView = new StoryView
                {
                    StoryId = storyId,
                    UserId = userId,
                    ViewDateTime = DateTime.Now
                };

                _storyViewRepo.InsertStoryView(newView);
            }
        }

        // Fetches total view list for a story 
        public List<StoryView> GetStoryViewersList(int storyId)
        {
            return _storyViewRepo.FetchStoryViewers(storyId)
                .OrderByDescending(v => v.ViewDateTime)
                .ToList();
        }

        // Counts overall distinct view interactions
        public int GetTotalViewCount(int storyId)
        {
            return _storyViewRepo.FetchStoryViewers(storyId).Count;
        }
    }
}
