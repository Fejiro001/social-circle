using SocialCircle.Models;

namespace SocialCircle.DAL
{
    public class PostLikeRepo
    {
        private readonly SocialCircleDbContext _context;

        public PostLikeRepo(SocialCircleDbContext context)
        {
            _context = context;
        }

        public void InsertLike(PostLike like)
        {
            _context.PostLikes.Add(like);
        }

        public void DeleteLike(int userId, int postId)
        {
            PostLike like = _context.PostLikes.Find(userId, postId);

            if (like != null)
            {
                _context.PostLikes.Remove(like);
            }
        }

        public bool CheckIfUserLiked(int userId, int postId)
        {
            return _context.PostLikes.Any(l =>
                l.UserId == userId &&
                l.PostId == postId);
        }

        public int GetTotalLikes(int postId)
        {
            return _context.PostLikes.Count(l => l.PostId == postId);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}