using Microsoft.AspNetCore.Mvc;
using SocialCircle.BLL;
using SocialCircle.Models;

namespace SocialCircle.Controllers
{
    public class StoryController : Controller
    {
        private readonly StoryService _storyService;
        private readonly StoryViewService _storyViewService;
        private const int CurrentUserMockID = 1;

        public StoryController(StoryService storyService, StoryViewService storyViewService)
        {
            _storyService = storyService;
            _storyViewService = storyViewService;
        }

       
    }
}
