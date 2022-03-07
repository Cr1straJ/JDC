using JDC.IntegrationTests.Infrastructure.Database;
using JDC.IntegrationTests.Infrastructure.DataBuilder.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JDC.IntegrationTests.Infrastructure.DataBuilder.Builders
{
    /// <inheritdoc/>
    public class UnitOfWorkBuilder : IDataBuilder
    {
        private readonly IntegrationTestsDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWorkBuilder"/> class.
        /// </summary>
        public UnitOfWorkBuilder(IntegrationTestsDbContext context)
        {
            this.context = context;
            InitializeBuilders();
        }

        /// <inheritdoc/>
        public IGroupsBuilder GroupsBuilder { get; private set; }

        /// <inheritdoc/>
        public IInstitutionsBuilder InstitutionsBuilder { get; private set; }

        /// <inheritdoc/>
        public ISpecialitiesBuilder SpecialitiesBuilder { get; private set; }

        /// <inheritdoc/>
        public ITeachersBuilder TeachersBuilder { get; private set; }

        /// <inheritdoc/>
        public void Clear()
        {
            GroupsBuilder.Clear();
            SpecialitiesBuilder.Clear();
            TeachersBuilder.Clear();
            InstitutionsBuilder.Clear();
        }

        /// <inheritdoc/>
        public void Reload()
        {
            context.Database.CloseConnection();
            context.Database.OpenConnection();
            InitializeBuilders();
        }

        private void InitializeBuilders()
        {
            GroupsBuilder = new GroupsBuilder(context);
            TeachersBuilder = new TeachersBuilder(context);
            InstitutionsBuilder = new InstitutionsBuilder(context);
            SpecialitiesBuilder = new SpecialitiesBuilder(context);
        }
    }
}
