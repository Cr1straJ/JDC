using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.Common.Interfaces
{
    public interface IGradesRepository
    {
        Task<Grade> GetById(int? id);

        IEnumerable<Grade> GetAll();

        IEnumerable<Grade> Find(Expression<Func<Grade, bool>> expression);

        void Add(Grade entity);

        void AddRange(IEnumerable<Grade> entities);

        void Remove(Grade entity);

        void RemoveRange(IEnumerable<Grade> entities);

        void Update(Grade entity);
    }
}
