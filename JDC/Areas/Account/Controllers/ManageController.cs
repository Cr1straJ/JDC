using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using JDC.Areas.Account.Models;
using JDC.BusinessLogic.Interfaces;
using JDC.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;

namespace JDC.Areas.Account.Controllers
{
    [Area("Account")]
    public class ManageController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IConfiguration configuration;
        private readonly IEmailSender emailSender;

        public ManageController(
            UserManager<User> userManager, 
            SignInManager<User> signInManager,
            IEmailSender emailSender,
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
            this.configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user is null)
            {
                return this.NotFound($"Unable to load user with ID {this.userManager.GetUserId(this.User)}.");
            }

            return this.View(await this.GetIndexModelAsync(user));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexModel indexModel)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user is null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(indexModel);
            }

            var phoneNumber = await this.userManager.GetPhoneNumberAsync(user);

            if (indexModel.Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await this.userManager.SetPhoneNumberAsync(user, indexModel.Input.PhoneNumber);

                if (!setPhoneResult.Succeeded)
                {
                    indexModel.StatusMessage = "Unexpected error when trying to set phone number.";
                }
            }

            user.LastName = indexModel.Input.LastName;
            user.FirstName = indexModel.Input.FirstName;
            user.MiddleName = indexModel.Input.MiddleName;
            user.Sex = indexModel.Input.Sex.Equals("Мужской", StringComparison.Ordinal);

            await this.userManager.UpdateAsync(user);
            await this.signInManager.RefreshSignInAsync(user);
            indexModel.StatusMessage = "Your profile has been updated";

            return this.View(indexModel);
        }

        public async Task<IActionResult> PersonalData()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user is null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            return this.View();
        }

        public async Task<IActionResult> DeletePersonalData()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user is null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            return this.View(new DeletePersonalDataModel() { RequirePassword = await this.userManager.HasPasswordAsync(user) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePersonalData(DeletePersonalDataModel deletePersonalDataModel)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user is null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            if (deletePersonalDataModel.RequirePassword)
            {
                if (!await this.userManager.CheckPasswordAsync(user, deletePersonalDataModel.Input.Password))
                {
                    this.ModelState.AddModelError(string.Empty, "Incorrect password.");
                    return this.View(deletePersonalDataModel);
                }
            }

            var result = await this.userManager.DeleteAsync(user);
            var userId = await this.userManager.GetUserIdAsync(user);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleting user with ID '{userId}'.");
            }

            await this.signInManager.SignOutAsync();

            return this.Redirect("~/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DownloadPersonalData()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user is null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            var personalData = new Dictionary<string, string>();
            var personalDataProps = typeof(User).GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));

            foreach (var propertyInfo in personalDataProps)
            {
                personalData.Add(propertyInfo.Name, propertyInfo.GetValue(user)?.ToString() ?? "null");
            }

            foreach (var login in await this.userManager.GetLoginsAsync(user))
            {
                personalData.Add($"{login.LoginProvider} external login provider key", login.ProviderKey);
            }

            this.Response.Headers.Add("Content-Disposition", "attachment; filename=PersonalData.json");

            return new FileContentResult(JsonSerializer.SerializeToUtf8Bytes(personalData), "application/json");
        }

        public async Task<IActionResult> Email()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user is null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            return this.View(await this.GetEmailModelAsync(user));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Email(EmailModel emailModel)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user is null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(emailModel);
            }

            var email = await this.userManager.GetEmailAsync(user);
            if (emailModel.Input.NewEmail != email)
            {
                var userId = await this.userManager.GetUserIdAsync(user);
                var code = await this.userManager.GenerateChangeEmailTokenAsync(user, emailModel.Input.NewEmail);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = this.Url.Page(
                    "/Manage/ConfirmEmailChange",
                    pageHandler: null,
                    values: new { userId, email = emailModel.Input.NewEmail, code },
                    protocol: this.Request.Scheme);

                await this.emailSender.SendEmailAsync(user.ShortName, emailModel.Input.NewEmail, "Подтвердите свою электронную почту", $"Пожалуйста, подтвердите свою учетную запись, <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>нажав здесь</a>.", this.configuration);

                emailModel.StatusMessage = "Отправлена ссылка для подтверждения изменения электронной почты. Пожалуйста, проверьте свою электронную почту.";

                return this.RedirectToAction();
            }

            emailModel.StatusMessage = "Ваша электронная почта не изменилась.";

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendVerificationEmail(EmailModel emailModel)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user is null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(emailModel);
            }

            var userId = await this.userManager.GetUserIdAsync(user);
            var email = await this.userManager.GetEmailAsync(user);
            var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = this.Url.Page(
                "/Manage/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Account", userId, code },
                protocol: this.Request.Scheme);

            await this.emailSender.SendEmailAsync(user.ShortName, email, "Подтвердите свою электронную почту", $"Пожалуйста, подтвердите свою учетную запись, <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>нажав здесь</a>.", this.configuration);
            emailModel.StatusMessage = "Письмо с подтверждением отправлено. Пожалуйста, проверьте свою электронную почту.";

            return this.View();
        }

        public async Task<IActionResult> ChangePassword()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user is null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            return this.View(new ChangePasswordModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var user = await this.userManager.GetUserAsync(this.User);

            if (user is null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            var changePasswordResult = await this.userManager.ChangePasswordAsync(user, changePasswordModel.Input.OldPassword, changePasswordModel.Input.NewPassword);
            
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }

                return this.View(changePasswordModel);
            }

            await this.signInManager.RefreshSignInAsync(user);
            changePasswordModel.StatusMessage = "Your password has been changed.";

            return this.View(new ChangePasswordModel());
        }

        private async Task<IndexModel> GetIndexModelAsync(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var phoneNumber = await this.userManager.GetPhoneNumberAsync(user);

            IndexModel indexModel = new IndexModel
            {
                Input = new IndexModel.InputModel()
                {
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    LastName = user.LastName,
                    PhoneNumber = phoneNumber,
                },
            };

            return indexModel;
        }

        private async Task<EmailModel> GetEmailModelAsync(User user)
        {
            var email = await this.userManager.GetEmailAsync(user);

            EmailModel emailModel = new EmailModel()
            {
                Email = email,
                Input = new EmailModel.InputModel
                {
                    NewEmail = email,
                },
                IsEmailConfirmed = await this.userManager.IsEmailConfirmedAsync(user),
            };

            return emailModel;
        }
    }
}
