using System.Collections.Generic;
using System.Linq;
using JDC.Common.Entities;
using JDC.IntegrationTests.Infrastructure.Database;
using JDC.IntegrationTests.Infrastructure.DataBuilder.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JDC.IntegrationTests.Infrastructure.DataBuilder.Builders
{
    public class InstitutionsBuilder : BuilderBase, IInstitutionsBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupsBuilder"/> class.
        /// </summary>
        public InstitutionsBuilder(IntegrationTestsDbContext context)
            : base(context)
        {
        }

        /// <inheritdoc/>
        public List<Institution> Institutions { get; private set; }

        /// <inheritdoc/>
        public IInstitutionsBuilder AddDefaultInstitution()
        {
            Context.Institutions.Add(new Institution());

            return this;
        }

        /// <inheritdoc/>
        public IInstitutionsBuilder AddInstitution(Institution institution)
        {
            Context.Institutions.Add(institution);

            return this;
        }


        /// <inheritdoc/>
        public override void Clear()
        {
            Context.Database.ExecuteSqlRaw(
                $"DELETE FROM [{nameof(Institution)}s]");
        }

        /// <inheritdoc/>
        protected override void InitializeCollections()
        {
            Institutions = Context.Institutions.AsEnumerable().ToList();
        }
    }
}
