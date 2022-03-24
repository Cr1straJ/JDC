using System.ComponentModel.DataAnnotations;

namespace JDC.Areas.Account.Models
{
    public class RegisterConfirmationModel
    {
        public int? Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Код подтверждения")]
        public string ConfirmationCode { get; set; }
    }
}
