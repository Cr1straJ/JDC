using System.Collections.Generic;
using System.Linq;
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

        public Task<List<Teacher>> GetInstitutionTeachers(int id)
        {
            return Task.Factory.StartNew(() =>
            {
                return this.context.Teachers.Where(teacher => teacher.User.InstitutionId == id).ToList();
            });
        }
    }
}
