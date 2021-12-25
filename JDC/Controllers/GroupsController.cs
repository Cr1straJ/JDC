using System.Collections.Generic;
using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using JDC.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JDC.Controllers
{
    public class GroupsController : Controller
    {
        private readonly IGroupService groupService;
        private readonly ITeacherService teacherService;
        private readonly ISpecialityService specialityService;
        private readonly UserManager<User> userManager;

        public GroupsController(
            UserManager<User> userManager, 
            IGroupService groupService,
            ITeacherService teacherService,
            ISpecialityService specialityService)
        {
            this.userManager = userManager;
            this.groupService = groupService;
            this.teacherService = teacherService;
            this.specialityService = specialityService;
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

        public async Task<IActionResult> Create()
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);

            this.ViewData["Specialities"] = new SelectList(await this.specialityService.GetInstitutionSpecialities(currentUser.InstitutionId));
            this.ViewData["Teachers"] = new SelectList(await this.teacherService.GetInstitutionTeachers(currentUser.InstitutionId), "Id", "User.ShortName");
            
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,TeacherId")] Group group)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);

            if (this.ModelState.IsValid)
            {
                group.InstitutionId = currentUser.InstitutionId;
                await this.groupService.Add(group);

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(group);
        }
    }
}
