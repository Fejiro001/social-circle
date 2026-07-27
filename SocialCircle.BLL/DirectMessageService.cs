using SocialCircle.DAL;
using SocialCircle.Models;

namespace SocialCircle.BLL
{
    public class DirectMessageService
    {
        private readonly DirectMessageRepo _dmRepo;

        public DirectMessageService(DirectMessageRepo dmRepo)
        {
            _dmRepo = dmRepo;
        }


    }

}
