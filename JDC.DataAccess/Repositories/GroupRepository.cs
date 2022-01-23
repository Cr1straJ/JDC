using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JDC.Common.Entities;
using JDC.DataAccess.Data;
using JDC.DataAccess.Interfaces;

namespace JDC.DataAccess.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationDbContext context;

        public GroupRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task Add(Group entity)
        {
            await this.context.Groups.AddAsync(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<Group> entities)
        {
            await this.context.Groups.AddRangeAsync(entities);
            await this.context.SaveChangesAsync();
        }

        public IEnumerable<Group> Find(Expression<Func<Group, bool>> expression)
        {
            return this.context.Groups.Where(expression);
        }

        public IEnumerable<Group> GetAll()
        {
            return this.context.Groups;
        }

        public async Task<Group> GetById(int? id)
        {
            return await this.context.Groups.FindAsync(id);
        }

        public async Task Remove(Group entity)
        {
            this.context.Groups.Remove(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task RemoveRange(IEnumerable<Group> entities)
        {
            this.context.Groups.RemoveRange(entities);
            await this.context.SaveChangesAsync();
        }

        public async Task Update(Group entity)
        {
            this.context.Groups.Update(entity);
            await this.context.SaveChangesAsync();
        }

        public Task<List<Group>> GetInstitutionGroups(int id)
        {
            return Task.Factory.StartNew((Func<List<Group>>)(() =>
            {
                return Queryable.Where<Group>(this.context.Groups, (Expression<Func<Group, bool>>)(group => (bool)(group.InstitutionId == id))).ToList();
            }));
        }
    }
}
