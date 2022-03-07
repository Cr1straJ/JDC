using System.Collections.Generic;
using System.Linq;
using JDC.Common.Entities;
using JDC.IntegrationTests.Infrastructure.Database;
using JDC.IntegrationTests.Infrastructure.DataBuilder.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JDC.IntegrationTests.Infrastructure.DataBuilder.Builders
{
    /// <inheritdoc/>
    public class TeachersBuilder : BuilderBase, ITeachersBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TeachersBuilder"/> class.
        /// </summary>
        public TeachersBuilder(IntegrationTestsDbContext context)
            : base(context)
        {
        }

        public List<Teacher> Teachers { get; private set; }

        public ITeachersBuilder AddDefaultTeacher()
        {
            Context.Teachers.Add(new Teacher());

            return this;
        }

        public ITeachersBuilder AddTeacher(Teacher teacher)
        {
            return this;
        }

        public override void Clear()
        {
            Context.Database.ExecuteSqlRaw(
                $"DELETE FROM [{nameof(Teacher)}s]");
        }

        protected override void InitializeCollections()
        {
            Teachers = Context.Teachers.AsEnumerable().ToList();
        }
    }
}
