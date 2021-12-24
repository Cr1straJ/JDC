using System.Collections.Generic;
using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using JDC.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JDC.Controllers
{
    public class GroupsController : Controller
    {
        private readonly IGroupService groupService;
        private readonly UserManager<User> userManager;

        public GroupsController(UserManager<User> userManager, IGroupService groupService)
        {
            this.userManager = userManager;
            this.groupService = groupService;
        }

        public async Task<IActionResult> Index(int? id)
        {
            bool canEditGroup = false;

            if (id is null)
            {
                User user = await this.userManager.GetUserAsync(this.User);

                if (user is not null && await this.userManager.IsInRoleAsync(user, "Director"))
                {
                    canEditGroup = true;
                    id = user.InstitutionId;
                }
            }

            this.ViewData["CanEditGroup"] = canEditGroup;
            List<Group> groups = await this.groupService.GetInstitutionGroups(id);

            if (groups is null)
            {
                return this.View("Error");
            }

            return this.View(groups);
        }
    }
}
