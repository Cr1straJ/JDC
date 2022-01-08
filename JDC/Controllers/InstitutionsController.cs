using System.Collections.Generic;
using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using JDC.Common.Entities;
using Microsoft.AspNetCore.Mvc;

namespace JDC.Controllers
{
    public class InstitutionsController : Controller
    {
        private readonly IInstitutionService institutionService;

        public InstitutionsController(IInstitutionService institutionService)
        {
            this.institutionService = institutionService;
        }

        public async Task<IActionResult> Index()
        {
            return this.View(await this.GetInstitutionsAsync());
        }

        public async Task<IEnumerable<Institution>> GetInstitutionsAsync()
        {
            return await this.institutionService.GetAll();
        }
    }
}
