using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.BusinessLogic.Interfaces
{
    public interface IMessageService
    {
        Task<Message> GetById(int? id);

        IEnumerable<Message> GetAll();

        IEnumerable<Message> Find(Expression<Func<Message, bool>> expression);

        Task Add(Message entity);

        Task AddRange(IEnumerable<Message> entities);

        Task Remove(Message entity);

        Task RemoveRange(IEnumerable<Message> entities);

        Task Update(Message entity);
    }
}
