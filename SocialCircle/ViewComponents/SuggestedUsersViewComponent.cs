using Microsoft.AspNetCore.Mvc;
using SocialCircle.BLL;
using SocialCircle.Helpers;
using SocialCircle.Models;

namespace SocialCircle.ViewComponents
{
    public class SuggestedUsersViewComponent : ViewComponent
    {
        private readonly UserService _userService;
        public SuggestedUsersViewComponent(UserService userService)
        {
            _userService = userService;
        }

        public IViewComponentResult Invoke()
        {
            var vm = new SuggestedUsersViewModel
            {
                SuggestedUsers = _userService.GetSuggestedUsers(CurrentUser.Id)
            };
            return View("_SuggestedUsers", vm);
        }
    }
}
