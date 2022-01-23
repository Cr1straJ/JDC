using System;
using System.Linq;
using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using JDC.BusinessLogic.Utilities.AutoMapper;
using JDC.BusinessLogic.Utilities.AzureStorage;
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
        private readonly IAzureStorage azureStorage;
        private readonly IInstitutionService institutionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="InstitutionsController"/> class.
        /// </summary>
        public InstitutionsController(IAzureStorage azureStorage, IInstitutionService institutionService)
        {
            this.azureStorage = azureStorage;
            this.institutionService = institutionService;
        }

        /// <summary>
        /// Gets all the institutions.
        /// </summary>
        public async Task<IActionResult> Index()
        {
            return this.View(await this.institutionService.GetAll());
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

            var institutions = await this.institutionService.GetAll();

            return this.Json(institutions.ToList()
                .Where(institution => types.Contains((int)institution.InstituteType)
                && institution.Name.Contains(filter, StringComparison.CurrentCultureIgnoreCase)));
        }

        /// <summary>
        /// Displays the institution editing page.
        /// </summary>
        /// <param name="institutionId">Institution id.</param>
        public async Task<IActionResult> Edit(int? institutionId)
        {
            var institution = await this.institutionService.GetById(institutionId);

            if (institution is null)
            {
                return this.View("Error");
            }

            return this.View(new CompiledMapper<InstitutionViewModel>().Map(institution));
        }

        /// <summary>
        /// Edits institution.
        /// </summary>
        /// <param name="institutionViewModel">Edit institution request information.</param>
        [HttpPost]
        public async Task<IActionResult> Edit(InstitutionViewModel institutionViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(institutionViewModel);
            }

            var institution = await this.institutionService.GetById(institutionViewModel.Id);

            if (institution is null)
            {
                return this.View("Error");
            }

            var formFile = institutionViewModel.Image;
            var mapper = new CompiledMapper<Institution>();
            institution = mapper.Map(institutionViewModel);

            if (!this.azureStorage.IsImage(formFile))
            {
                return new UnsupportedMediaTypeResult();
            }

            if (formFile?.Length > 0)
            {
                string imageUrl = await this.azureStorage.UploadFileToStorage(formFile.OpenReadStream(), $"item-preview-{DateTime.Now.Ticks}.png");
                institution.ImageUrl = imageUrl;
            }

            await this.institutionService.Update(institution);

            return this.RedirectToAction("Index");
        }
    }
}
