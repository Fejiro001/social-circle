using Microsoft.AspNetCore.Mvc;
using SocialCircle.BLL;
using SocialCircle.Helpers;
using SocialCircle.Models;

namespace SocialCircle.ViewComponents
{
    public class SuggestedUsersViewComponent : ViewComponent
    {
        private readonly UserService _userService;
        private readonly UserFollowService _followService;
        public SuggestedUsersViewComponent(UserService userService, UserFollowService followService)
        {
            _userService = userService;
            _followService = followService;
        }

        public IViewComponentResult Invoke()
        {
            int currentUserId = CurrentUser.Id;
            var suggestions = _userService.GetSuggestedUsers(currentUserId);

            var vm = new SuggestedUsersViewModel
            {
                SuggestedUsers = suggestions?.Select(u => new SuggestedUserItemViewModel
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    Bio = u.Bio,
                    ProfilePicUrl = u.ProfilePicUrl,
                    IsFollowing = _followService.IsFollowing(currentUserId, u.UserId)
                }).ToList() ?? new List<SuggestedUserItemViewModel>()
            };
            return View(vm);
        }
    }
}
