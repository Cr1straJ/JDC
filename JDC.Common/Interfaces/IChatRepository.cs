using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.Common.Interfaces
{
    public interface IChatRepository
    {
        ChatGroup GetById(int? id);

        IEnumerable<ChatGroup> GetAll();

        IEnumerable<ChatGroup> Find(Expression<Func<ChatGroup, bool>> expression);

        void Add(ChatGroup entity);

        void AddRange(IEnumerable<ChatGroup> entities);

        void Remove(ChatGroup entity);

        void RemoveRange(IEnumerable<ChatGroup> entities);

        void Update(Grade entity);
    }
}
