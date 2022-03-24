using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDC.Common.Entities;
using JDC.DataAccess.Data;
using JDC.DataAccess.Interfaces;

namespace JDC.DataAccess.Repositories
{
    /// <inheritdoc/>
    public class RegistrationRequestRepository : IRegistrationRequestRepository
    {
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationRequestRepository"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        public RegistrationRequestRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <inheritdoc/>
        public async Task ConfirmEmail(int requestId)
        {
            var request = context.RegistrationRequests.FirstOrDefault(request => request.Id == requestId);

            if (request is not null)
            {
                request.EmailConfirmed = true;
                await context.SaveChangesAsync();
            }
        }

        /// <inheritdoc/>
        public async Task Create(RegistrationRequest registrationRequest)
        {
            await context.RegistrationRequests.AddAsync(registrationRequest);
            await context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task Delete(RegistrationRequest registrationRequest)
        {
            context.RegistrationRequests.Remove(registrationRequest);
            await context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public RegistrationRequest FirstOrDefault(Func<RegistrationRequest, bool> predicate)
        {
            return context.RegistrationRequests.FirstOrDefault(predicate);
        }

        /// <inheritdoc/>
        public async Task<List<RegistrationRequest>> GetAll()
        {
            var requests = this.context.RegistrationRequests.ToList()
                .Where(request => !request.EmailConfirmed && (DateTime.Now - request.CreationDate).Duration().Days >= 2);

            this.context.RegistrationRequests.RemoveRange(requests);
            await this.context.SaveChangesAsync();

            return this.context.RegistrationRequests.ToList();
        }

        /// <inheritdoc/>
        public async Task<RegistrationRequest> GetById(int id)
        {
            return await this.context.RegistrationRequests.FindAsync(id);
        }
    }
}
