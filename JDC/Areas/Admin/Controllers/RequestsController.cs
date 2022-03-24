using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JDC.Areas.Admin.Controllers
{
    /// <summary>
    /// Provides registration request endpoints.
    /// </summary>
    [Area("Admin")]
    public class RequestsController : Controller
    {
        private readonly IRegistrationRequestService registrationRequestService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestsController"/> class.
        /// </summary>
        /// <param name="registrationRequestService">Instance of the <see cref="IRegistrationRequestService"/> interface.</param>
        public RequestsController(IRegistrationRequestService registrationRequestService)
        {
            this.registrationRequestService = registrationRequestService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await registrationRequestService.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult> Accept(int? id)
        {
            if (id is null)
            {
                return View("Error");
            }

            await registrationRequestService.Accept(id.Value);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return View("Error");
            }

            var request = await registrationRequestService.GetById(id.Value);

            if (request is null)
            {
                return View("Error");
            }

            await registrationRequestService.Delete(request);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id is null)
            {
                return View("Error");
            }

            var request = await registrationRequestService.GetById(id.Value);

            if (request is null)
            {
                return View("Error");
            }

            return View(request);
        }
    }
}
