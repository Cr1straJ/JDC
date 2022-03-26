using System.Security.Claims;
using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using JDC.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JDC.Areas.Profile.Controllers
{
    [Area("Profile")]
    public class HomeController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IStudentService studentService;

        public HomeController(
            UserManager<User> userManager,
            IStudentService studentService)
        {
            this.userManager = userManager;
            this.studentService = studentService;
        }

        public IActionResult Index(string userId)
        {
            userId ??= User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = userManager.FindByIdAsync(userId);

            if (user is null)
            {
                return View("Error");
            }

            return View();
        }

        public async Task<IActionResult> Characteristic(int? studentId)
        {
            if (studentId is null)
            {
                return View("Error");
            }

            var student = await studentService.GetById(studentId.Value);

            if (student is null)
            {
                return View("Error");
            }

            return View();
        }
    }
}
