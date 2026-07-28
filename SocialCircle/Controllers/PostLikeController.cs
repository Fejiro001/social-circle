using Microsoft.AspNetCore.Mvc;
using SocialCircle.BLL;

namespace SocialCircle.Controllers
{
    public class PostLikeController : Controller
    {
        private readonly PostLikeService _postLikeService;

        public PostLikeController(PostLikeService postLikeService)
        {
            _postLikeService = postLikeService;
        }

        [HttpPost]
        public IActionResult TogglePostLike(int postId)
        {
            _postLikeService.ToggleLike(postId);

            return RedirectToAction("Index", "Post");
        }
    }
}