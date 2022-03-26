using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.DataAccess.Interfaces
{
    public interface ITeacherRepository
    {
        Task<Teacher> GetById(int teacherId);

        IEnumerable<Teacher> Find(Expression<Func<Teacher, bool>> expression);

        Task Add(Teacher teacher);

        Task Remove(Teacher teacher);

        Task Update(Teacher teacher);

        Task<List<Teacher>> GetInstitutionTeachers(int institutionId);
    }
}
