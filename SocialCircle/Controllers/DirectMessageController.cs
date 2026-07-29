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

        // Displays active conversational threads and the active chat side-by-side
        public IActionResult Index(int? targetUserId)
        {
            ViewData["HideRightSidebar"] = true;
            int currentUserId = CurrentUser.Id;

            // Get sidebar threads
            var inboxModels = GetInboxViewModels(currentUserId);

            // Default to the first thread if none is explicitly selected
            if (!targetUserId.HasValue && inboxModels.Any())
            {
                targetUserId = inboxModels.First().TargetUserId;
            }

            // Assemble the final master-detail view model
            var vm = new DirectMessagesViewModel
            {
                Inbox = inboxModels,
                ActiveChat = targetUserId.HasValue ? GetActiveChatViewModel(currentUserId, targetUserId.Value) : null
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

            return RedirectToAction("Index", new { targetUserId = model.ReceiverId });
        }

        private List<InboxViewModel> GetInboxViewModels(int currentUserId)
        {
            var activeThreads = _dmService.GetInboxSummaries(currentUserId);

            return activeThreads.Select(t =>
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
        }

        private ChatHistoryViewModel GetActiveChatViewModel(int currentUserId, int targetUserId)
        {
            var messages = _dmService.GetChatHistory(currentUserId, targetUserId);

            string targetUserName = "User_" + targetUserId;
            string? targetUserAvatar = null;

            // Try to find user details from the messages first
            var firstMsg = messages.FirstOrDefault(m => m.SenderId == targetUserId || m.ReceiverId == targetUserId);
            var targetUserEntity = (firstMsg?.SenderId == targetUserId) ? firstMsg.Sender : firstMsg?.Receiver;

            if (targetUserEntity != null)
            {
                targetUserName = targetUserEntity.UserName;
                targetUserAvatar = targetUserEntity.ProfilePicUrl;
            }
            else
            {
                // Fallback: fetch directly from database if no chat history exists yet
                var dbUser = _userService.GetUserById(targetUserId);
                if (dbUser != null)
                {
                    targetUserName = dbUser.UserName;
                    targetUserAvatar = dbUser.ProfilePicUrl;
                }
            }

            return new ChatHistoryViewModel
            {
                CurrentUserId = currentUserId,
                TargetUserId = targetUserId,
                TargetUserName = targetUserName,
                TargetAvatar = targetUserAvatar,
                Messages = messages
            };
        }
    }
}
