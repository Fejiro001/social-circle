using Microsoft.AspNetCore.Mvc;
using SocialCircle.BLL;
using SocialCircle.Helpers;
using SocialCircle.Models;

namespace SocialCircle.Controllers
{
    public class DirectMessageController : Controller
    {
        private readonly DirectMessageService _dmService;
        private readonly UserService _userService;

        public DirectMessageController(DirectMessageService dmService, UserService userService)
        {
            _dmService = dmService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            int currentUserId = CurrentUser.Id;

            // Load inbox sidebar summaries
            var activeThreads = _dmService.GetInboxSummaries(currentUserId);
            var inboxModels = activeThreads.Select(t =>
            {
                bool isMeSender = t.SenderId == currentUserId;
                int targetId = isMeSender ? t.ReceiverId : t.SenderId;
                var targetUser = isMeSender ? t.Receiver : t.Sender;

                return new InboxViewModel
                {
                    TargetUserId = targetId,
                    TargetUserName = targetUser.UserName,
                    TargetAvatar = targetUser?.ProfilePicUrl,
                    LastMessageText = t.MessageText,
                    LastMessageTimestamp = t.Timestamp,
                    IsUnread = !t.IsRead && t.ReceiverId == currentUserId
                };
            }).ToList();

            return View(inboxModels);
        }

        public IActionResult ViewChat(int targetUserId)
        {
            int currentUserId = CurrentUser.Id;
            var messages = _dmService.GetChatHistory(currentUserId, targetUserId);
            var targetUser = _userService.GetUserById(targetUserId);

            var vm = new ChatHistoryViewModel
            {
                CurrentUserId = currentUserId,
                TargetUserId = targetUserId,
                TargetUserName = targetUser.UserName,
                TargetAvatar = targetUser?.ProfilePicUrl,
                Messages = messages
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult SendMessage(SendMessageViewModel model)
        {
            int currentUserId = CurrentUser.Id;

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", new { targetUserId = model.ReceiverId });
            }

            _dmService.SendChatMessage(currentUserId, model.ReceiverId, model.MessageText);

            return RedirectToAction("ViewChat", new { targetUserId = model.ReceiverId });
        }
    }
}
