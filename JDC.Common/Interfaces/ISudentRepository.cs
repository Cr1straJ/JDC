using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.Common.Interfaces
{
    public interface ISudentRepository
    {
        Task<Student> GetById(int? id);

        IEnumerable<Student> GetAll();

        IEnumerable<Student> Find(Expression<Func<Student, bool>> expression);

        void Add(Student entity);

        void AddRange(IEnumerable<Student> entities);

        void Remove(Student entity);

        void RemoveRange(IEnumerable<Student> entities);

        void Update(Student entity);
    }
}
