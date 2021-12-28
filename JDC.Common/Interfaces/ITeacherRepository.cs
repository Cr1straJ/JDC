using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.Common.Interfaces
{
    public interface ITeacherRepository
    {
        Task<Teacher> GetById(int? id);

        IEnumerable<Teacher> GetAll();

        IEnumerable<Teacher> Find(Expression<Func<Teacher, bool>> expression);

        void Add(Teacher entity);

        void AddRange(IEnumerable<Teacher> entities);

        void Remove(Teacher entity);

        void RemoveRange(IEnumerable<Teacher> entities);

        void Update(Teacher entity);
    }
}
