using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.BusinessLogic.Interfaces
{
    public interface IInstitutionService
    {
        Task<EInstitution> GetById(int? id);

        IEnumerable<EInstitution> GetAll();

        IEnumerable<EInstitution> Find(Expression<Func<EInstitution, bool>> expression);

        Task Add(EInstitution entity);

        Task AddRange(IEnumerable<EInstitution> entities);

        Task Remove(EInstitution entity);

        Task RemoveRange(IEnumerable<EInstitution> entities);

        Task Update(EInstitution entity);
    }
}
