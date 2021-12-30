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
    public class GradesRepository : IGradesRepository
    {
        private readonly ApplicationDbContext context;

        public GradesRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task Add(Grade entity)
        {
            await this.context.Grades.AddAsync(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<Grade> entities)
        {
            await this.context.Grades.AddRangeAsync(entities);
            await this.context.SaveChangesAsync();
        }

        public IEnumerable<Grade> Find(Expression<Func<Grade, bool>> expression)
        {
            return this.context.Grades.Where(expression);
        }

        public IEnumerable<Grade> GetAll()
        {
            return this.context.Grades;
        }

        public async Task<Grade> GetById(int? id)
        {
            return await this.context.Grades.FindAsync(id);
        }

        public async Task Remove(Grade entity)
        {
            this.context.Grades.Remove(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task RemoveRange(IEnumerable<Grade> entities)
        {
            this.context.Grades.RemoveRange(entities);
            await this.context.SaveChangesAsync();
        }

        public async Task Update(Grade entity)
        {
            this.context.Grades.Update(entity);
            await this.context.SaveChangesAsync();
        }
    }
}
