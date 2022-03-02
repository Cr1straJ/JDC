using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using JDC.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JDC.Controllers
{
    /// <summary>
    /// Provides group endpoints.
    /// </summary>
    public class GroupsController : Controller
    {
        private readonly IGroupService groupService;
        private readonly IInstitutionService institutionService;
        private readonly UserManager<User> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupsController"/> class.
        /// </summary>
        /// <param name="userManager">Provides the APIs for managing user in a persistence store.</param>
        /// <param name="groupService">Instance of the <see cref="IGroupService"/> interface.</param>
        /// <param name="institutionService">Instance of the <see cref="IInstitutionService"/> interface.</param>
        public GroupsController(UserManager<User> userManager, IGroupService groupService, IInstitutionService institutionService)
        {
            this.userManager = userManager;
            this.groupService = groupService;
            this.institutionService = institutionService;
        }

        public async Task<IActionResult> Index(int? institutionId)
        {
            if (institutionId is null)
            {
                return View("Error");
            }

            var groups = await groupService.GetInstitutionGroups(institutionId.Value);

            return View(groups);
        }

        public async Task<IActionResult> Create()
        {
            await InitViewData();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,TeacherId")] Group group)
        {
            var currentUser = await userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                group.InstitutionId = currentUser.InstitutionId;

                await groupService.Add(group);

                return RedirectToAction(nameof(Index));
            }

            await InitViewData();

            return View(group);
        }

        /// <summary>
        /// Edits plan information.
        /// </summary>
        /// <param name="groupId">Group id to be edited.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<IActionResult> Edit(int? groupId)
        {
            if (groupId is null)
            {
                return View("Error");
            }

            var group = groupService.GetById(groupId.Value);

            if (group is null)
            {
                return View("Error");
            }

            await InitViewData();

            return View(group);
        }

        /// <summary>
        /// Edits group information.
        /// </summary>
        /// <param name="group">Edit group request information.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Name,TeacherId")] Group group)
        {
            if (ModelState.IsValid)
            {
                await groupService.Update(group);

                return RedirectToAction(nameof(Index));
            }

            await InitViewData();

            return View(group);
        }

        public IActionResult Delete(int? id)
        {
            if (id is null)
            {
                return View("Error");
            }

            var group = groupService.GetById(id.Value);

            if (group is null)
            {
                return View("Error");
            }

            return View(group);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var group = await groupService.GetById(id);

            await groupService.Remove(group);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id is null)
            {
                return View("Error");
            }

            var group = groupService.GetById(id.Value);

            if (group is null)
            {
                return View("Error");
            }

            return View(group);
        }

        private async Task InitViewData()
        {
            var currentUser = await userManager.GetUserAsync(User);
            var institution = await institutionService.GetById(currentUser.InstitutionId);

            ViewData["Specialities"] = new SelectList(institution.Specialities);
            ViewData["Teachers"] = new SelectList(institution.Teachers, "Id", "User.ShortName");
        }
    }
}
