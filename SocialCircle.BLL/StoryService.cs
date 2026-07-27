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


    }
}
