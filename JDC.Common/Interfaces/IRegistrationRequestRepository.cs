﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.Common.Interfaces
{
    public interface IRegistrationRequestRepository
    {
        Task<IEnumerable<RegistrationRequest>> GetAll();

        Task<RegistrationRequest> GetById(int id);

        Task Create(RegistrationRequest registrationRequest);

        Task Update(RegistrationRequest registrationRequest);

        Task Delete(RegistrationRequest registrationRequest);
    }
}
