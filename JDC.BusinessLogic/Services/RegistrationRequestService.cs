using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using JDC.Common.Entities;
using JDC.Common.Interfaces;

namespace JDC.BusinessLogic.Services
{
    public class RegistrationRequestService : IRegistrationRequestService
    {
        private readonly IRegistrationRequestRepository registrationRequestRepository;

        public RegistrationRequestService(IRegistrationRequestRepository registrationRequestRepository)
        {
            this.registrationRequestRepository = registrationRequestRepository;
        }

        public async Task Create(RegistrationRequest registrationRequest)
        {
            await this.registrationRequestRepository.Create(registrationRequest);
        }

        public async Task Delete(RegistrationRequest registrationRequest)
        {
            await this.Delete(registrationRequest);
        }

        public async Task<IEnumerable<RegistrationRequest>> GetAll()
        {
            return await this.registrationRequestRepository.GetAll();
        }

        public async Task<RegistrationRequest> GetById(int id)
        {
            return await this.registrationRequestRepository.GetById(id);
        }

        public async Task Update(RegistrationRequest registrationRequest)
        {
            await this.registrationRequestRepository.Update(registrationRequest);
        }
    }
}
