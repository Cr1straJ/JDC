using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.BusinessLogic.Interfaces
{
    public interface ITeacherService
    {
        Task<List<Teacher>> GetInstitutionTeachers(int? id);

        Task<Teacher> GetById(int? id);

        IEnumerable<Teacher> GetAll();

        IEnumerable<Teacher> Find(Expression<Func<Teacher, bool>> expression);

        Task Add(Teacher entity);

        Task AddRange(IEnumerable<Teacher> entities);

        Task Remove(Teacher entity);

        Task RemoveRange(IEnumerable<Teacher> entities);

        Task Update(Teacher entity);
    }
}
