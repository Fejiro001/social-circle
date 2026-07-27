using SocialCircle.Models;

namespace SocialCircle.DAL
{
    public class DirectMessageRepo
    {
        private readonly SocialCircleDbContext _context;

        public DirectMessageRepo(SocialCircleDbContext context)
        {
            _context = context;
        }

        public void InsertMessage(DirectMessage message)
        {
            _context.DirectMessages.Add(message);
            _context.SaveChanges();
        }

        public List<DirectMessage> FetchConversation(int userA, int userB)
        {
            return _context.DirectMessages
                .Where(m => (m.SenderId == userA && m.ReceiverId == userB) ||
                            (m.SenderId == userB && m.ReceiverId == userA))
                .ToList();
        }

        public List<DirectMessage> FetchAllUserMessages(int userId)
        {
            return _context.DirectMessages
                .Where(m => m.SenderId == userId || m.ReceiverId == userId)
                .ToList();
        }
    }
}
