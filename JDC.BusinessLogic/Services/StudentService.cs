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
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository sudentRepository;

        public StudentService(IStudentRepository sudentRepository)
        {
            this.sudentRepository = sudentRepository;
        }

        public async Task Add(Student entity)
        {
            await this.sudentRepository.Add(entity);
        }

        public async Task AddRange(IEnumerable<Student> entities)
        {
            await this.sudentRepository.AddRange(entities);
        }

        public IEnumerable<Student> Find(Expression<Func<Student, bool>> expression)
        {
            return this.sudentRepository.Find(expression);
        }

        public IEnumerable<Student> GetAll()
        {
            return this.sudentRepository.GetAll();
        }

        public async Task<Student> GetById(int? id)
        {
            return await this.sudentRepository.GetById(id);
        }

        public async Task Remove(Student entity)
        {
            await this.sudentRepository.Remove(entity);
        }

        public async Task RemoveRange(IEnumerable<Student> entities)
        {
            await this.sudentRepository.RemoveRange(entities);
        }

        public async Task Update(Student entity)
        {
            await this.sudentRepository.Update(entity);
        }
    }
}
