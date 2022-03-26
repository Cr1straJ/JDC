using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDC.Common.Entities;
using JDC.DataAccess.Data;
using JDC.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            await context.Groups.AddAsync(group);

            var teacher = await context.Teachers
                .FirstOrDefaultAsync(teacher => teacher.Id == group.TeacherId);

            if (teacher is not null)
            {
                teacher.Group = group;
            }

            await context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<Group> GetById(int groupId)
        {
            return await context.Groups.FindAsync(groupId);
        }

        /// <inheritdoc/>
        public async Task Remove(Group group)
        {
            context.Groups.Remove(group);
            await context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task Update(Group group)
        {
            context.Groups.Update(group);
            await context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public Task<List<Group>> GetInstitutionGroups(int institutionId)
        {
            return Task.Factory.StartNew(() =>
            {
                return context.Groups
                    .Include(g => g.Teacher)
                    .ThenInclude(t => t.User)
                    .Where(group => group.InstitutionId == institutionId)
                    .ToList();
            });
        }
    }
}
