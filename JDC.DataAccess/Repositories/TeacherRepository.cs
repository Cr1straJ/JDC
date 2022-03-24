using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JDC.Common.Entities;
using JDC.DataAccess.Data;
using JDC.DataAccess.Interfaces;

namespace JDC.DataAccess.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ApplicationDbContext context;

        public TeacherRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task Add(Teacher entity)
        {
            await this.context.Teachers.AddAsync(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<Teacher> entities)
        {
            await this.context.Teachers.AddRangeAsync(entities);
            await this.context.SaveChangesAsync();
        }

        public IEnumerable<Teacher> Find(Expression<Func<Teacher, bool>> expression)
        {
            return this.context.Teachers.Where(expression);
        }

        public IEnumerable<Teacher> GetAll()
        {
            return this.context.Teachers;
        }

        public async Task<Teacher> GetById(int? id)
        {
            return await this.context.Teachers.FindAsync(id);
        }

        public async Task Remove(Teacher entity)
        {
            this.context.Teachers.Remove(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task RemoveRange(IEnumerable<Teacher> entities)
        {
            this.context.Teachers.RemoveRange(entities);
            await this.context.SaveChangesAsync();
        }

        public async Task Update(Teacher entity)
        {
            this.context.Teachers.Update(entity);
            await this.context.SaveChangesAsync();
        }

        public Task<List<Teacher>> GetInstitutionTeachers(int id)
        {
            return Task.Factory.StartNew(() =>
            {
                return this.context.Teachers.Where(teacher => teacher.InstitutionId == id).ToList();
            });
        }
    }
}
