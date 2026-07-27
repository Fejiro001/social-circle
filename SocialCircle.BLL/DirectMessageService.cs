using SocialCircle.DAL;
using SocialCircle.Models;

namespace SocialCircle.BLL
{
    public class DirectMessageService
    {
        private readonly DirectMessageRepo _dmRepo;

        public DirectMessageService(DirectMessageRepo dmRepo)
        {
            _dmRepo = dmRepo;
        }

        public void SendChatMessage(int senderId, int receiverId, string text)
        {
            var message = new DirectMessage
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                MessageText = text,
                Timestamp = DateTime.Now,
                IsRead = false
            };
            _dmRepo.InsertMessage(message);
        }

        public List<DirectMessage> GetChatHistory(int userA, int userB)
        {
            // Loads chronologically by time ascending
            return _dmRepo.FetchConversation(userA, userB)
                .OrderBy(m => m.Timestamp)
                .ToList();
        }

        public List<DirectMessage> GetInboxSummaries(int currentUserId)
        {
            return _dmRepo.FetchAllUserMessages(currentUserId)
                .GroupBy(m => m.SenderId == currentUserId ? m.ReceiverId : m.SenderId)
                .Select(g => g.OrderByDescending(m => m.Timestamp).First())
                .OrderByDescending(m => m.Timestamp)
                .ToList();
        }
    }

}
