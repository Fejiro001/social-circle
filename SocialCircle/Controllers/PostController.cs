using Microsoft.AspNetCore.Mvc;
using SocialCircle.BLL;
using SocialCircle.Helpers;
using SocialCircle.Models;

namespace SocialCircle.Controllers
{
    public class PostController : Controller
    {
        private readonly PostService _postService;
        private readonly PostLikeService _postLikeService;
        private readonly UserService _userService;
        private readonly CommentService _commentService;

        public PostController(PostService postService, PostLikeService postLikeService, UserService userService, CommentService commentService)
        {
            _postService = postService;
            _postLikeService = postLikeService;
            _userService = userService;
            _commentService = commentService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            int currentUserId = CurrentUser.Id;
            List<Post> posts = _postService.GetFeedPosts(currentUserId);
            User currentUser = _userService.GetUserById(currentUserId);

            var feedPosts = posts.Select(p => new FeedPostViewModel
            {
                PostId = p.PostId,
                PostText = p.PostText,
                Timestamp = p.Timestamp,
                UserId = p.UserId,
                UserName = p.User.UserName,
                ProfilePicUrl = p.User?.ProfilePicUrl,
                LikesCount = _postService.GetLikesCount(p.PostId),
                CommentsCount = _postService.GetCommentsCount(p.PostId),
                HasCurrentUserLiked = _postLikeService.HasCurrentUserLiked(p.PostId, currentUserId)
            }).ToList();

            NewsfeedViewModel vm = new NewsfeedViewModel
            {
                Posts = feedPosts,
                CurrentUser = currentUser
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult CreatePost(CreatePostViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Post cannot be empty.";
                return RedirectToAction("Index");
            }

            Post newPost = new Post
            {
                UserId = CurrentUser.Id,
                PostText = vm.PostText
            };

            _postService.CreatePost(newPost);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ToggleLike(int postId)
        {
            int currentUserId = CurrentUser.Id;
            _postLikeService.ToggleLike(postId, currentUserId);

            // Safely return back to Feed, Profile, or Detail view
            string referer = Request.Headers.Referer.ToString();

            if (!string.IsNullOrEmpty(referer))
            {
                Uri uri = new Uri(referer);
                string relativePath = uri.PathAndQuery;

                if (Url.IsLocalUrl(relativePath))
                {
                    return Redirect(relativePath);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            int currentUserId = CurrentUser.Id;

            var post = _postService.GetPostById(id);
            if (post == null) return NotFound();

            var rawComments = _commentService.GetPostComments(id);

            var commentVM = rawComments.Select(c => new CommentViewModel
            {
                CommentId = c.CommentId,
                CommentText = c.CommentText,
                AuthorName = c.User.UserName,
                Timestamp = c.Timestamp
            }).ToList();

            var vm = new PostDetailsViewModel
            {
                Post = post,
                Interactions = new PostInteractionsViewModel
                {
                    TotalLikes = _postLikeService.GetTotalLikes(id),
                    HasCurrentUserLiked = _postLikeService.HasCurrentUserLiked(id, currentUserId),
                    Comments = commentVM
                }
            };

            return View(vm);
        }
    }
}
