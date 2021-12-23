using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using JDC.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace JDC.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel
    {
        private readonly ApplicationDbContext context;

        public RegisterConfirmationModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public string ReturnUrl { get; set; }

        public bool DisplayConfirmAccountLink { get; set; }

        public string EmailConfirmationUrl { get; set; }

        public IActionResult OnGet(int id, string email = null, string returnUrl = null)
        {
            this.Input.Id = id;
            this.Input.Email = email;
            this.ReturnUrl = returnUrl;
            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= this.Url.Content("~/");

            var registrationRequest = this.Input.Id == -1 ? await this.context.RegistrationRequests.FirstOrDefaultAsync(m => m.Email.Equals(this.Input.Email)) :
                await this.context.RegistrationRequests.FirstOrDefaultAsync(m => m.ID == this.Input.Id);

            if (!Equals(registrationRequest.ConfirmationCode, int.Parse(this.Input.ConfirmationCode)))
            {
                this.ModelState.AddModelError(string.Empty, $"Неверный код подтверждения!");
            }

            if (this.ModelState.IsValid)
            {
                registrationRequest.EmailConfirmed = true;
                await this.context.SaveChangesAsync();
                return this.RedirectToPage("ConfirmEmail", new { email = registrationRequest.Email });
            }

            return this.Page();
        }

        public class InputModel
        {
            public int Id { get; set; } = -1;

            [Required]
            [EmailAddress]
            [Display(Name = "Адрес электронной почты")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Код подтверждения")]
            public string ConfirmationCode { get; set; }
        }
    }
}
