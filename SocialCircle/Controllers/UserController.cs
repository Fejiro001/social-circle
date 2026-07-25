using Microsoft.AspNetCore.Mvc;
using SocialCircle.BLL;
using SocialCircle.Models;

namespace SocialCircle.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;
        public UserController(UserService service)
        {
            _userService = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<User> users = _userService.GetAllUsers();
            return View(users);
        }

        [HttpGet]
        public IActionResult UsersProfile(int id)
        {
            User user = _userService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}
