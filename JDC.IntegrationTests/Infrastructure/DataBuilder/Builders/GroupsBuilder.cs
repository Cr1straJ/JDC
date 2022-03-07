using System.Collections.Generic;
using System.Linq;
using JDC.IntegrationTests.Infrastructure.Database;
using JDC.IntegrationTests.Infrastructure.DataBuilder.Interfaces;
using JDC.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace JDC.IntegrationTests.Infrastructure.DataBuilder.Builders
{
    /// <inheritdoc/>
    public class GroupsBuilder : BuilderBase, IGroupsBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupsBuilder"/> class.
        /// </summary>
        public GroupsBuilder(IntegrationTestsDbContext context)
            : base(context)
        {
        }

        /// <inheritdoc/>
        public List<Group> Groups { get; private set; }

        /// <inheritdoc/>
        public IGroupsBuilder AddDefaultGroup(int institutionId, int specialityId, int teacherId)
        {
            Context.Groups.Add(new Group
            {
                InstitutionId = institutionId,
                SpecialityId = specialityId,
                TeacherId = teacherId,
            });

            return this;
        }

        /// <inheritdoc/>
        public IGroupsBuilder AddGroup(Group group)
        {
            Context.Groups.Add(group);

            return this;
        }

        /// <inheritdoc/>
        public override void Clear()
        {
            Context.Database.ExecuteSqlRaw(
                $"DELETE FROM [{nameof(Group)}s]");
        }

        /// <inheritdoc/>
        protected override void InitializeCollections()
        {
            Groups = Context.Groups.AsEnumerable().ToList();
        }
    }
}
