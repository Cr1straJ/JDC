using JDC.Common.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JDC.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<RegistrationRequest> RegistrationRequests { get; set; }

        public DbSet<ChatGroup> ChatGroups { get; set; }
        
        public DbSet<Grade> Grades { get; set; }
        
        public DbSet<StudentGroup> StudentGroups { get; set; }
        
        public DbSet<EInstitution> EInstitutions { get; set; }
        
        public DbSet<Message> Messages { get; set; }
        
        public DbSet<Student> Students { get; set; }
        
        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
