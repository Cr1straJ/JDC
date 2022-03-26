using System.Security.Claims;
using System.Threading.Tasks;
using JDC.BusinessLogic.Models.Requests;
using JDC.Common.Entities;

namespace JDC.BusinessLogic.Interfaces
{
    /// <summary>
    /// Authentication service.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Registers user.
        /// </summary>
        /// <param name="request">Registration request information.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<bool> Register(RegistrationRequest request);

        /// <summary>
        /// Authencicates user according login form information.
        /// </summary>
        /// <param name="request">Login request information.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<User> TryLogin(LoginRequest request);

        /// <summary>
        /// Returns a flag indicating whether the specified user is a member of the given named role.
        /// </summary>
        /// <param name="user">The user whose role membership should be checked.</param>
        /// <param name="role">The name of the role to be checked.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation, containing a flag indicating whether the specified user is a member of the named role.</returns>
        Task<bool> IsInRole(User user, string role);

        /// <summary>
        /// Returns the user corresponding to the claim in the principal or null.
        /// </summary>
        /// <param name="principal">The principal which contains the user id claim.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<User> GetUser(ClaimsPrincipal principal);

        /// <summary>
        /// Signs the current user out of the application.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation, containing a flag indicating whether the specified user is a member of the named role.</returns>
        Task SignOut();
    }
}
