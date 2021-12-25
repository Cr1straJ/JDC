using System.Collections.Generic;
using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.DataAccess.Interfaces
{
    public interface ISpecialityRepository
    {
        Task<List<Speciality>> GetInstitutionSpecialities(int id);
    }
}
