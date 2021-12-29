using JDC.BusinessLogic.Interfaces;
using JDC.BusinessLogic.Services;
using JDC.Common.Entities;
using JDC.Common.Interfaces;
using JDC.DataAccess.Data;
using JDC.DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JDC.DependencyInjection
{
    public static class ServiceDependency
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddTransient<IRegistrationRequestRepository, RegistrationRequestRepository>();
            services.AddTransient<IChatRepository, ChatRepository>();
            services.AddTransient<IGradesRepository, GradesRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddTransient<IInstitutionRepository, InstitutionRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<ITeacherRepository, TeacherRepository>();

            services.AddTransient<IRegistrationRequestService, RegistrationRequestService>();
            services.AddTransient<IChatService, ChatService>();
            services.AddTransient<IGradesService, GradesService>();
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<IInstitutionService, InstitutionService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<ITeacherService, TeacherService>();
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
