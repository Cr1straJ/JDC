namespace JDC.IntegrationTests.Infrastructure.DataBuilder.Interfaces
{
    /// <summary>
    /// Contains common builder functionality.
    /// </summary>
    public interface IBuilder
    {
        /// <summary>
        /// Builds context.
        /// </summary>
        void Build();

        /// <summary>
        /// Clears context.
        /// </summary>
        void Clear();

        /// <summary>
        /// Reloads entity.
        /// </summary>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <param name="entity">Entity.</param>
        void Reload<T>(T entity)
            where T : class;
    }
}
