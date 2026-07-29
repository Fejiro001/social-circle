using Microsoft.AspNetCore.Mvc;
using SocialCircle.BLL;
using SocialCircle.Helpers;

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

        // Reusable helper to move the user back to the exact page they came from
        private IActionResult RedirectToPreviousPage()
        {
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

            return RedirectToAction("Index", "Post");
        }
    }
}