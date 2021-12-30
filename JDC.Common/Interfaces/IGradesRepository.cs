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

        Task Add(Grade entity);

        Task AddRange(IEnumerable<Grade> entities);

        Task Remove(Grade entity);

        Task RemoveRange(IEnumerable<Grade> entities);

        Task Update(Grade entity);
    }
}
