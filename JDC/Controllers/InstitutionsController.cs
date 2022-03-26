using System;
using System.Linq;
using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using JDC.BusinessLogic.Utilities.AutoMapper;
using JDC.BusinessLogic.Utilities.ImageStorage;
using JDC.Common.Entities;
using JDC.Models;
using Microsoft.AspNetCore.Mvc;

namespace JDC.Controllers
{
    /// <summary>
    /// Provides institution endpoints.
    /// </summary>
    public class InstitutionsController : Controller
    {
        private readonly IAuthService authService;
        private readonly IImageStorage imageStorage;
        private readonly IInstitutionService institutionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="InstitutionsController"/> class.
        /// </summary>
        public InstitutionsController(
            IAuthService authService,
            IImageStorage imageStorage,
            IInstitutionService institutionService)
        {
            this.authService = authService;
            this.imageStorage = imageStorage;
            this.institutionService = institutionService;
        }

        /// <summary>
        /// Gets all the institutions.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<IActionResult> Index()
        {
            return View(await institutionService.GetAll());
        }

        /// <summary>
        /// Gets all the institutions based on the filter.
        /// </summary>
        /// <param name="filter">Part of the institution's name.</param>
        /// <param name="types">Types of institutions.</param>
        [HttpPost]
        public async Task<JsonResult> GetInstitutions(string filter, int[] types)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                filter = string.Empty;
            }

            var institutions = await institutionService.GetAll();

            return Json(institutions.ToList()
                .Where(institution => types.Contains((int)institution.InstituteType)
                && institution.Name.Contains(filter, StringComparison.CurrentCultureIgnoreCase)));
        }

        /// <summary>
        /// Displays the institution editing page.
        /// </summary>
        /// <param name="institutionId">Institution id.</param>
        public async Task<IActionResult> Edit(int? institutionId)
        {
            if (institutionId is null)
            {
                return View("Error");
            }

            var institution = await institutionService.GetById(institutionId.Value);

            if (institution is null)
            {
                return View("Error");
            }

            return View(new CompiledMapper<InstitutionViewModel>().Map(institution));
        }

        /// <summary>
        /// Edits institution.
        /// </summary>
        /// <param name="institutionViewModel">Edit institution request information.</param>
        [HttpPost]
        public async Task<IActionResult> Edit(InstitutionViewModel institutionViewModel)
        {
            var institution = await institutionService.GetById(institutionViewModel.Id);

            if (institution is null)
            {
                return View("Error");
            }

            var formFile = institutionViewModel.Image;
            var mapper = new CompiledMapper<Institution>();
            institution = mapper.Map(institutionViewModel);

            if (!imageStorage.IsImage(formFile))
            {
                return new UnsupportedMediaTypeResult();
            }

            if (formFile?.Length > 0)
            {
                string imageUrl = await imageStorage.UploadFileToStorage(formFile.OpenReadStream(), $"item-preview-{DateTime.Now.Ticks}.png");
                institution.ImageUrl = imageUrl;
            }

            await institutionService.Update(institution);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Displays the institution view page.
        /// </summary>
        /// <param name="institutionId">Institution id.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.</returns>
        public async Task<IActionResult> Details(int? institutionId)
        {
            var user = await authService.GetUser(User);

            if (user is null && institutionId is null)
            {
                return View("Error");
            }

            institutionId ??= user.InstitutionId;

            var institution = await institutionService.GetById(institutionId.Value);

            if (institution is null)
            {
                return View("Error");
            }

            return View(institution);
        }
    }
}
