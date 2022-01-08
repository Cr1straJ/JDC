using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDC.Common.Entities;
using JDC.DataAccess.Data;
using JDC.DataAccess.Interfaces;

namespace JDC.DataAccess.Repositories
{
    public class RegistrationRequestRepository : IRegistrationRequestRepository
    {
        private readonly ApplicationDbContext context;

        public RegistrationRequestRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task Create(RegistrationRequest registrationRequest)
        {
            await this.context.RegistrationRequests.AddAsync(registrationRequest);
            await this.context.SaveChangesAsync();
        }

        public async Task Delete(RegistrationRequest registrationRequest)
        {
            this.context.RegistrationRequests.Remove(registrationRequest);
            await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RegistrationRequest>> GetAll()
        {
            var requests = this.context.RegistrationRequests.ToList()
                .Where(request => !request.EmailConfirmed && (DateTime.Now - request.CreationDate).Duration().Days >= 2);
            
            this.context.RegistrationRequests.RemoveRange(requests);
            await this.context.SaveChangesAsync();

            return this.context.RegistrationRequests;
        }

        public async Task<RegistrationRequest> GetById(int id)
        {
            return await this.context.RegistrationRequests.FindAsync(id);
        }

        public async Task Update(RegistrationRequest registrationRequest)
        {
            this.context.RegistrationRequests.Update(registrationRequest);
            await this.context.SaveChangesAsync();
        }
    }
}
