using Microsoft.AspNetCore.Mvc;
using SocialCircle.BLL;
using SocialCircle.Helpers;

namespace SocialCircle.Controllers
{
    public class CommentController : BaseController
    {
        private readonly CommentService _commentService;

        public CommentController(CommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public IActionResult AddComment(int postId, string commentText)
        {
            int currentUserId = CurrentUser.Id;

            if (string.IsNullOrWhiteSpace(commentText))
            {
                TempData["ErrorMessage"] = "Comment cannot be empty.";
                return RedirectToPreviousPage();
            }

            _commentService.AddComment(postId, currentUserId, commentText);

            return RedirectToPreviousPage();
        }

        [HttpPost]
        public IActionResult RemoveComment(int commentId)
        {
            _commentService.RemoveComment(commentId);

            return RedirectToPreviousPage();
        }
    }
}