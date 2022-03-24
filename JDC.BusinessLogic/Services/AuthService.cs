using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using JDC.BusinessLogic.Models.Requests;
using JDC.BusinessLogic.Utilities.EmailSender;
using JDC.BusinessLogic.Utilities.PasswordGenerator;
using JDC.Common.Entities;
using Microsoft.AspNetCore.Identity;

namespace JDC.BusinessLogic.Services
{
    /// <inheritdoc/>
    public class AuthService : IAuthService
    {
        private readonly IEmailSender emailSender;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IPasswordGenerator passwordGenerator;
        private readonly IInstitutionService institutionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        /// <param name="signInManager">Provides the APIs for user sign in.</param>
        /// <param name="passwordGenerator">Password generator.</param>
        /// <param name="userManager">Provides the APIs for managing user in persistence store.</param>
        /// <param name="emailSender">Email sender service.</param>
        /// <param name="institutionService">Institution service.</param>
        public AuthService(
            SignInManager<User> signInManager,
            IEmailSender emailSender,
            UserManager<User> userManager,
            IPasswordGenerator passwordGenerator,
            IInstitutionService institutionService)
        {
            this.institutionService = institutionService;
            this.signInManager = signInManager;
            this.passwordGenerator = passwordGenerator;
            this.userManager = userManager;
            this.emailSender = emailSender;
        }

        /// <inheritdoc/>
        public async Task<bool> Register(RegistrationRequest request)
        {
            var director = (User)request;
            var institution = new Institution(director);
            await institutionService.Add(institution);

            var password = passwordGenerator.GeneratePassword();
            var token = await userManager.GeneratePasswordResetTokenAsync(director);
            var result = await userManager.ResetPasswordAsync(director, token, password);

            var isAccept = result.Succeeded;
            if (isAccept)
            {
                string message = $"Login: {request.Email}<br/>Password: <strong>{password}</strong>";
                await emailSender.SendEmailAsync(request.DirectorName, request.Email, "Данные для входа на платформу JDC", message);
                await userManager.AddToRoleAsync(director, "Director");
            }

            return isAccept;
        }

        /// <inheritdoc/>
        public async Task<User> TryLogin(LoginRequest request)
        {
            var result = await signInManager.PasswordSignInAsync(request.Username, request.Password, request.RememberMe, lockoutOnFailure: false);

            return result.Succeeded ? await signInManager.UserManager.FindByNameAsync(request.Username) : null;
        }

        /// <inheritdoc/>
        public async Task<bool> IsInRole(User user, string role)
        {
            return await signInManager.UserManager.IsInRoleAsync(user, role);
        }
    }
}
