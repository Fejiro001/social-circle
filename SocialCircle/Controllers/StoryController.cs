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

        public IActionResult Index()
        {
            var activeStories = _storyService.GetActiveStories();
            var viewModelList = activeStories.Select(s => new StoryViewModel
            {
                StoryId = s.StoryId,
                StoryContent = s.StoryContent,
                Timestamp = s.Timestamp,
                AuthorName = "User_" + s.UserId,
                AuthorAvatar = $"{s.UserId}" //can be adjusted
            }).ToList();

            return View(viewModelList); 
        }
        public IActionResult ViewStory(int storyId)
        {
            var story = _storyService.LogStoryView(storyId, CurrentUserMockID);
            if (story == null) return NotFound("The story time has expired.");

            _storyViewService.RecordStoryView(storyId, CurrentUserMockID);

            var model = new StoryViewModel
            {
                StoryId = story.StoryId,
                StoryContent = story.StoryContent,
                Timestamp = story.Timestamp,
                AuthorName = "User_" + story.UserId,
                AuthorAvatar = $"{story.UserId}" //can be adjusted
            };

            ViewBag.TotalViews = _storyViewService.GetTotalViewCount(storyId);

            return View(model);
        }

    }
}
