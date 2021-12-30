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
    public class InstitutionRepository : IInstitutionRepository
    {
        private readonly ApplicationDbContext context;

        public InstitutionRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task Add(Institution entity)
        {
            await this.context.Institutions.AddAsync(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<Institution> entities)
        {
            await this.context.Institutions.AddRangeAsync(entities);
            await this.context.SaveChangesAsync();
        }

        public IEnumerable<Institution> Find(Expression<Func<Institution, bool>> expression)
        {
            return this.context.Institutions.Where(expression);
        }

        public IEnumerable<Institution> GetAll()
        {
            return this.context.Institutions;
        }

        public async Task<Institution> GetById(int? id)
        {
            return await this.context.Institutions.FindAsync(id);
        }

        public async Task Remove(Institution entity)
        {
            this.context.Institutions.Remove(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task RemoveRange(IEnumerable<Institution> entities)
        {
            this.context.Institutions.RemoveRange(entities);
            await this.context.SaveChangesAsync();
        }

        public async Task Update(Institution entity)
        {
            this.context.Institutions.Update(entity);
            await this.context.SaveChangesAsync();
        }
    }
}
