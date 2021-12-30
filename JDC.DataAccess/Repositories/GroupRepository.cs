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
    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationDbContext context;

        public GroupRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task Add(Group entity)
        {
            await this.context.StudentGroups.AddAsync(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<Group> entities)
        {
            await this.context.StudentGroups.AddRangeAsync(entities);
            await this.context.SaveChangesAsync();
        }

        public IEnumerable<Group> Find(Expression<Func<Group, bool>> expression)
        {
            return this.context.StudentGroups.Where(expression);
        }

        public IEnumerable<Group> GetAll()
        {
            return this.context.StudentGroups;
        }

        public async Task<Group> GetById(int? id)
        {
            return await this.context.StudentGroups.FindAsync(id);
        }

        public async Task Remove(Group entity)
        {
            this.context.StudentGroups.Remove(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task RemoveRange(IEnumerable<Group> entities)
        {
            this.context.StudentGroups.RemoveRange(entities);
            await this.context.SaveChangesAsync();
        }

        public async Task Update(Group entity)
        {
            this.context.StudentGroups.Update(entity);
            await this.context.SaveChangesAsync();
        }
        
        public Task<List<Group>> GetInstitutionGroups(int id)
        {
            return Task.Factory.StartNew(() =>
            {
                return this.context.StudentGroups.Where(group => group.InstitutionId == id).ToList();
            });
        }
    }
}
