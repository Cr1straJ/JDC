using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JDC.Common.Entities;
using JDC.DataAccess.Data;
using JDC.DataAccess.Interfaces;

namespace JDC.DataAccess.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly ApplicationDbContext context;

        public ChatRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task Add(ChatGroup entity)
        {
            await this.context.ChatGroups.AddAsync(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<ChatGroup> entities)
        {
            await this.context.ChatGroups.AddRangeAsync(entities);
            await this.context.SaveChangesAsync();
        }

        public IEnumerable<ChatGroup> Find(Expression<Func<ChatGroup, bool>> expression)
        {
            return this.context.ChatGroups.Where(expression);
        }

        public IEnumerable<ChatGroup> GetAll()
        {
            return this.context.ChatGroups;
        }

        public async Task<ChatGroup> GetById(int? id)
        {
            return await this.context.ChatGroups.FindAsync(id);
        }

        public async Task Remove(ChatGroup entity)
        {
            this.context.ChatGroups.Remove(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task RemoveRange(IEnumerable<ChatGroup> entities)
        {
            this.context.ChatGroups.RemoveRange(entities);
            await this.context.SaveChangesAsync();
        }

        public async Task Update(ChatGroup entity)
        {
            this.context.ChatGroups.Update(entity);
            await this.context.SaveChangesAsync();
        }
    }
}
