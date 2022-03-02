using JDC.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JDC.DataAccess.Data
{
    public class Role : IdentityRole<int>
    {
        public Role() { }
        public Role(string name) { Name = name; }
    }

    public class UserStore : UserStore<User, Role, ApplicationDbContext, int>
    {
        public UserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    public class RoleStore : RoleStore<Role, ApplicationDbContext, int>
    {
        public RoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Institution> Institutions { get; set; }

        public DbSet<Speciality> Specialities { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<Grade> Grades { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<RegistrationRequest> RegistrationRequests { get; set; }

        public DbSet<ChatGroup> ChatGroups { get; set; }

        public DbSet<Group> StudentGroups { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
