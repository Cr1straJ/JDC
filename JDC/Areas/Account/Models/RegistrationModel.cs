using System.ComponentModel.DataAnnotations;

namespace JDC.Areas.Account.Models
{
    public class RegistrationModel
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
