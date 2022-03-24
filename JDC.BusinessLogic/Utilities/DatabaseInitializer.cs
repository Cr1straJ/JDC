using System.Collections.Generic;
using System.Threading.Tasks;
using JDC.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace JDC.BusinessLogic.Utilities
{
    /// <summary>
    /// Database initializer.
    /// </summary>
    public class DatabaseInitializer
    {
        private static readonly List<string> Roles = new ()
        {
            "Admin",
            "Director",
            "Teacher",
            "Parent",
            "Student",
        };

        /// <summary>
        /// Creates user roles and Admin.
        /// </summary>
        /// <param name="userManager">Provides the APIs for managing user in a persistence store.</param>
        /// <param name="roleManager">Provides the APIs for managing roles in a persistence store.</param>
        /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task Initialize(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            string adminEmail = configuration["Admin:Email"];
            string adminPassword = configuration["Admin:Password"];

            foreach (var role in Roles)
            {
                if (await roleManager.FindByNameAsync(role) is null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            if (await userManager.FindByNameAsync(adminEmail) is null)
            {
                User admin = new User
                {
                    LastName = "Admin",
                    FirstName = "Admin",
                    MiddleName = "Admin",
                    UserName = "Admin",
                    Email = adminEmail,
                    PhoneNumber = "+375441111111",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                };
                IdentityResult result = await userManager.CreateAsync(admin, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
