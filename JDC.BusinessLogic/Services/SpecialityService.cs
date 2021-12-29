using System.Collections.Generic;
using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using JDC.Common.Entities;
using JDC.DataAccess.Interfaces;

namespace JDC.BusinessLogic.Services
{
    public class SpecialityService : ISpecialityService
    {
        private readonly ISpecialityRepository specialityRepository;

        public SpecialityService(ISpecialityRepository specialityRepository)
        {
            this.specialityRepository = specialityRepository;
        }

        public async Task<List<Speciality>> GetInstitutionSpecialities(int? id)
        {
            return id.HasValue ? await this.specialityRepository.GetInstitutionSpecialities(id.Value) : null;
        }
    }
}
