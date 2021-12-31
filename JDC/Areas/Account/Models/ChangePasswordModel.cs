﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace JDC.Areas.Account.Models
{
    public class ChangePasswordModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [DataType(DataType.Password)]
            [Display(Name = "Текущий пароль")]
            public string OldPassword { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Новый пароль")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Повторите новый пароль")]
            [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }
    }
}
