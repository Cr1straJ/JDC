using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.BusinessLogic.Interfaces
{
    public interface IChatService
    {
        Task<ChatGroup> GetById(int? id);

        IEnumerable<ChatGroup> GetAll();

        IEnumerable<ChatGroup> Find(Expression<Func<ChatGroup, bool>> expression);

        Task Add(ChatGroup entity);

        Task AddRange(IEnumerable<ChatGroup> entities);

        Task Remove(ChatGroup entity);

        Task RemoveRange(IEnumerable<ChatGroup> entities);

        Task Update(ChatGroup entity);
    }
}
