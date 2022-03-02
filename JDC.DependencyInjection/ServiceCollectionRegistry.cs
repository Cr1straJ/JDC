using JDC.BusinessLogic.Interfaces;
using JDC.BusinessLogic.Models;
using JDC.BusinessLogic.Services;
using JDC.BusinessLogic.Utilities.AzureStorage;
using JDC.BusinessLogic.Utilities.EmailSender;
using JDC.BusinessLogic.Utilities.PasswordGenerator;
using JDC.Common.Entities;
using JDC.DataAccess.Data;
using JDC.DataAccess.Interfaces;
using JDC.DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JDC.DependencyInjection
{
    /// <summary>
    /// Service collection registry.
    /// </summary>
    public static class ServiceCollectionRegistry
    {
        /// <summary>
        /// Adds configs to service collection.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <param name="configuration">Configuration.</param>
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

            services.AddSingleton(new AzureStorageConfig()
            {
                AccountName = configuration["AzureStorageConfig:AccountName"],
                AccountKey = configuration["AzureStorageConfig:AccountKey"],
                ImageContainer = configuration["AzureStorageConfig:ImageContainer"],
                SasToken = configuration["AzureStorageConfig:SasToken"],
            });
        }

        /// <summary>
        /// Adds services and repositories to service collection.
        /// </summary>
        /// <param name="services">Service collection.</param>
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<IGroupRepository, GroupRepository>();

            services.AddTransient<IGradeService, GradeService>();
            services.AddTransient<IGradeRepository, GradeRepository>();

            services.AddTransient<ILessonService, LessonService>();
            services.AddTransient<ILessonRepository, LessonRepository>();

            services.AddTransient<ITeacherService, TeacherService>();
            services.AddTransient<ITeacherRepository, TeacherRepository>();

            services.AddTransient<ISpecialityService, SpecialityService>();
            services.AddTransient<ISpecialityRepository, SpecialityRepository>();

            services.AddTransient<IRegistrationRequestService, RegistrationRequestService>();
            services.AddTransient<IRegistrationRequestRepository, RegistrationRequestRepository>();

            services.AddTransient<IChatService, ChatService>();
            services.AddTransient<IChatRepository, ChatRepository>();

            services.AddTransient<IInstitutionService, InstitutionService>();
            services.AddTransient<IInstitutionRepository, InstitutionRepository>();

            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IMessageRepository, MessageRepository>();

            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IStudentRepository, StudentRepository>();

            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IAzureStorage, AzureStorage>();
            services.AddTransient<IPasswordGenerator, PasswordGenerator>();
        }

        /// <summary>
        /// Adds identity to service collection.
        /// </summary>
        /// <param name="services">Service collection.</param>
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
                .AddRoles<Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
        }

        /// <summary>
        /// Adds database context to service collection.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <param name="connectionString">Connection string.</param>
        public static void AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();
        }
    }
}
