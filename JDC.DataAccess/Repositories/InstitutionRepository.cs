using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JDC.Common.Entities;
using JDC.DataAccess.Data;
using JDC.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            await context.Institutions.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<Institution> entities)
        {
            await context.Institutions.AddRangeAsync(entities);
            await context.SaveChangesAsync();
        }

        public IEnumerable<Institution> Find(Expression<Func<Institution, bool>> expression)
        {
            return context.Institutions.Where(expression);
        }

        public async Task<Institution> GetById(int? id)
        {
            return await context.Institutions.FindAsync(id);
        }

        public async Task Remove(Institution entity)
        {
            context.Institutions.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task RemoveRange(IEnumerable<Institution> entities)
        {
            context.Institutions.RemoveRange(entities);
            await context.SaveChangesAsync();
        }

        public async Task Update(Institution entity)
        {
            context.Institutions.Update(entity);
            await context.SaveChangesAsync();
        }

        public Task<List<Institution>> GetAll()
        {
            return context.Institutions.ToListAsync();
        }
    }
}
