using System.Collections.Generic;
using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.BusinessLogic.Interfaces
{
    public interface ISpecialityService
    {
        Task<List<Speciality>> GetInstitutionSpecialities(int id);
    }
}
