using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.DataAccess.Interfaces
{
    public interface IGroupRepository
    {
        Task<Group> GetById(int? id);

        IEnumerable<Group> GetAll();

        IEnumerable<Group> Find(Expression<Func<Group, bool>> expression);

        Task Add(Group entity);

        Task AddRange(IEnumerable<Group> entities);

        Task Remove(Group entity);

        Task RemoveRange(IEnumerable<Group> entities);

        Task Update(Group entity);

        Task<List<Group>> GetInstitutionGroups(int id);
    }
}
