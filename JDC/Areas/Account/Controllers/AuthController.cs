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
        private readonly ILogger<AuthController> logger;
        private readonly IConfiguration configuration;
        private readonly IEmailSender emailSender;
        private readonly IRegistrationRequestService registrationRequestService;
        private readonly SignInManager<User> signInManager;

        public AuthController(
            ILogger<AuthController> logger, 
            IConfiguration configuration, 
            IEmailSender emailSender, 
            IRegistrationRequestService registrationRequestService, 
            SignInManager<User> signInManager)
        {
            this.configuration = configuration;
            this.logger = logger;
            this.emailSender = emailSender;
            this.registrationRequestService = registrationRequestService;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> Registration(RegistrationModel registrationModel)
        {

            if (this.ModelState.IsValid)
            {
                int confirmationCode = new Random().Next(1000, 10000);
                RegistrationRequest registrationRequest = new(registrationModel.DirectorName, registrationModel.PhoneNumber, registrationModel.WebsiteLink, registrationModel.Email, confirmationCode);

                await this.registrationRequestService.Create(registrationRequest);

                await this.emailSender.SendEmailAsync(registrationModel.DirectorName, registrationModel.Email, "Подтверждение регистрации учреждения", $"Ваш код: {confirmationCode}", this.configuration);
                this.logger.LogInformation("The letter is sent");
                return this.RedirectToAction("RegisterConfirmation", new { id = registrationRequest.ID, email = registrationModel.Email });
            }

            return this.View(registrationModel);
        }

        public async Task<IActionResult> Login(LoginModel inputLogin)
        {
            if (this.ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await this.signInManager.PasswordSignInAsync(inputLogin.UserName, inputLogin.Password, inputLogin.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    this.logger.LogInformation("User logged in.");
                    User user = await this.signInManager.UserManager.FindByNameAsync(inputLogin.UserName);

                    if (await this.signInManager.UserManager.IsInRoleAsync(user, "Director"))
                    {
                        return this.RedirectToAction("Index", "Groups");
                    }

                    return this.RedirectToAction("Index", "Admin");
                }

                if (result.IsLockedOut)
                {
                    this.logger.LogWarning("User account locked out.");
                    return this.RedirectToPage("./Lockout");
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
