using System.Collections.Generic;
using JDC.Common.Entities;

namespace JDC.IntegrationTests.Infrastructure.DataBuilder.Interfaces
{
    /// <summary>
    /// Teachers builder.
    /// </summary>
    public interface ITeachersBuilder : IBuilder
    {
        /// <summary>
        /// Teacher collection.
        /// </summary>
        List<Teacher> Teachers { get; }

        /// <summary>
        /// Adds teacher.
        /// </summary>
        /// <param name="teacher">Teacher.</param>
        ITeachersBuilder AddTeacher(Teacher teacher);

        /// <summary>
        /// Adds default teacher.
        /// </summary>
        ITeachersBuilder AddDefaultTeacher();
    }
}
