using System.Linq;
using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
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
                && institution.Name.Contains(filter, System.StringComparison.CurrentCultureIgnoreCase)));
        }
    }
}
