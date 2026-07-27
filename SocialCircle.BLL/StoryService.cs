using SocialCircle.DAL;
using SocialCircle.Models;

namespace SocialCircle.BLL
{
    public class StoryService
    {
        private readonly StoryRepo _storyRepo;
        private readonly StoryViewRepo _viewRepo;

        public StoryService(StoryRepo storyRepo, StoryViewRepo viewRepo)
        {
            _storyRepo = storyRepo;
            _viewRepo = viewRepo;
        }

        public void CreateStory(int userId, string content)
        {
            var story = new Story
            {
                UserId = userId,
                StoryContent = content,
                Timestamp = DateTime.Now
            };
            _storyRepo.AddStory(story);
        }

        public List<Story> GetActiveStories()
        {
            // Hide rows older than 24 hours using a computed database expiration check
            return _storyRepo.FetchActiveStories()
                .Where(s => s.ExpirationTime.HasValue && s.ExpirationTime.Value > DateTime.Now)
                .OrderByDescending(s => s.Timestamp)
                .ToList();
        }


    }
}
