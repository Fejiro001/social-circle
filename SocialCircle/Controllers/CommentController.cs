using Microsoft.AspNetCore.Mvc;
using SocialCircle.BLL;
using SocialCircle.Models;

namespace SocialCircle.Controllers
{
    public class CommentController : Controller
    {
        private readonly CommentService _commentService;

        public CommentController(CommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public IActionResult AddComment(int postId, string commentText)
        {
            if (string.IsNullOrWhiteSpace(commentText))
            {
                TempData["ErrorMessage"] = "Comment cannot be empty.";
                return RedirectToAction("Index", "Post");
            }

            _commentService.AddComment(postId, commentText);

            return RedirectToAction("Index", "Post");
        }

        [HttpPost]
        public IActionResult RemoveComment(int commentId)
        {
            _commentService.RemoveComment(commentId);

            return RedirectToAction("Index", "Post");
        }
    }
}