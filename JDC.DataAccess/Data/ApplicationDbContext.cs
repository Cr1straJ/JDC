using JDC.Common.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JDC.DataAccess.Data
{
    /// <summary>
    /// An <see cref="ApplicationDbContext"/> instance represents a session with the database and can be used to query and save
    /// instances of your entities. <see cref="DbContext"/> is a combination of the Unit Of Work and Repository patterns.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class using the specified options.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the institutions of the application.
        /// </summary>
        public DbSet<Institution> Institutions { get; set; }

        /// <summary>
        /// Gets or sets the specialities of the application.
        /// </summary>
        public DbSet<Speciality> Specialities { get; set; }

        /// <summary>
        /// Gets or sets the teachers of the application.
        /// </summary>
        public DbSet<Teacher> Teachers { get; set; }

        /// <summary>
        /// Gets or sets the lessons of the application.
        /// </summary>
        public DbSet<Lesson> Lessons { get; set; }

        /// <summary>
        /// Gets or sets the students' grades.
        /// </summary>
        public DbSet<Grade> Grades { get; set; }

        /// <summary>
        /// Gets or sets the registration requests of the application.
        /// </summary>
        public DbSet<RegistrationRequest> RegistrationRequests { get; set; }

        /// <summary>
        /// Gets or sets the chats of the application.
        /// </summary>
        public DbSet<Chat> Chats { get; set; }

        /// <summary>
        /// Gets or sets the student groups of the application.
        /// </summary>
        public DbSet<Group> Groups { get; set; }

        /// <summary>
        /// Gets or sets the messages of the application.
        /// </summary>
        public DbSet<Message> Messages { get; set; }

        /// <summary>
        /// Gets or sets the students of the application.
        /// </summary>
        public DbSet<Student> Students { get; set; }

        /// <summary>
        /// This method to configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="DbSet{TEntity}" /> properties on derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">
        /// The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.
        /// </param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
