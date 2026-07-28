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
        private readonly PostService _postService;
        public UserController(UserService service, UserFollowService followService, PostService postService)
        {
            _userService = service;
            _followService = followService;
            _postService = postService;
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

            var feedPosts = profile.Posts.Select(p => new FeedPostViewModel
            {
                PostId = p.PostId,
                PostText = p.PostText,
                Timestamp = p.Timestamp,
                UserId = p.UserId,
                UserName = profile.User.UserName,
                ProfilePicUrl = profile.User.ProfilePicUrl,
                LikesCount = _postService.GetLikesCount(p.PostId),
                CommentsCount = _postService.GetCommentsCount(p.PostId)
            }).ToList();

            UserProfileViewModel vm = new UserProfileViewModel
            {
                User = profile.User,
                Posts = feedPosts,
                FollowersCount = profile.FollowersCount,
                FollowingCount = profile.FollowingCount,
                IsCurrentlyFollowing = profile.IsCurrentlyFollowing,
                IsSelf = profile.User.UserId == currentUserId
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
            return RedirectToAction("Index", "Post");
        }
    }
}
