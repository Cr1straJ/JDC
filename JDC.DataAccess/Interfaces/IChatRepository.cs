using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.DataAccess.Interfaces
{
    public interface IChatRepository
    {
        Task<Chat> GetById(int? id);

        IEnumerable<Chat> GetAll();

        IEnumerable<Chat> Find(Expression<Func<Chat, bool>> expression);

        Task Add(Chat entity);

        Task AddRange(IEnumerable<Chat> entities);

        Task Remove(Chat entity);

        Task RemoveRange(IEnumerable<Chat> entities);

        Task Update(Chat entity);
    }
}
