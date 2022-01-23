using Microsoft.AspNetCore.Mvc;

namespace JDC.Controllers
{
    /// <summary>
    /// Provides home endpoints.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        public HomeController()
        {
        }

        /// <summary>
        /// Displays the home page.
        /// </summary>
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Displays the documentation page.
        /// </summary>
        public IActionResult Documentation()
        {
            return this.View();
        }

        /// <summary>
        /// Displays the error page.
        /// </summary>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View();
        }
    }
}
