using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace JDC.Areas.Account.Models
{
    public class DeletePersonalDataModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public bool RequirePassword { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Пароль")]
            public string Password { get; set; }
        }
    }
}
