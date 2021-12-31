using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.DataAccess.Interfaces
{
    public interface IInstitutionRepository
    {
        Task<Institution> GetById(int? id);

        IEnumerable<Institution> GetAll();

        IEnumerable<Institution> Find(Expression<Func<Institution, bool>> expression);

        Task Add(Institution entity);

        Task AddRange(IEnumerable<Institution> entities);

        Task Remove(Institution entity);

        Task RemoveRange(IEnumerable<Institution> entities);

        Task Update(Institution entity);
    }
}
