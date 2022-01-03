using System.Collections.Generic;
using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.BusinessLogic.Interfaces
{
    public interface IRegistrationRequestService
    {
        Task<List<RegistrationRequest>> GetAll();

        Task<RegistrationRequest> GetById(int id);

        Task Accept(int id);

        Task Create(RegistrationRequest registrationRequest);

        Task Update(RegistrationRequest registrationRequest);

        Task Delete(RegistrationRequest registrationRequest);
    }
}
