using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.Common.Interfaces
{
    public interface IInstitutionRepository
    {
        EInstitution GetById(int? id);

        IEnumerable<EInstitution> GetAll();

        IEnumerable<EInstitution> Find(Expression<Func<EInstitution, bool>> expression);

        void Add(EInstitution entity);

        void AddRange(IEnumerable<EInstitution> entities);

        void Remove(EInstitution entity);

        void RemoveRange(IEnumerable<EInstitution> entities);

        void Update(EInstitution entity);
    }
}
