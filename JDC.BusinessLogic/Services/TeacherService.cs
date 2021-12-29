using System.Collections.Generic;
using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using JDC.Common.Entities;
using JDC.DataAccess.Interfaces;

namespace JDC.BusinessLogic.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository teacherRepository;

        public TeacherService(ITeacherRepository teacherRepository)
        {
            this.teacherRepository = teacherRepository;
        }

        public async Task<List<Teacher>> GetInstitutionTeachers(int? id)
        {
            return id.HasValue ? await this.teacherRepository.GetInstitutionTeachers(id.Value) : null;
        }
    }
}
