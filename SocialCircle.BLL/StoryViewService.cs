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

  
    }
}
