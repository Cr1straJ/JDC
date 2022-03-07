namespace JDC.IntegrationTests.Infrastructure.DataBuilder.Interfaces
{
    /// <summary>
    /// Unit of Work for builders.
    /// </summary>
    public interface IDataBuilder
    {
        /// <summary>
        /// Grpoups builder.
        /// </summary>
        IGroupsBuilder GroupsBuilder { get; }

        /// <summary>
        /// Institutions builder.
        /// </summary>
        IInstitutionsBuilder InstitutionsBuilder { get; }

        /// <summary>
        /// Specialities builder.
        /// </summary>
        ISpecialitiesBuilder SpecialitiesBuilder { get; }

        /// <summary>
        /// Teachers builder.
        /// </summary>
        ITeachersBuilder TeachersBuilder { get; }

        /// <summary>
        /// Clears builders.
        /// </summary>
        void Clear();

        /// <summary>
        /// Reloads builders.
        /// </summary>
        void Reload();
    }
}
