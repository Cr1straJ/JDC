using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JDC.Common.Entities;

namespace JDC.IntegrationTests.Infrastructure.DataBuilder.Interfaces
{
    /// <summary>
    /// Institutions builder.
    /// </summary>
    public interface IInstitutionsBuilder : IBuilder
    {
        /// <summary>
        /// Institution collection.
        /// </summary>
        List<Institution> Institutions { get; }

        /// <summary>
        /// Adds institution.
        /// </summary>
        /// <param name="institution">Institution.</param>
        IInstitutionsBuilder AddInstitution(Institution institution);

        /// <summary>
        /// Adds default institution.
        /// </summary>
        IInstitutionsBuilder AddDefaultInstitution();
    }
}
