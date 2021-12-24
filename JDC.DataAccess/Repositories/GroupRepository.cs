using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDC.Common.Entities;
using JDC.DataAccess.Data;
using JDC.DataAccess.Interfaces;

namespace JDC.DataAccess.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationDbContext context;

        public GroupRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Task<List<Group>> GetInstitutionGroups(int id)
        {
            return Task.Factory.StartNew(() =>
            {
                return this.context.Groups.Where(group => group.InstitutionId == id).ToList();
            });
        }
    }
}
