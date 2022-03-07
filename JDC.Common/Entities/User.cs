using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace JDC.Common.Entities
{
    /// <summary>
    /// User entity.
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// Gets or sets the user's last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the user's first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the user's middle name.
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets a link to the user's avatar.
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// Gets or sets the user's sex.
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// Gets or sets an institution id to which the user belongs.
        /// </summary>
        public int InstitutionId { get; set; }

        /// <summary>
        /// Gets or sets an institution to which the user belongs.
        /// </summary>
        public Institution Institution { get; set; }

        /// <summary>
        /// Gets or sets a country where the student lives.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets a city where the student lives.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets a street where the student lives.
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets a house where the student lives.
        /// </summary>
        public string House { get; set; }

        /// <summary>
        /// Gets or sets an apartment where the student lives.
        /// </summary>
        public string Apartment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Chat> Chats { get; set; }
 
        /// <summary>
        /// Gets the user's address.
        /// </summary>
        [NotMapped]
        public string Address => $"{this.City}, {this.Country}";

        /// <summary>
        /// Gets the user's short name.
        /// </summary>
        /// <example>
        /// Ivanov I. I.
        /// </example>
        [NotMapped]
        public string ShortName => $"{this.LastName} {this.FirstName?.ToUpper()[0]}. {this.MiddleName?.ToUpper()[0]}.";

        /// <summary>
        /// Gets the user's full name.
        /// </summary>
        /// <example>
        /// Ivanov Ivan Ivanovich.
        /// </example>
        [NotMapped]
        public string FullName => $"{this.LastName} {this.FirstName} {this.MiddleName}";

        /// <summary>
        /// Gets the user's long name.
        /// </summary>
        /// <example>
        /// Ivanov Ivan.
        /// </example>
        [NotMapped]
        public string LongName => $"{this.LastName} {this.FirstName}";

        /// <summary>
        /// Verifies if the name of the user is the same as the source.
        /// </summary>
        /// <param name="name">The source user name.</param>
        /// <returns>true if the name of the user is the same as the source, false otherwise.</returns>
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
