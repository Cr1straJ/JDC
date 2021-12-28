using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JDC.Common.Entities;
using JDC.DataAccess.Data;
using JDC.DataAccess.Interfaces;

namespace JDC.DataAccess.Repositories
{
    public class GradeRepository : IGradeRepository
    {
        private readonly ApplicationDbContext context;

        public GradeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task Add(Grade grade)
        {
            await this.context.Grades.AddAsync(grade);
            await this.context.SaveChangesAsync();
        }

        public async Task<Grade> GetById(int id)
        {
            return await this.context.Grades.FindAsync(id);
        }

        public async Task Update(Grade grade)
        {
            this.context.Grades.Update(grade);
            await this.context.SaveChangesAsync();
        }
    }
}
