using JDC.BusinessLogic.Interfaces;
using JDC.BusinessLogic.Models;
using JDC.BusinessLogic.Services;
using JDC.Common.Entities;
using JDC.Common.Interfaces;
using JDC.DataAccess.Data;
using JDC.DataAccess.Interfaces;
using JDC.DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JDC.DependencyInjection
{
    public static class ServiceDependency
    {
        public static void AddConfigurationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(new SmtpClientSettings()
            {
                Email = configuration["SmtpClient:Email"],
                Password = configuration["SmtpClient:Password"],
                Name = configuration["SmtpClient:Name"],
                Host = configuration["SmtpClient:Host"],
                Port = int.Parse(configuration["SmtpClient:Port"]),
            });
        }

        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<IGroupRepository, GroupRepository>();

            services.AddTransient<IGradeService, GradeService>();
            services.AddTransient<IGradeRepository, GradeRepository>();

            services.AddTransient<ITeacherService, TeacherService>();
            services.AddTransient<ITeacherRepository, TeacherRepository>();

            services.AddTransient<ISpecialityService, SpecialityService>();
            services.AddTransient<ISpecialityRepository, SpecialityRepository>();

            services.AddTransient<IRegistrationRequestService, RegistrationRequestService>();
            services.AddTransient<IRegistrationRequestRepository, RegistrationRequestRepository>();

            services.AddTransient<IEmailSender, EmailSender>();
        }

        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddDefaultIdentity<User>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 0;
                options.User.AllowedUserNameCharacters += "1234567890";
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
        }

        public static void AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();
        }
    }
}
