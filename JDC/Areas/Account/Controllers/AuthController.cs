using System.Threading.Tasks;
using JDC.Common.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using JDC.Areas.Account.Models;
using System;
using JDC.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace JDC.Areas.Identity.Controllers
{
    public class AuthController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IEmailSender emailSender;
        private readonly IRegistrationRequestService registrationRequestService;
        private readonly SignInManager<User> signInManager;

        public AuthController(
            IConfiguration configuration, 
            IEmailSender emailSender, 
            IRegistrationRequestService registrationRequestService, 
            SignInManager<User> signInManager)
        {
            this.configuration = configuration;
            this.emailSender = emailSender;
            this.registrationRequestService = registrationRequestService;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationModel registrationModel)
        {

            if (this.ModelState.IsValid)
            {
                int confirmationCode = new Random().Next(1000, 10000);
                RegistrationRequest registrationRequest = new(registrationModel.DirectorName, registrationModel.PhoneNumber, registrationModel.WebsiteLink, registrationModel.Email, confirmationCode);

                await this.registrationRequestService.Create(registrationRequest);

                await this.emailSender.SendEmailAsync(registrationModel.DirectorName, registrationModel.Email, "Подтверждение регистрации учреждения", $"Ваш код: {confirmationCode}", this.configuration);
                return this.RedirectToAction("RegisterConfirmation", new { id = registrationRequest.ID, email = registrationModel.Email });
            }

            return this.View(registrationModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel inputLogin)
        {
            if (this.ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await this.signInManager.PasswordSignInAsync(inputLogin.UserName, inputLogin.Password, inputLogin.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    User user = await this.signInManager.UserManager.FindByNameAsync(inputLogin.UserName);

                    if (await this.signInManager.UserManager.IsInRoleAsync(user, "Director"))
                    {
                        return this.RedirectToAction("Index", "Groups");
                    }

                    return this.RedirectToAction("Index", "Admin");
                }

                if (result.IsLockedOut)
                {
                    //Redirect to lockout
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return this.View();
                }
            }

            // If we got this far, something failed, redisplay form
            return this.View(inputLogin);
        }
    }
}
