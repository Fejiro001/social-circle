using Microsoft.EntityFrameworkCore;
using SocialCircle.Models;

namespace SocialCircle.DAL
{
    public class PostRepo
    {
        private readonly SocialCircleDbContext _context;
        public PostRepo(SocialCircleDbContext context)
        {
            _context = context;
        }

        public List<Post> GetFeedPosts(List<int> userIds)
        {
            return _context.Posts
                .Include(p => p.User)
                .Where(p => userIds.Contains(p.UserId))
                .OrderByDescending(p => p.Timestamp)
                .ToList();
        }

        public int GetLikesCount(int postId) => _context.PostLikes.Count(l => l.PostId == postId);

        public int GetCommentsCount(int postId) => _context.Comments.Count(c => c.PostId == postId);

        public List<Post> GetUsersPosts(int targetUserId)
        {
            return _context.Posts
                .Where(p => p.UserId == targetUserId)
                .OrderByDescending(p => p.Timestamp)
                .ToList();
        }

        public Post GetPostById(int postId) => _context.Posts.Find(postId);

        public void AddPost(Post post)
        {
            _context.Posts.Add(post);
        }

        public void UpdatePost(Post post)
        {
            _context.Posts.Update(post);
        }

        public void DeletePost(int postId)
        {
            Post post = _context.Posts.Find(postId);
            if (post != null)
            {
                _context.Posts.Remove(post);
            }
        }

        public void Save() => _context.SaveChanges();
    }
}
