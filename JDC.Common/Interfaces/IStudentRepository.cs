using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.Common.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student> GetById(int? id);

        IEnumerable<Student> GetAll();

        IEnumerable<Student> Find(Expression<Func<Student, bool>> expression);

        Task Add(Student entity);

        Task AddRange(IEnumerable<Student> entities);

        Task Remove(Student entity);

        Task RemoveRange(IEnumerable<Student> entities);

        Task Update(Student entity);
    }
}
