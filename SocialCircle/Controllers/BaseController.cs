using Microsoft.AspNetCore.Mvc;

namespace SocialCircle.Controllers
{
    public class BaseController : Controller
    {
        // Reusable helper to move the user back to the exact page they came from
        protected IActionResult RedirectToPreviousPage(string fallbackAction = "Index", string fallbackController = "Post")
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

            return RedirectToAction(fallbackAction, fallbackController);
        }
    }
}
