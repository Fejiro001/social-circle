using Microsoft.AspNetCore.Mvc;
using SocialCircle.BLL;
using SocialCircle.Helpers;
using SocialCircle.Models;

namespace SocialCircle.Controllers
{
    public class StoryController : Controller
    {
        private readonly StoryService _storyService;
        private readonly StoryViewService _storyViewService;

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
                AuthorName = s.User.UserName,
                AuthorAvatar = s.User?.ProfilePicUrl
            }).ToList();

            return View(viewModelList); 
        }
        public IActionResult ViewStory(int storyId)
        {
            int currentUserId = CurrentUser.Id;
            var story = _storyService.LogStoryView(storyId, currentUserId);
            if (story == null) return NotFound("The story time has expired or does not exist.");

            var model = new StoryViewModel
            {
                StoryId = story.StoryId,
                StoryContent = story.StoryContent,
                Timestamp = story.Timestamp,
                AuthorName = story.User.UserName,
                AuthorAvatar = story.User.ProfilePicUrl,
            };

            ViewBag.TotalViews = _storyViewService.GetTotalViewCount(storyId);

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UploadStory(string storyContent)
        {
            int currentUserId = CurrentUser.Id;

            if (string.IsNullOrWhiteSpace(storyContent))
            {
                TempData["ErrorMessage"] = "Story text content cannot be blank.";
                return RedirectToAction("Index");
            }

            _storyService.CreateStory(currentUserId, storyContent);

            TempData["SuccessMessage"] = "Story successfully posted!";
            return RedirectToAction("Index");
        }
    }
}
