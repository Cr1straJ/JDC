using System.ComponentModel.DataAnnotations;

namespace JDC.BusinessLogic.Models.Requests
{
    /// <summary>
    /// Login request model.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Gets or sets username.
        /// </summary>
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether remember choose.
        /// </summary>
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
