using System.Collections.Generic;
using JDC.Common.Entities;

namespace JDC.IntegrationTests.Infrastructure.DataBuilder.Interfaces
{
    /// <summary>
    /// Specialities builder.
    /// </summary>
    public interface ISpecialitiesBuilder : IBuilder
    {
        /// <summary>
        /// Speciality collection.
        /// </summary>
        List<Speciality> Specialities { get; }

        /// <summary>
        /// Adds speciality.
        /// </summary>
        /// <param name="speciality">Speciality.</param>
        ISpecialitiesBuilder AddSpeciality(Speciality speciality);

        /// <summary>
        /// Adds default speciality.
        /// </summary>
        ISpecialitiesBuilder AddDefaultSpeciality();
    }
}
