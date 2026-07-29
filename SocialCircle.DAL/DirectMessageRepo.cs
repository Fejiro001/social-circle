using Microsoft.EntityFrameworkCore;
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

        public IQueryable<DirectMessage> FetchConversation(int userA, int userB)
        {
            return _context.DirectMessages
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .Where(m => (m.SenderId == userA && m.ReceiverId == userB) ||
                            (m.SenderId == userB && m.ReceiverId == userA));
        }

        public IQueryable<DirectMessage> FetchAllUserMessages(int userId)
        {
            return _context.DirectMessages
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .Where(m => m.SenderId == userId || m.ReceiverId == userId);
        }
    }
}
