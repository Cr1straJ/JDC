using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using JDC.Common.Entities;
using JDC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JDC.Controllers
{
    /// <summary>
    /// Provides teacher endpoints.
    /// </summary>
    public class TeachersController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ITeacherService teacherService;
        private readonly IInstitutionService institutionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeachersController"/> class.
        /// </summary>
        /// <param name="userManager">Provides the APIs for managing user in a persistence store.</param>
        /// <param name="teacherService">Teacher service.</param>
        /// <param name="institutionService">Institution service.</param>
        public TeachersController(
            UserManager<User> userManager,
            ITeacherService teacherService,
            IInstitutionService institutionService)
        {
            this.userManager = userManager;
            this.teacherService = teacherService;
            this.institutionService = institutionService;
        }

        /// <summary>
        /// Displays the index page.
        /// </summary>
        /// <param name="institutionId">Institution id.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<IActionResult> Index(int? institutionId)
        {
            var user = await userManager.GetUserAsync(User);

            if (user is null && institutionId is null)
            {
                return View("Error");
            }

            institutionId ??= user.InstitutionId;

            if (institutionId is null)
            {
                return View("Error");
            }

            var teachers = await teacherService.GetInstitutionTeachers(institutionId.Value);

            return View(teachers);
        }

        /// <summary>
        /// Displays the creation page of the teacher.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<IActionResult> Create()
        {
            var user = await userManager.GetUserAsync(User);

            if (user is null)
            {
                return View("Error");
            }

            var institutionId = user.InstitutionId.Value;
            var institution = await institutionService.GetById(institutionId);

            ViewData["Groups"] = new SelectList(institution.Groups);

            return View(new TeacherViewModel
            {
                InstitutionId = institutionId,
            });
        }

        /// <summary>
        /// Creates teacher.
        /// </summary>
        /// <param name="teacherViewModel">Teacher view model.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeacherViewModel teacherViewModel)
        {
            if (ModelState.IsValid)
            {
                Teacher teacher = new Teacher()
                {
                    InstitutionId = teacherViewModel.InstitutionId,
                    GroupId = teacherViewModel.GroupId,
                    User = new User
                    {
                        UserName = teacherViewModel.Email,
                        Email = teacherViewModel.Email,
                        FirstName = teacherViewModel.FirstName,
                        LastName = teacherViewModel.LastName,
                        MiddleName = teacherViewModel.MiddleName,
                    },
                };

                await teacherService.Add(teacher);

                return RedirectToAction(nameof(Index));
            }

            await InitViewData();

            return View(teacherViewModel);
        }

        private async Task InitViewData()
        {
            var user = await userManager.GetUserAsync(User);

            if (user is null)
            {
                return;
            }

            var institution = await institutionService.GetById(user.InstitutionId.Value);

            ViewData["Groups"] = new SelectList(institution.Groups);
        }
    }
}
