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
    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationDbContext context;

        public GroupRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async void Add(StudentGroup entity)
        {
            await this.context.StudentGroups.AddAsync(entity);
            await this.context.SaveChangesAsync();
        }

        public async void AddRange(IEnumerable<StudentGroup> entities)
        {
            await this.context.StudentGroups.AddRangeAsync(entities);
            await this.context.SaveChangesAsync();
        }

        public IEnumerable<StudentGroup> Find(Expression<Func<StudentGroup, bool>> expression)
        {
            return this.context.StudentGroups.Where(expression);
        }

        public IEnumerable<StudentGroup> GetAll()
        {
            return this.context.StudentGroups;
        }

        public async Task<StudentGroup> GetById(int? id)
        {
            return await this.context.StudentGroups.FindAsync(id);
        }

        public async void Remove(StudentGroup entity)
        {
            this.context.StudentGroups.Remove(entity);
            await this.context.SaveChangesAsync();
        }

        public async void RemoveRange(IEnumerable<StudentGroup> entities)
        {
            this.context.StudentGroups.RemoveRange(entities);
            await this.context.SaveChangesAsync();
        }

        public async void Update(StudentGroup entity)
        {
            this.context.StudentGroups.Update(entity);
            await this.context.SaveChangesAsync();
        }
    }
}
