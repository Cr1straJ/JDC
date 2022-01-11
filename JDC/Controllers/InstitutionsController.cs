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
    public class InstitutionsController : Controller
    {
        private readonly IAzureStorage azureStorage;
        private readonly IInstitutionService institutionService;

        public InstitutionsController(IAzureStorage azureStorage, IInstitutionService institutionService)
        {
            this.azureStorage = azureStorage;
            this.institutionService = institutionService;
        }

        public async Task<IActionResult> Index()
        {
            return this.View(await this.institutionService.GetAll());
        }

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

        public async Task<IActionResult> Edit(int? id)
        {
            var institution = await this.institutionService.GetById(id);

            if (institution is null)
            {
                return this.View("Error");
            }

            return this.View(new CompiledMapper<InstitutionViewModel>().Map(institution));
        }

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
