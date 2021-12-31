using System;
using System.Threading.Tasks;
using JDC.Areas.Account.Models;
using JDC.BusinessLogic.Interfaces;
using JDC.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JDC.Areas.Identity.Controllers
{
    [Area("Account")]
    public class AuthController : Controller
    {
        private readonly IEmailSender emailSender;
        private readonly IRegistrationRequestService registrationRequestService;
        private readonly SignInManager<User> signInManager;

        public AuthController(
            IEmailSender emailSender,
            IRegistrationRequestService registrationRequestService,
            SignInManager<User> signInManager)
        {
            this.emailSender = emailSender;
            this.registrationRequestService = registrationRequestService;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Register()
        {
            return this.View(new RegistrationModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationModel registrationModel)
        {
            if (this.ModelState.IsValid)
            {
                int confirmationCode = new Random().Next(1000, 10000);
                RegistrationRequest registrationRequest = new RegistrationRequest(registrationModel.DirectorName, registrationModel.PhoneNumber, registrationModel.WebsiteLink, registrationModel.Email, confirmationCode);

                await this.registrationRequestService.Create(registrationRequest);
                await this.emailSender.SendEmailAsync(registrationModel.DirectorName, registrationModel.Email, "Подтверждение регистрации учреждения", $"Ваш код: {confirmationCode}");
                
                return this.RedirectToAction("RegisterConfirmation", new { id = registrationRequest.Id, email = registrationModel.Email });
            }

            return this.View(registrationModel);
        }

        public IActionResult Login()
        {
            return this.View(new LoginModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel inputLogin)
        {
            if (this.ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await this.signInManager.PasswordSignInAsync(inputLogin.Username, inputLogin.Password, inputLogin.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    User user = await this.signInManager.UserManager.FindByNameAsync(inputLogin.Username);

                    if (await this.signInManager.UserManager.IsInRoleAsync(user, "Director"))
                    {
                        return this.RedirectToAction("Index", "Groups");
                    }

                    return this.RedirectToAction("Index", "Admin");
                }

                this.ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                
                return this.View();
            }

            return this.View(inputLogin);
        }
    }
}
