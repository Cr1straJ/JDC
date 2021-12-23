using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace JDC.Areas.Account.Models
{
    public class EmailModel
    {
        [Display(Name = "Текущая почта")]
        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Новая почта")]
            public string NewEmail { get; set; }
        }
    }
}
