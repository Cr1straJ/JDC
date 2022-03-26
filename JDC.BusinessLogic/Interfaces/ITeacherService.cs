using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.BusinessLogic.Interfaces
{
    public interface ITeacherService
    {
        Task<List<Teacher>> GetInstitutionTeachers(int id);

        Task<Teacher> GetById(int id);

        IEnumerable<Teacher> Find(Expression<Func<Teacher, bool>> expression);

        Task Add(Teacher teacher);

        Task Remove(Teacher teacher);

        Task Update(Teacher teacher);
    }
}
