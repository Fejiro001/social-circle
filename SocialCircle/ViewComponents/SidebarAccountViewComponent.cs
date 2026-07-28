using Microsoft.AspNetCore.Mvc;
using SocialCircle.BLL;
using SocialCircle.Helpers;
using SocialCircle.Models;

namespace SocialCircle.ViewComponents
{
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
