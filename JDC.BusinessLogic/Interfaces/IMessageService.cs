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

        void Add(Message entity);

        void AddRange(IEnumerable<Message> entities);

        void Remove(Message entity);

        void RemoveRange(IEnumerable<Message> entities);

        void Update(Message entity);
    }
}
