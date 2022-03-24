using System.Security.Claims;
using JDC.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JDC.Areas.Profile.Controllers
{
    [Area("Profile")]
    public class HomeController : Controller
    {
        private readonly UserManager<User> userManager;

        public HomeController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Index(string userId)
        {
            userId ??= User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = userManager.FindByIdAsync(userId);

            if (user is null)
            {
                return View("Error");
            }

            return View();
           /* return View(new User()
            {
                UserName = "ivanov_ivan",
                FirstName = "Ivan",
                LastName = "Ivanov",
                MiddleName = "Ivanovich",
                Email = "ivanov_ivan@mail.ru",
                PhoneNumber = "+375291587982",
                Country = "Belarus",
                City = "Minsk",
            });*/
        }
    }
}
