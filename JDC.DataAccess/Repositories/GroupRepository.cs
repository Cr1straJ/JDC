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

        public async Task Add(Group group)
        {
            await this.context.Groups.AddAsync(group);
            await this.context.SaveChangesAsync();
        }

        public async Task<Group> GetById(int id)
        {
            return await this.context.Groups.FindAsync(id);
        }

        public Task<List<Group>> GetInstitutionGroups(int id)
        {
            return Task.Factory.StartNew(() =>
            {
                return this.context.Groups.Where(group => group.InstitutionId == id).ToList();
            });
        }

        public async Task Update(Group group)
        {
            this.context.Groups.Update(group);
            await this.context.SaveChangesAsync();
        }
    }
}
