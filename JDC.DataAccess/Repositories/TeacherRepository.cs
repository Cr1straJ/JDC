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
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ApplicationDbContext context;

        public TeacherRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task Add(Teacher entity)
        {
            await context.Teachers.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public IEnumerable<Teacher> Find(Expression<Func<Teacher, bool>> expression)
        {
            return context.Teachers.Where(expression);
        }

        public async Task<Teacher> GetById(int id)
        {
            return await context.Teachers.FindAsync(id);
        }

        public async Task Remove(Teacher entity)
        {
            context.Teachers.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task Update(Teacher entity)
        {
            context.Teachers.Update(entity);
            await context.SaveChangesAsync();
        }

        public Task<List<Teacher>> GetInstitutionTeachers(int id)
        {
            return Task.Factory.StartNew(() =>
            {
                return context.Teachers
                    .Include(t => t.User)
                    .Where(teacher => teacher.InstitutionId == id)
                    .ToList();
            });
        }
    }
}
