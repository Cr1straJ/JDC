using System.Collections.Generic;
using JDC.Common.Entities;

namespace JDC.IntegrationTests.Infrastructure.DataBuilder.Interfaces
{
    /// <summary>
    /// Groups builder.
    /// </summary>
    public interface IGroupsBuilder : IBuilder
    {
        /// <summary>
        /// Group collection.
        /// </summary>
        List<Group> Groups { get; }

        /// <summary>
        /// Adds group.
        /// </summary>
        /// <param name="group">Group.</param>
        IGroupsBuilder AddGroup(Group group);

        /// <summary>
        /// Adds default group.
        /// </summary>
        /// <param name="institutionId">Institution Id.</param>
        /// <param name="specialityId">Speciality Id.</param>
        /// <param name="teacherId">Teacher Id.</param>
        IGroupsBuilder AddDefaultGroup(int institutionId, int specialityId, int teacherId);
    }
}
