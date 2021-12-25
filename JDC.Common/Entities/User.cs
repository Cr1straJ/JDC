using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace JDC.Common.Entities
{
    public class User : IdentityUser
    {
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string Avatar { get; set; }

        public string Sex { get; set; }

        public int InstitutionId { get; set; }

        public Institution Institution { get; set; }

        [NotMapped]
        public string ShortName => $"{this.LastName} {this.FirstName?.ToUpper()[0]}. {this.MiddleName?.ToUpper()[0]}.";

        [NotMapped]
        public string LongName => $"{this.LastName} {this.FirstName} {this.MiddleName}";

        public bool IsEqualsName(string name)
            => name.Split(' ', System.StringSplitOptions.RemoveEmptyEntries).Any(word => this.IsContainsText(word));

        private bool IsContainsText(string text)
        {
            return this.LastName.Contains(text, System.StringComparison.CurrentCultureIgnoreCase) ||
                this.FirstName.Contains(text, System.StringComparison.CurrentCultureIgnoreCase) ||
                this.MiddleName.Contains(text, System.StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
