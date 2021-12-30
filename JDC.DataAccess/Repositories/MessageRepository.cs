using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JDC.Common.Entities;
using JDC.Common.Interfaces;
using JDC.DataAccess.Data;

namespace JDC.DataAccess.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext context;

        public MessageRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task Add(Message entity)
        {
            await this.context.Messages.AddAsync(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<Message> entities)
        {
            await this.context.Messages.AddRangeAsync(entities);
            await this.context.SaveChangesAsync();
        }

        public IEnumerable<Message> Find(Expression<Func<Message, bool>> expression)
        {
            return this.context.Messages.Where(expression);
        }

        public IEnumerable<Message> GetAll()
        {
            return this.context.Messages;
        }

        public async Task<Message> GetById(int? id)
        {
            return await this.context.Messages.FindAsync(id);
        }

        public async Task Remove(Message entity)
        {
            this.context.Messages.Remove(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task RemoveRange(IEnumerable<Message> entities)
        {
            this.context.Messages.RemoveRange(entities);
            await this.context.SaveChangesAsync();
        }

        public async Task Update(Message entity)
        {
            this.context.Messages.Update(entity);
            await this.context.SaveChangesAsync();
        }
    }
}
