using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

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
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates host builder.
        /// </summary>
        /// <returns>Instance of the <see cref="HostBuilder"/> with defaults for hosting a web app.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
