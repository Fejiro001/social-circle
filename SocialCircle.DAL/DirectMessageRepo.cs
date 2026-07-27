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


    }
}
