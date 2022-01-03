using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using JDC.Common.Entities;
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
            // return this.View(await this.registrationRequestService.GetAll());
            return this.View(new List<RegistrationRequest>() 
            {
                new RegistrationRequest("Ivano Ivan Ivanovich", "+375293584675", null, "gret.@gmail.com", 4356),
                new RegistrationRequest("Ivano Ivan Ivanovich", "+375293584675", null, "gret.@gmail.com", 4356),
                new RegistrationRequest("Ivano Ivan Ivanovich", "+375293584675", null, "gret.@gmail.com", 4356),
                new RegistrationRequest("Ivano Ivan Ivanovich", "+375293584675", null, "gret.@gmail.com", 4356),
                new RegistrationRequest("Ivano Ivan Ivanovich", "+375293584675", null, "gret.@gmail.com", 4356),
                new RegistrationRequest("Ivano Ivan Ivanovich", "+375293584675", null, "gret.@gmail.com", 4356),
                new RegistrationRequest("Ivano Ivan Ivanovich", "+375293584675", null, "gret.@gmail.com", 4356),
                new RegistrationRequest("Ivano Ivan Ivanovich", "+375293584675", null, "gret.@gmail.com", 4356),
                new RegistrationRequest("Ivano Ivan Ivanovich", "+375293584675", null, "gret.@gmail.com", 4356),
                new RegistrationRequest("Ivano Ivan Ivanovich", "+375293584675", null, "gret.@gmail.com", 4356),
                new RegistrationRequest("Ivano Ivan Ivanovich", "+375293584675", null, "gret.@gmail.com", 4356),
                new RegistrationRequest("Ivano Ivan Ivanovich", "+375293584675", null, "gret.@gmail.com", 4356),
            });
        }
    }
}
