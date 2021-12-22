using Microsoft.AspNetCore.Mvc;

namespace JDC.Areas.Account.Controllers
{
    [Area("Account")]
    public class ManageController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
