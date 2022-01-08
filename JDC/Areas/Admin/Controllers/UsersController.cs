using System;
using System.Linq;
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
            return this.View(this.userManager.Users.ToList());
        }

        public async Task<IActionResult> Details(string id)
        {
            User user = await this.userManager.FindByIdAsync(id);

            if (user is null)
            {
                return this.View("Error");
            }

            return this.View(user);
        }

        [HttpPost]
        public IActionResult Details(User user)
        {
            if (user is null)
            {
                return this.View("Error");
            }

            // TODO: Add a change of user
            return this.View();
        }

        [HttpPost]
        public async Task<string> ChangeStatus(string id)
        {
            string status;
            User user = await this.userManager.FindByIdAsync(id);

            if (await this.userManager.IsLockedOutAsync(user))
            {
                status = "Blocked";
                user.LockoutEnd = DateTime.Now;
            }
            else
            {
                status = "Unblocked";
                user.LockoutEnd = DateTime.MaxValue;
            }

            await this.userManager.UpdateAsync(user);

            return status;
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);

            if (user is null)
            {
                return this.View("Error");
            }

            await this.userManager.DeleteAsync(user);

            return this.RedirectToAction("Index");
        }
    }
}
