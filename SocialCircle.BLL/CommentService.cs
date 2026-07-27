using SocialCircle.DAL;
using SocialCircle.Models;

namespace SocialCircle.BLL
{
    public class CommentService
    {
        private readonly CommentRepo _commentRepo;

        public CommentService(CommentRepo commentRepo)
        {
            _commentRepo = commentRepo;
        }

        public List<Comment> GetPostComments(int postId)
        {
            return _commentRepo.FetchCommentsForPost(postId);
        }

        public void AddComment(int postId, string commentText)
        {
            if (string.IsNullOrWhiteSpace(commentText))
            {
                throw new ArgumentException("Comment cannot be empty.");
            }

            Comment comment = new Comment
            {
                PostId = postId,
                UserId = 1, // Current logged-in user
                CommentText = commentText,
                Timestamp = DateTime.Now
            };

            _commentRepo.InsertComment(comment);
            _commentRepo.Save();
        }

        public void RemoveComment(int commentId)
        {
            _commentRepo.DeleteComment(commentId);
            _commentRepo.Save();
        }
    }
}