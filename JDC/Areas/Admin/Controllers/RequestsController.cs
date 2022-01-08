using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JDC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RequestsController : Controller
    {
        private readonly IRegistrationRequestService registrationRequestService;

        public RequestsController(IRegistrationRequestService registrationRequestService)
        {
            this.registrationRequestService = registrationRequestService;
        }

        public async Task<IActionResult> Index()
        {
            return this.View(await this.registrationRequestService.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult> Accept(int? id)
        {
            if (!id.HasValue)
            {
                return this.View("Error");
            }

            await this.registrationRequestService.Accept(id.Value);

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int? id)
        {
            var request = await this.registrationRequestService.GetById(id);

            if (request is null)
            {
                return this.View("Error");
            }

            await this.registrationRequestService.Delete(request);

            return this.RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(int? id)
        {
            var request = await this.registrationRequestService.GetById(id);

            if (request is null)
            {
                return this.View("Error");
            }

            return this.View(request);
        }
    }
}
