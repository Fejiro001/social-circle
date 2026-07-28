using Microsoft.EntityFrameworkCore;
using SocialCircle.Models;

namespace SocialCircle.DAL
{
    public class CommentRepo
    {
        private readonly SocialCircleDbContext _context;

        public CommentRepo(SocialCircleDbContext context)
        {
            _context = context;
        }

        public void InsertComment(Comment comment)
        {
            _context.Comments.Add(comment);
        }

        public void DeleteComment(int commentId)
        {
            Comment comment = _context.Comments.Find(commentId);

            if (comment != null)
            {
                _context.Comments.Remove(comment);
            }
        }

        public List<Comment> FetchCommentsForPost(int postId)
        {
            return _context.Comments
                .Include(c => c.User)
                .Where(c => c.PostId == postId)
                .OrderByDescending(c => c.Timestamp)
                .ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}