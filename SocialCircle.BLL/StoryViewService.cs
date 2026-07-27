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

       
    }
}
