using System;
using System.Threading.Tasks;
using JDC.BusinessLogic.Utilities;
using JDC.Common.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace JDC
{
    /// <summary>
    /// Contains the entry point.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry point.
        /// </summary>
        /// <param name="args">Arguments.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public static async Task Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;

                try
                {
                    await DatabaseInitializer.Initialize(
                        services.GetRequiredService<UserManager<User>>(),
                        services.GetRequiredService<RoleManager<IdentityRole>>(),
                        services.GetRequiredService<IConfiguration>());
                }
                catch (InvalidOperationException exception)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(exception, "An error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        /// <summary>
        /// Creates host builder.
        /// </summary>
        /// <param name="args">Arguments.</param>
        /// <returns>Instance of the <see cref="HostBuilder"/> with defaults for hosting a web app.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
