using Microsoft.AspNetCore.Mvc;
using SocialCircle.BLL;
using SocialCircle.Models;

namespace SocialCircle.ViewComponents
{
    // Used a ViewComponent instead of a Controller + Partial View.
    // The Story Feed requires its own data fetching logic and model, 
    // but is rendered as a reusable, self-contained UI component.
    public class StoryFeedViewComponent : ViewComponent
    {
        private readonly StoryService _storyService;

        public StoryFeedViewComponent(StoryService storyService)
        {
            _storyService = storyService;
        }

        public IViewComponentResult Invoke()
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
    }
}
