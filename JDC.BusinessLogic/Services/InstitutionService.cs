using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
            await institutionRepository.Add(entity);
        }

        public async Task AddRange(IEnumerable<Institution> entities)
        {
            await institutionRepository.AddRange(entities);
        }

        public IEnumerable<Institution> Find(Expression<Func<Institution, bool>> expression)
        {
            return institutionRepository.Find(expression);
        }

        public async Task<IEnumerable<Institution>> GetAll()
        {
            return await institutionRepository.GetAll();
        }

        public async Task<Institution> GetById(int id)
        {
            return await institutionRepository.GetById(id);
        }

        public async Task Remove(Institution entity)
        {
            await institutionRepository.Remove(entity);
        }

        public async Task RemoveRange(IEnumerable<Institution> entities)
        {
            await institutionRepository.RemoveRange(entities);
        }

        public async Task Update(Institution entity)
        {
            await institutionRepository.Update(entity);
        }
    }
}
