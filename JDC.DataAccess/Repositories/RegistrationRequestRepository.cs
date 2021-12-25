﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JDC.Common.Entities;
using JDC.Common.Interfaces;
using JDC.DataAccess.Data;

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
            return this.context.RegistrationRequests.ToList();
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
