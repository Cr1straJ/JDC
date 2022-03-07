using System.Collections.Generic;
using System.Linq;
using JDC.Common.Entities;
using JDC.IntegrationTests.Infrastructure.Database;
using JDC.IntegrationTests.Infrastructure.DataBuilder.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JDC.IntegrationTests.Infrastructure.DataBuilder.Builders
{
    internal class SpecialitiesBuilder : BuilderBase, ISpecialitiesBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpecialitiesBuilder"/> class.
        /// </summary>
        public SpecialitiesBuilder(IntegrationTestsDbContext context)
            : base(context)
        {
        }

        public List<Speciality> Specialities { get; private set; }

        public ISpecialitiesBuilder AddDefaultSpeciality()
        {
            Context.Specialities.Add(new Speciality());

            return this;
        }

        public ISpecialitiesBuilder AddSpeciality(Speciality speciality)
        {
            return this;
        }

        public override void Clear()
        {
            Context.Database.ExecuteSqlRaw(
               $"DELETE FROM [Specialities]");
        }

        protected override void InitializeCollections()
        {
            Specialities = Context.Specialities.AsEnumerable().ToList();
        }
    }
}
