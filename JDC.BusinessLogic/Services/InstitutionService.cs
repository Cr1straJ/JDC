using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using JDC.Common.Entities;
using JDC.DataAccess.Interfaces;

namespace JDC.BusinessLogic.Services
{
    public class InstitutionService : IInstitutionService
    {
        private readonly IInstitutionRepository institutionRepository;

        public InstitutionService(IInstitutionRepository institutionRepository)
        {
            this.institutionRepository = institutionRepository;
        }

        public async Task Add(Institution entity)
        {
            await this.institutionRepository.Add(entity);
        }

        public async Task AddRange(IEnumerable<Institution> entities)
        {
            await this.institutionRepository.AddRange(entities);
        }

        public IEnumerable<Institution> Find(Expression<Func<Institution, bool>> expression)
        {
            return this.institutionRepository.Find(expression);
        }

        public IEnumerable<Institution> GetAll()
        {
            return this.institutionRepository.GetAll();
        }

        public async Task<Institution> GetById(int? id)
        {
            return await this.institutionRepository.GetById(id);
        }

        public async Task Remove(Institution entity)
        {
            await this.institutionRepository.Remove(entity);
        }

        public async Task RemoveRange(IEnumerable<Institution> entities)
        {
            await this.institutionRepository.RemoveRange(entities);
        }

        public async Task Update(Institution entity)
        {
            await this.institutionRepository.Update(entity);
        }
    }
}
