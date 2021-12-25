using System.Collections.Generic;
using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.DataAccess.Interfaces
{
    public interface ITeacherRepository
    {
        Task<List<Teacher>> GetInstitutionTeachers(int id);
    }
}
