using SocialCircle.DAL;
using SocialCircle.Models;

namespace SocialCircle.BLL
{
    public class StoryService
    {
        private readonly StoryRepo _storyRepo;
        private readonly StoryViewService _storyViewService;
        public StoryService(StoryRepo storyRepo, StoryViewService storyViewService)
        {
            _storyRepo = storyRepo;
            _storyViewService = storyViewService;
        }

        public void CreateStory(int userId, string content)
        {
            var now = DateTime.Now;
            var story = new Story
            {
                UserId = userId,
                StoryContent = content,
                Timestamp = now,
                ExpirationTime = now.AddHours(24)
            };
            _storyRepo.AddStory(story);
        }

        public List<Story> GetActiveStories()
        {
            return _storyRepo.FetchActiveStories()
                .OrderByDescending(s => s.Timestamp)
                .ToList();
        }

        public Story? LogStoryView(int storyId, int currentUserId)
        {
            var activeStories = GetActiveStories();
            var targetStory = activeStories.FirstOrDefault(s => s.StoryId == storyId);
            if (targetStory == null) return null;

            var viewRecord = new StoryView
            {
                StoryId = storyId,
                UserId = currentUserId,
                ViewDateTime = DateTime.Now
            };
            _storyViewService.RecordStoryView(storyId, currentUserId);

            return targetStory;
        }
    }
}
