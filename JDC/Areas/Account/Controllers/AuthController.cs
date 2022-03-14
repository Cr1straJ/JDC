using System;
using System.Threading.Tasks;
using JDC.Areas.Account.Models;
using JDC.BusinessLogic.Interfaces;
using JDC.BusinessLogic.Utilities.AutoMapper;
using JDC.BusinessLogic.Utilities.EmailSender;
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

        public IActionResult Register()
        {
            return View(new RegistrationModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationModel registrationModel)
        {
            if (ModelState.IsValid)
            {
                var confirmationCode = new Random().Next(1000, 10000);
                var registrationRequest = new CompiledMapper<RegistrationRequest>().Map(registrationModel);
                registrationRequest.ConfirmationCode = confirmationCode;

                await registrationRequestService.Create(registrationRequest);
                await emailSender.SendEmailAsync(registrationModel.DirectorName, registrationModel.Email, "Подтверждение регистрации учреждения", $"Ваш код: {confirmationCode}");

                return RedirectToAction("RegisterConfirmation", new { id = registrationRequest.Id, email = registrationModel.Email });
            }

            return View(registrationModel);
        }

        public IActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel inputLogin)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(inputLogin.Username, inputLogin.Password, inputLogin.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    User user = await signInManager.UserManager.FindByNameAsync(inputLogin.Username);

                    if (await signInManager.UserManager.IsInRoleAsync(user, "Director"))
                    {
                        return RedirectToAction("Index", "Groups");
                    }

                    return RedirectToAction("Index", "Admin");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");

                return View();
            }

            return View(inputLogin);
        }
    }
}
