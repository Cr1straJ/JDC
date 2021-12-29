using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using JDC.Common.Entities;
using JDC.Common.Interfaces;

namespace JDC.BusinessLogic.Services
{
    public class InstitutionService : IInstitutionService
    {
        private readonly IInstitutionRepository institutionRepository;

        public InstitutionService(IInstitutionRepository institutionRepository)
        {
            this.institutionRepository = institutionRepository;
        }

        public async Task Add(EInstitution entity)
        {
            await this.institutionRepository.Add(entity);
        }

        public async Task AddRange(IEnumerable<EInstitution> entities)
        {
            await this.institutionRepository.AddRange(entities);
        }

        public IEnumerable<EInstitution> Find(Expression<Func<EInstitution, bool>> expression)
        {
            return this.institutionRepository.Find(expression);
        }

        public IEnumerable<EInstitution> GetAll()
        {
            return this.institutionRepository.GetAll();
        }

        public async Task<EInstitution> GetById(int? id)
        {
            return await this.institutionRepository.GetById(id);
        }

        public async Task Remove(EInstitution entity)
        {
            await this.institutionRepository.Remove(entity);
        }

        public async Task RemoveRange(IEnumerable<EInstitution> entities)
        {
            await this.institutionRepository.RemoveRange(entities);
        }

        public async Task Update(EInstitution entity)
        {
            await this.institutionRepository.Update(entity);
        }
    }
}
