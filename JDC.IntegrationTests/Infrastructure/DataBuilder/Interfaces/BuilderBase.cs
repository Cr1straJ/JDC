using JDC.IntegrationTests.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace JDC.IntegrationTests.Infrastructure.DataBuilder.Interfaces
{
    /// <summary>
    /// Builder base functionality.
    /// </summary>
    public abstract class BuilderBase : IBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuilderBase"/> class.
        /// </summary>
        public BuilderBase(IntegrationTestsDbContext context)
        {
            Context = context;
            InitializeCollections();
        }

        /// <summary>
        /// Database context.
        /// </summary>
        protected IntegrationTestsDbContext Context { get; }

        /// <inheritdoc/>
        public void Build()
        {
            Context.SaveChanges();
            InitializeCollections();
        }

        /// <inheritdoc/>
        public void Reload<T>(T entity)
            where T : class
        {
            Context.Entry(entity).Reload();
        }

        /// <inheritdoc/>
        public abstract void Clear();

        /// <summary>
        /// Initializes collections.
        /// </summary>
        protected abstract void InitializeCollections();
    }
}
