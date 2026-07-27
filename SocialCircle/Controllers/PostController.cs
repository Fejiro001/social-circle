using Microsoft.AspNetCore.Mvc;
using SocialCircle.BLL;
using SocialCircle.Helpers;
using SocialCircle.Models;

namespace SocialCircle.Controllers
{
    public class PostController : Controller
    {
        private readonly PostService _postService;
        private readonly UserService _userService;
        public PostController(PostService postService, UserService userService)
        {
            _postService = postService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            int currentUserId = CurrentUser.Id;
            List<Post> posts = _postService.GetFeedPosts(currentUserId);

            User currentUser = _userService.GetUserById(currentUserId);

            NewsfeedViewModel vm = new NewsfeedViewModel
            {
                Posts = posts,
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
    }
}
