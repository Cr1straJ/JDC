using System.Collections.Generic;
using System.Threading.Tasks;
using JDC.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JDC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<User> userManager;

        public UsersController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            // return this.View(this.userManager.Users.ToList());
            return this.View(new List<User>()
            {
                new User()
                {
                    UserName = "Cr1straJ",
                    PhoneNumber = "+375293584675",
                    Email = "nikitagalynets52@gmail.com",
                },
            });
        }

        public async Task<IActionResult> Details(string id)
        {
           /* User user = await this.userManager.FindByIdAsync(id);

            if (user is null)
            {
                return this.View("Error");
            }*/

            return this.View(new User()
            {
                FirstName = "Nikita",
                LastName = "Galynets",
                MiddleName = "Arturovich",
                UserName = "Cr1straJ",
                PhoneNumber = "+375293584675",
                Email = "nikitagalynets52@gmail.com",
                Institution = new Institution()
                {
                    Name = "filial UO «‎BGTU»‎ «‎BGLK»‎",
                },
                Country = "Belarus",
                City = "Bobruisk",
            });
        }
    }
}
