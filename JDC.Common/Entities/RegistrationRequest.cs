﻿using System;
using JDC.Common.Enums;

namespace JDC.Common.Entities
{
    /// <summary>
    /// Registration request entity.
    /// </summary>
    public class RegistrationRequest
    {
        /// <summary>
        /// Gets or sets a registration request id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets an institution director name.
        /// </summary>
        public string DirectorName { get; set; }

        /// <summary>
        /// Gets or sets a registration request phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets an institution website link.
        /// </summary>
        public string WebsiteLink { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets an institution email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a registration request creation date.
        /// </summary>
        public DateTime CreationDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets a value indicating whether the email is confirmed.
        /// </summary>
        public bool EmailConfirmed { get; set; } = false;

        /// <summary>
        /// Gets or sets an institution type.
        /// </summary>
        public InstituteType? InstitutionType { get; set; }

        /// <summary>
        /// Gets or sets a confirmation code.
        /// </summary>
        public int ConfirmationCode { get; set; } = -1;

        public static explicit operator User(RegistrationRequest registrationRequest)
        {
            string[] name = registrationRequest.DirectorName.Split(' ');

            return new User()
            {
                LastName = name[0],
                FirstName = name[1],
                MiddleName = name[2],
                UserName = registrationRequest.Email,
                Email = registrationRequest.Email,
                PhoneNumber = registrationRequest.PhoneNumber,
                EmailConfirmed = registrationRequest.EmailConfirmed,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 0,
            };
        }
    }
}
