using SocialCircle.DAL;
using SocialCircle.Models;

namespace SocialCircle.BLL
{
    public class PostService
    {
        private readonly PostRepo _postRepo;
        public PostService(PostRepo postRepo)
        {
            _postRepo = postRepo;
        }

        public List<Post> GetFeedPosts(int currentUserId)
        {
            return _postRepo.GetFeedPosts(currentUserId);
        }

        public List<Post> GetUsersPosts(int targetUserId)
        {
            return _postRepo.GetUsersPosts(targetUserId);
        }

        public void CreatePost(Post post)
        {
            post.Timestamp = DateTime.Now;

            _postRepo.AddPost(post);
            _postRepo.Save();
        }

        public void UpdatePost(Post post)
        {
            post.Timestamp = DateTime.Now;

            _postRepo.UpdatePost(post);
            _postRepo.Save();
        }

        public void RemovePost(int postId)
        {
            _postRepo.DeletePost(postId);
            _postRepo.Save();
        }
    }
}
