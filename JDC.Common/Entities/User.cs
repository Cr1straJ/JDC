using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace JDC.Common.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public bool Sex { get; set; }

        [NotMapped]
        public string ShortName => $"{this.LastName} {this.FirstName?.ToUpper()[0]}. {this.MiddleName?.ToUpper()[0]}.";
    }
}
