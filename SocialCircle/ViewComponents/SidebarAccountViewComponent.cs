using Microsoft.AspNetCore.Mvc;
using SocialCircle.BLL;
using SocialCircle.Helpers;
using SocialCircle.Models;

namespace SocialCircle.ViewComponents
{
    // Used a ViewComponent instead of a Controller + Partial View.
    // The Sidebar requires its own data fetching logic and model,
    // to display the user in the sidebar
    // but is rendered as a reusable, self-contained UI component.
    public class SidebarAccountViewComponent : ViewComponent
    {
        private readonly UserService _userService;
        public SidebarAccountViewComponent(UserService userService)
        {
            _userService = userService;
        }

        public IViewComponentResult Invoke()
        {
            int currentUserId = CurrentUser.Id;
            var user = _userService.GetUserById(currentUserId);

            var vm = new SidebarAccountViewModel
            {
                UserId = user.UserId,
                DisplayName = user.UserName,
                Handle = $"@{user.UserName}",
                ProfilePicUrl = user.ProfilePicUrl,
                Initial = user.UserName.Substring(0, 1).ToUpper()
            };

            return View(vm);
        }
    }
}
