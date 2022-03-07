using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JDC.Common.Entities;
using JDC.DataAccess.Data;
using JDC.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JDC.DataAccess.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly ApplicationDbContext context;

        public ChatRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task Add(Chat entity)
        {
            await this.context.Chats.AddAsync(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<Chat> entities)
        {
            await this.context.Chats.AddRangeAsync(entities);
            await this.context.SaveChangesAsync();
        }

        public IEnumerable<Chat> Find(Expression<Func<Chat, bool>> expression)
        {
            return this.context.Chats.Where(expression);
        }

        public IEnumerable<Chat> GetAll()
        {
            return this.context.Chats;
        }

        public async Task<Chat> GetById(int? id)
        {
            return await this.context.Chats.FindAsync(id);
        }

        public async Task Remove(Chat entity)
        {
            this.context.Chats.Remove(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task RemoveRange(IEnumerable<Chat> entities)
        {
            this.context.Chats.RemoveRange(entities);
            await this.context.SaveChangesAsync();
        }

        public async Task Update(Chat entity)
        {
            this.context.Chats.Update(entity);
            await this.context.SaveChangesAsync();
        }
    }
}
