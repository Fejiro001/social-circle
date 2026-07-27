using Microsoft.AspNetCore.Mvc;
using SocialCircle.BLL;
using SocialCircle.Models;

namespace SocialCircle.Controllers
{
    public class DirectMessageController : Controller
    {
        private readonly DirectMessageService _dmService;
        private const int CurrentUserMockID = 1; 

        public DirectMessageController(DirectMessageService dmService)
        {
            _dmService = dmService;
        }

        // Displays active conversational threads
        public IActionResult Index()
        {
            var activeThreads = _dmService.GetInboxSummaries(CurrentUserMockID);

            var inboxModels = activeThreads.Select(t => {
                int targetId = (t.SenderId == CurrentUserMockID) ? t.ReceiverId : t.SenderId;

                return new InboxViewModel
                {
                    TargetUserId = targetId,
                    TargetUserName = "User_" + targetId,

                    TargetAvatar = $"{t.SenderId}", //can be adjusted

                    LastMessageText = t.MessageText,
                    LastMessageTimestamp = t.Timestamp,
                    IsUnread = !t.IsRead && t.ReceiverId == CurrentUserMockID
                };
            }).ToList();

            return View(inboxModels); 
        }




    }
}
