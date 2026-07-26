using Microsoft.AspNetCore.Mvc;
using SocialCircle.BLL;
using SocialCircle.Helpers;
using SocialCircle.Models;

namespace SocialCircle.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;
        private readonly UserFollowService _followService;
        public UserController(UserService service, UserFollowService followService)
        {
            _userService = service;
            _followService = followService;
        }

        [HttpGet]
        public IActionResult ViewProfile(int id)
        {
            int currentUserId = CurrentUser.Id;
            var profile = _userService.GetProfileData(id, currentUserId);

            if (profile == null)
            {
                return NotFound();
            }

            UserProfileViewModel vm = new UserProfileViewModel
            {
                User = profile.User,
                Posts = profile.Posts,
                FollowersCount = profile.FollowersCount,
                FollowingCount = profile.FollowingCount,
                IsCurrentlyFollowing = profile.IsCurrentlyFollowing
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult ToggleFollow(int targetUserId)
        {
            int currentUserId = CurrentUser.Id;
            bool isFollowing = _followService.IsFollowing(currentUserId, targetUserId);

            if (isFollowing)
            {
                _followService.UnfollowUser(currentUserId, targetUserId);
            }
            else
            {
                UserFollow newFollow = new UserFollow
                {
                    FollowerId = currentUserId,
                    FollowingId = targetUserId
                };

                _followService.FollowUser(newFollow);
            }
            return RedirectToAction("ViewProfile", new { id = targetUserId });
        }

        [HttpGet]
        public IActionResult GetSuggestedUsers()
        {
            int currentUserId = CurrentUser.Id;
            List<User> suggestedUsers = _userService.GetSuggestedUsers(currentUserId);
            var vm = new SuggestedUsersViewModel
            {
                SuggestedUsers = suggestedUsers
            };

            return PartialView("_SuggestedUsers", vm);
        }
    }
}
