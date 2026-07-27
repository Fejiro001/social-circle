using Microsoft.AspNetCore.Mvc;
using SocialCircle.BLL;
using SocialCircle.Models;

namespace SocialCircle.Controllers
{
    public class DirectMessageController : Controller
    {
        private readonly DirectMessageService _dmService;
        private const int CurrentUserMockID = 1; 

        public DirectMessageController(DirectMessageService dmService)
        {
            _dmService = dmService;
        }


    }
}
