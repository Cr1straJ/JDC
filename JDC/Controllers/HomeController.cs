using System.Diagnostics;
using JDC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace JDC.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Documentation()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Contact()
        {
            return this.RedirectToAction("Documentation");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
