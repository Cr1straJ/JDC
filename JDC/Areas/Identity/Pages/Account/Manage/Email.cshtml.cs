using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using JDC.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;

namespace JDC.Areas.Identity.Pages.Account.Manage
{
    public class EmailModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;

        public EmailModel(UserManager<User> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        [Display(Name = "Текущая почта")]
        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user is null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            await this.LoadAsync(user);

            return this.Page();
        }

        public async Task<IActionResult> OnPostChangeEmailAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user is null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            if (!this.ModelState.IsValid)
            {
                await this.LoadAsync(user);
                return this.Page();
            }

            var email = await this.userManager.GetEmailAsync(user);
            if (this.Input.NewEmail != email)
            {
                var userId = await this.userManager.GetUserIdAsync(user);
                var code = await this.userManager.GenerateChangeEmailTokenAsync(user, this.Input.NewEmail);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = this.Url.Page(
                    "/Account/ConfirmEmailChange",
                    pageHandler: null,
                    values: new { userId, email = this.Input.NewEmail, code },
                    protocol: this.Request.Scheme);

                await new EmailService().SendEmailAsync(user.ShortName, this.Input.NewEmail, "Подтвердите свою электронную почту", $"Пожалуйста, подтвердите свою учетную запись, <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>нажав здесь</a>.", this.configuration);

                this.StatusMessage = "Отправлена ссылка для подтверждения изменения электронной почты. Пожалуйста, проверьте свою электронную почту.";

                return this.RedirectToPage();
            }

            this.StatusMessage = "Ваша электронная почта не изменилась.";

            return this.RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user is null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            if (!this.ModelState.IsValid)
            {
                await this.LoadAsync(user);

                return this.Page();
            }

            var userId = await this.userManager.GetUserIdAsync(user);
            var email = await this.userManager.GetEmailAsync(user);
            var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = this.Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId, code },
                protocol: this.Request.Scheme);

            await new EmailService().SendEmailAsync(user.ShortName, email, "Подтвердите свою электронную почту", $"Пожалуйста, подтвердите свою учетную запись, <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>нажав здесь</a>.", this.configuration);

            this.StatusMessage = "Письмо с подтверждением отправлено. Пожалуйста, проверьте свою электронную почту.";

            return this.RedirectToPage();
        }

        private async Task LoadAsync(User user)
        {
            var email = await this.userManager.GetEmailAsync(user);
            this.Email = email;

            this.Input = new InputModel
            {
                NewEmail = email,
            };

            this.IsEmailConfirmed = await this.userManager.IsEmailConfirmedAsync(user);
        }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Новая почта")]
            public string NewEmail { get; set; }
        }
    }
}
