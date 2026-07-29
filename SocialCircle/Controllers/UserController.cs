using Microsoft.AspNetCore.Mvc;
using SocialCircle.BLL;
using SocialCircle.Helpers;
using SocialCircle.Models;

namespace SocialCircle.Controllers
{
    public class UserController : BaseController
    {
        private readonly UserService _userService;
        private readonly UserFollowService _followService;
        private readonly PostService _postService;
        private readonly PostLikeService _postLikeService;

        public UserController(UserService service, UserFollowService followService, PostService postService, PostLikeService postLikeService)
        {
            _userService = service;
            _followService = followService;
            _postService = postService;
            _postLikeService = postLikeService;
        }


        [HttpGet]
        public IActionResult ViewProfile(int? id)
        {
            int currentUserId = CurrentUser.Id;
            int targetId = id ?? currentUserId;

            var profile = _userService.GetProfileData(targetId, currentUserId);

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
                CommentsCount = _postService.GetCommentsCount(p.PostId),
                HasCurrentUserLiked = _postLikeService.HasCurrentUserLiked(p.PostId, currentUserId)
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

            return RedirectToPreviousPage();
        }

        [HttpGet]
        public IActionResult Following(int id)
        {
            int currentUserId = CurrentUser.Id;
            var targetUser = _userService.GetUserById(id);

            if (targetUser == null)
            {
                return NotFound();
            }

            var followingRels = _followService.GetFollowing(id);

            var userCards = followingRels.Select(f =>
            {
                var u = f.Following;
                return new FollowUserCardViewModel
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    Bio = u.Bio,
                    ProfilePicUrl = u.ProfilePicUrl,
                    IsFollowing = _followService.IsFollowing(currentUserId, u.UserId),
                    IsSelf = u.UserId == currentUserId
                };
            }).ToList();

            var vm = new FollowListViewModel
            {
                User = targetUser,
                ListType = ListType.Following,
                Users = userCards
            };
            return View("FollowList", vm);
        }

        [HttpGet]
        public IActionResult Followers(int id)
        {
            int currentUserId = CurrentUser.Id;
            var targetUser = _userService.GetUserById(id);

            if (targetUser == null)
            {
                return NotFound();
            }

            var followerRels = _followService.GetFollowers(id);

            var userCards = followerRels.Select(f =>
            {
                var u = f.Follower;
                return new FollowUserCardViewModel
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    Bio = u.Bio,
                    ProfilePicUrl = u.ProfilePicUrl,
                    IsFollowing = _followService.IsFollowing(currentUserId, u.UserId),
                    IsSelf = u.UserId == currentUserId
                };
            }).ToList();

            var vm = new FollowListViewModel
            {
                User = targetUser,
                ListType = ListType.Followers,
                Users = userCards
            };
            return View("FollowList", vm);
        }
    }
}
