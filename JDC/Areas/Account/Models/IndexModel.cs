using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace JDC.Areas.Account.Models
{
    public class IndexModel
    {
        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

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
