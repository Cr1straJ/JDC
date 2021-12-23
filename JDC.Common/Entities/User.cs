using Microsoft.AspNetCore.Identity;

namespace JDC.Common.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public bool Sex { get; set; }
    }
}
