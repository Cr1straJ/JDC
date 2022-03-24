using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.DataAccess.Interfaces
{
    /// <summary>
    /// Registration request repository.
    /// </summary>
    public interface IRegistrationRequestRepository
    {
        /// <summary>
        /// Gets all registration requests.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<List<RegistrationRequest>> GetAll();

        /// <summary>
        /// Gets registration request by Id.
        /// </summary>
        /// <param name="requestId">Registration request id.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<RegistrationRequest> GetById(int requestId);

        /// <summary>
        /// Returns the first element of the sequence that satisfies a condition or a default value if no such element is found.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>A <see cref="RegistrationRequest"/> model or a default value if no such element is found.</returns>
        RegistrationRequest FirstOrDefault(Func<RegistrationRequest, bool> predicate);

        /// <summary>
        /// Confirm registration request email by Id.
        /// </summary>
        /// <param name="requestId">Registration request id.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task ConfirmEmail(int requestId);

        /// <summary>
        /// Creates registration request.
        /// </summary>
        /// <param name="request">Registration request information.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Create(RegistrationRequest request);

        /// <summary>
        /// Deletes a registration request.
        /// </summary>
        /// <param name="request">Registration request information.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Delete(RegistrationRequest request);
    }
}
