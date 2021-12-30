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
    public class GradesService : IGradesService
    {
        private readonly IGradesRepository gradesRepository;

        public GradesService(IGradesRepository gradesRepository)
        {
            this.gradesRepository = gradesRepository;
        }

        public async Task Add(Grade entity)
        {
            await this.gradesRepository.Add(entity);
        }

        public async Task AddRange(IEnumerable<Grade> entities)
        {
            await this.gradesRepository.AddRange(entities);
        }

        public IEnumerable<Grade> Find(Expression<Func<Grade, bool>> expression)
        {
            return this.gradesRepository.Find(expression);
        }

        public IEnumerable<Grade> GetAll()
        {
            return this.gradesRepository.GetAll();
        }

        public async Task<Grade> GetById(int? id)
        {
            return await this.gradesRepository.GetById(id);
        }

        public async Task Remove(Grade entity)
        {
            await this.gradesRepository.Remove(entity);
        }

        public async Task RemoveRange(IEnumerable<Grade> entities)
        {
            await this.gradesRepository.RemoveRange(entities);
        }

        public async Task Update(Grade entity)
        {
            await this.gradesRepository.Update(entity);
        }
    }
}
