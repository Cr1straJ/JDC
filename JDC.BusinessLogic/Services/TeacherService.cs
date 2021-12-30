using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using JDC.Common.Entities;
using JDC.Common.Interfaces;
using JDC.DataAccess.Interfaces;


namespace JDC.BusinessLogic.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository teacherRepository;

        public TeacherService(ITeacherRepository teacherRepository)
        {
            this.teacherRepository = teacherRepository;
        }

        public async Task Add(Teacher entity)
        {
            await this.teacherRepository.Add(entity);
        }

        public async Task AddRange(IEnumerable<Teacher> entities)
        {
            await this.teacherRepository.AddRange(entities);
        }

        public IEnumerable<Teacher> Find(Expression<Func<Teacher, bool>> expression)
        {
            return this.teacherRepository.Find(expression);
        }

        public IEnumerable<Teacher> GetAll()
        {
            return this.teacherRepository.GetAll();
        }

        public async Task<Teacher> GetById(int? id)
        {
            return await this.teacherRepository.GetById(id);
        }

        public async Task Remove(Teacher entity)
        {
            await this.teacherRepository.Remove(entity);
        }

        public async Task RemoveRange(IEnumerable<Teacher> entities)
        {
            await this.teacherRepository.RemoveRange(entities);
        }

        public async Task Update(Teacher entity)
        {
            await this.teacherRepository.Update(entity);
        }
        
        public async Task<List<Teacher>> GetInstitutionTeachers(int id)
        {
            return await this.teacherRepository.GetInstitutionTeachers(id);
        }
    }
}
