using System.Security.Claims;
using System.Threading.Tasks;
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
            /*userId ??= User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = userManager.FindByIdAsync(userId);

            if (user is null)
            {
                return View("Error");
            }*/

            return this.View(new User());
        }

        public IActionResult CreateCharacteristic(string userId)
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCharacteristic()
        {
            return View();
        }
    }
}
