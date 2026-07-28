using SocialCircle.DAL;
using SocialCircle.Models;

namespace SocialCircle.BLL
{
    public class PostLikeService
    {
        private readonly PostLikeRepo _postLikeRepo;

        public PostLikeService(PostLikeRepo postLikeRepo)
        {
            _postLikeRepo = postLikeRepo;
        }

        public void ToggleLike(int postId, int currentUserId)
        {
            bool alreadyLiked = _postLikeRepo.CheckIfUserLiked(currentUserId, postId);

            if (alreadyLiked)
            {
                _postLikeRepo.DeleteLike(currentUserId, postId);
            }
            else
            {
                PostLike like = new PostLike
                {
                    UserId = currentUserId,
                    PostId = postId,
                    LikeDateTime = DateTime.Now
                };

                _postLikeRepo.InsertLike(like);
            }

            _postLikeRepo.Save();
        }

        public int GetTotalLikes(int postId)
        {
            return _postLikeRepo.GetTotalLikes(postId);
        }

        public bool HasCurrentUserLiked(int postId)
        {
            return _postLikeRepo.CheckIfUserLiked(1, postId);
        }
    }
}