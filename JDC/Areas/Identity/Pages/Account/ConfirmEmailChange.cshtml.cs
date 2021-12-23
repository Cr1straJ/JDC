using System.Text;
using System.Threading.Tasks;
using JDC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace JDC.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailChangeModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public ConfirmEmailChangeModel(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string email, string code)
        {
            if (userId == null || email == null || code == null)
            {
                return this.RedirectToPage("/Index");
            }

            var user = await this.userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await this.userManager.ChangeEmailAsync(user, email, code);
            if (!result.Succeeded)
            {
                this.StatusMessage = "Ошибка при изменении электронной почты.";
                return this.Page();
            }

            await this.signInManager.RefreshSignInAsync(user);
            this.StatusMessage = "Спасибо за подтверждение изменения электронной почты.";
            return this.Page();
        }
    }
}
