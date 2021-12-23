using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using JDC.Data;
using JDC.Models;
using JDC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace JDC.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly ILogger<RegisterModel> logger;
        private readonly ApplicationDbContext context;
        private readonly IConfiguration configuration;

        public RegisterModel(ApplicationDbContext context, ILogger<RegisterModel> logger, IConfiguration configuration)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl = null) => this.ReturnUrl = returnUrl;

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= this.Url.Content("~/");

            if (this.ModelState.IsValid)
            {
                int confirmationCode = new Random().Next(1000, 10000);
                RegistrationRequest registrationRequest = new (this.Input.DirectorName, this.Input.PhoneNumber, this.Input.WebsiteLink, this.Input.Email, confirmationCode);

                await this.context.AddAsync(registrationRequest);
                await this.context.SaveChangesAsync();

                await new EmailService().SendEmailAsync(this.Input.DirectorName, this.Input.Email, "Подтверждение регистрации учреждения", $"Ваш код: {confirmationCode}", this.configuration);
                this.logger.LogInformation("The letter is sent");
                return this.RedirectToPage("RegisterConfirmation", new { id = registrationRequest.ID, email = this.Input.Email, returnUrl });
            }

            return this.Page();
        }

        public class InputModel
        {
            [Required]
            [Display(Name = "ФИО директора")]
            public string DirectorName { get; set; }

            [Required]
            [Phone]
            [Display(Name = "Номер телефона для связи")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Ссылка на сайт учреждения (если имеется)")]
            public string WebsiteLink { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Я принимаю условия Соглашения")]
            public bool AcceptTermsOfAgreement { get; set; }
        }
    }
}
