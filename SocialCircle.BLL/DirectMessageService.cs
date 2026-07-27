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


    }

}
