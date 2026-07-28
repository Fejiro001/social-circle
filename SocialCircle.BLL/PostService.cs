using Microsoft.EntityFrameworkCore;
using SocialCircle.DAL;
using SocialCircle.Models;

namespace SocialCircle.BLL
{
    public class PostService
    {
        private readonly PostRepo _postRepo;
        private readonly UserFollowRepo _followRepo;
        public PostService(PostRepo postRepo, UserFollowRepo followRepo)
        {
            _postRepo = postRepo;
            _followRepo = followRepo;
        }

        public List<Post> GetFeedPosts(int currentUserId)
        {
            List<UserFollow> folowingList = _followRepo.GetFollowing(currentUserId);
            List<int> userIds = folowingList.Select(f => f.FollowingId).ToList();

            // Current user should see their posts too
            userIds.Add(currentUserId);

            return _postRepo.GetFeedPosts(userIds);
        }

        public List<Post> GetUsersPosts(int targetUserId)
        {
            return _postRepo.GetUsersPosts(targetUserId);
        }

        public Post GetPostById(int postId) => _postRepo.GetPostById(postId);

        public int GetLikesCount(int postId) => _postRepo.GetLikesCount(postId);

        public int GetCommentsCount(int postId) => _postRepo.GetCommentsCount(postId);

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
