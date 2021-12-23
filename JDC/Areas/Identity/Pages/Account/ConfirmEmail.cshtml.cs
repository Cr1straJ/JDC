using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JDC.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        public ConfirmEmailModel()
        {
        }

        [TempData]
        public string StatusMessage { get; set; }

        public IActionResult OnGet(string email)
        {
            if (email is null)
            {
                return this.NotFound($"Неудалось завершить подтверждение электронной почты '{email}'.");
            }

            this.StatusMessage = $"В данный момент времени ваша заявка находится на рассмотрении. В случае успешного прохождения проверки вашей заявки, на вашу электронную почту будут высланы данные для авотризации.";

            return this.Page();
        }
    }
}
