using Microsoft.AspNetCore.Mvc;

namespace JDC.Areas.Identity.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
