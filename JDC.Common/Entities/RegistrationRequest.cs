using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDC.Common.Entities
{
    public class RegistrationRequest
    {
        public RegistrationRequest(string directorName, string phoneNumber, string websiteLink, string email, int confirmationCode)
        {
            this.DirectorName = directorName;
            this.PhoneNumber = phoneNumber;
            this.WebsiteLink = websiteLink;
            this.Email = email;
            this.ConfirmationCode = confirmationCode;
        }

        public int ID { get; set; }

        public string DirectorName { get; set; }

        public string PhoneNumber { get; set; }

        public string WebsiteLink { get; set; } = string.Empty;

        public string Email { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public bool EmailConfirmed { get; set; } = false;

        public int ConfirmationCode { get; set; } = -1;
    }
}
