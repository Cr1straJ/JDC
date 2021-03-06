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
    public class SpecialityRepository : ISpecialityRepository
    {
        private readonly ApplicationDbContext context;

        public SpecialityRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Task<List<Speciality>> GetInstitutionSpecialities(int id)
        {
            return Task.Factory.StartNew(() =>
            {
                return this.context.Specialities.ToList();
            });
        }
    }
}
