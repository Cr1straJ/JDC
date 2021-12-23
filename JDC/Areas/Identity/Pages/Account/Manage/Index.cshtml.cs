using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using JDC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JDC.Areas.Identity.Pages.Account.Manage
{
    public class Index : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public Index(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

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

        public async Task<IActionResult> OnPostAsync()
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

            var phoneNumber = await this.userManager.GetPhoneNumberAsync(user);

            if (this.Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await this.userManager.SetPhoneNumberAsync(user, this.Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    this.StatusMessage = "Unexpected error when trying to set phone number.";
                    return this.RedirectToPage();
                }
            }

            user.LastName = this.Input.LastName;
            user.FirstName = this.Input.FirstName;
            user.MiddleName = this.Input.MiddleName;

            // user.Sex = this.Input.Sex;
            await this.userManager.UpdateAsync(user);

            await this.signInManager.RefreshSignInAsync(user);
            this.StatusMessage = "Your profile has been updated";

            return this.RedirectToPage();
        }

        private async Task LoadAsync(User user)
        {
            var phoneNumber = await this.userManager.GetPhoneNumberAsync(user);

            this.Input = new InputModel
            {
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                PhoneNumber = phoneNumber,
            };
        }

        public class InputModel
        {
            [Display(Name = "Имя")]
            public string FirstName { get; set; }

            [Display(Name = "Отчество")]
            public string MiddleName { get; set; }

            [Display(Name = "Фамилия")]
            public string LastName { get; set; }

            [Display(Name = "Пол")]
            public string Sex { get; set; }

            [Phone]
            [Display(Name = "Номер телефона")]
            public string PhoneNumber { get; set; }
        }
    }
}
