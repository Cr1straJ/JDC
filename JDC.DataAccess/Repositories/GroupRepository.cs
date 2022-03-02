using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JDC.Common.Entities;
using JDC.DataAccess.Data;
using JDC.DataAccess.Interfaces;

namespace JDC.DataAccess.Repositories
{
    /// <inheritdoc/>
    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupRepository"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        public GroupRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <inheritdoc/>
        public async Task Add(Group group)
        {
            await context.StudentGroups.AddAsync(group);
            await context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<Group> GetById(int groupId)
        {
            return await context.StudentGroups.FindAsync(groupId);
        }

        /// <inheritdoc/>
        public async Task Remove(Group group)
        {
            context.StudentGroups.Remove(group);
            await context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task Update(Group group)
        {
            context.StudentGroups.Update(group);
            await context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public Task<List<Group>> GetInstitutionGroups(int institutionId)
        {
            return Task.Factory.StartNew((Func<List<Group>>)(() =>
            {
                return context.StudentGroups.Where(group => group.InstitutionId == institutionId).ToList();
            });
        }
    }
}
