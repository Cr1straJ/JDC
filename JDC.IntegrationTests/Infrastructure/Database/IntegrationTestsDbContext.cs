using JDC.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace JDC.IntegrationTests.Infrastructure.Database
{
    /// <summary>
    /// Integration tests database context.
    /// </summary>
    public class IntegrationTestsDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntegrationTestsDbContext"/> class.
        /// </summary>
        public IntegrationTestsDbContext(DbContextOptions<IntegrationTestsDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Group database set.
        /// </summary>
        public DbSet<Group> Groups { get; set; }

        /// <summary>
        /// Teacher database set.
        /// </summary>
        public DbSet<Teacher> Teachers { get; set; }

        /// <summary>
        /// Institution database set.
        /// </summary>
        public DbSet<Institution> Institutions { get; set; }

        /// <summary>
        /// Speciality database set.
        /// </summary>
        public DbSet<Speciality> Specialities { get; set; }

        /// <summary>
        /// On model creating instructions.
        /// </summary>
        /// <param name="modelBuilder">Model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Institution>()
                .HasOne(a => a.Director)
                .WithOne(a => a.Institution)
                .HasForeignKey<User>(c => c.InstitutionId);

            modelBuilder.Entity<Group>().ToTable($"{nameof(Group)}s").HasKey(x => x.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
