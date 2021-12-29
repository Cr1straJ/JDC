using System.Collections.Generic;
using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.BusinessLogic.Interfaces
{
    public interface ITeacherService
    {
        Task<List<Teacher>> GetInstitutionTeachers(int? id);
    }
}
