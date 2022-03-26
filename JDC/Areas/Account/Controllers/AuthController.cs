using System.Threading.Tasks;
using JDC.Areas.Account.Models;
using JDC.BusinessLogic.Interfaces;
using JDC.BusinessLogic.Models.Requests;
using JDC.BusinessLogic.Utilities.AutoMapper;
using JDC.Common.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JDC.Areas.Identity.Controllers
{
    /// <summary>
    /// Provides authentication endpoints.
    /// </summary>
    [Area("Account")]
    public class AuthController : Controller
    {
        private readonly IAuthService authService;
        private readonly IRegistrationRequestService registrationRequestService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="authService">Authentication service.</param>
        /// <param name="registrationRequestService">Registration request service.</param>
        public AuthController(
            IAuthService authService,
            IRegistrationRequestService registrationRequestService)
        {
            this.authService = authService;
            this.registrationRequestService = registrationRequestService;
        }

        /// <summary>
        /// Displays the register page.
        /// </summary>
        /// <returns>The result of an action method.</returns>
        public IActionResult Register()
        {
            return View(new RegistrationModel());
        }

        /// <summary>
        /// Registers user according register form information.
        /// </summary>
        /// <param name="model">Register form information.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                var registrationRequest = new CompiledMapper<RegistrationRequest>().Map(model);

                await registrationRequestService.Create(registrationRequest);

                return RedirectToAction(nameof(RegisterConfirmation), new { id = registrationRequest.Id, email = model.Email });
            }

            return View(model);
        }

        /// <summary>
        /// Displays the login page.
        /// </summary>
        /// <returns>The result of an action method.</returns>
        public IActionResult Login()
        {
            return View(new LoginRequest());
        }

        /// <summary>
        /// Authencicates user according login form information.
        /// </summary>
        /// <param name="request">Login form information.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await authService.TryLogin(request);

            if (ModelState.IsValid && user is not null)
            {
                if (await authService.IsInRole(user, "Director"))
                {
                    return Redirect("~/Groups");
                }

                return RedirectToAction("~/Admin");
            }

            return View(request);
        }

        /// <summary>
        /// Displays the register confirmation page.
        /// </summary>
        /// <param name="id">Registration request id.</param>
        /// <param name="email">Registration request email.</param>
        /// <returns>The result of an action method.</returns>
        public IActionResult RegisterConfirmation(int id, string email)
        {
            return View(new RegisterConfirmationModel()
            {
                Id = id,
                Email = email,
            });
        }

        /// <summary>
        /// Confirmes user registration request information.
        /// </summary>
        /// <param name="model">Register confirmation form information.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterConfirmation(RegisterConfirmationModel model)
        {
            var registrationRequest = registrationRequestService
                .FirstOrDefault(request => model.Id is null ? request.Email.Equals(model.Email) : request.Id.Equals(model.Id));

            if (!Equals(registrationRequest.ConfirmationCode, int.Parse(model.ConfirmationCode)))
            {
                ModelState.AddModelError(string.Empty, $"Неверный код подтверждения!");
            }

            if (ModelState.IsValid)
            {
                await registrationRequestService.ConfirmEmail(registrationRequest.Id);
                return Redirect("~/Home/Index");
            }

            return View(model);
        }

        /// <summary>
        /// Logout user.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            await authService.SignOut();

            return Redirect("~/Home");
        }
    }
}
