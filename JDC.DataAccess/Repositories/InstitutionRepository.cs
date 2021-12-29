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
    public class InstitutionRepository : IInstitutionRepository
    {
        private readonly ApplicationDbContext context;

        public InstitutionRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task Add(EInstitution entity)
        {
            await this.context.EInstitutions.AddAsync(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<EInstitution> entities)
        {
            await this.context.EInstitutions.AddRangeAsync(entities);
            await this.context.SaveChangesAsync();
        }

        public IEnumerable<EInstitution> Find(Expression<Func<EInstitution, bool>> expression)
        {
            return this.context.EInstitutions.Where(expression);
        }

        public IEnumerable<EInstitution> GetAll()
        {
            return this.context.EInstitutions;
        }

        public async Task<EInstitution> GetById(int? id)
        {
            return await this.context.EInstitutions.FindAsync(id);
        }

        public async Task Remove(EInstitution entity)
        {
            this.context.EInstitutions.Remove(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task RemoveRange(IEnumerable<EInstitution> entities)
        {
            this.context.EInstitutions.RemoveRange(entities);
            await this.context.SaveChangesAsync();
        }

        public async Task Update(EInstitution entity)
        {
            this.context.EInstitutions.Update(entity);
            await this.context.SaveChangesAsync();
        }
    }
}
