using JDC.IntegrationTests.Infrastructure.Database;
using JDC.IntegrationTests.Infrastructure.DataBuilder.Builders;
using JDC.IntegrationTests.Infrastructure.DataBuilder.Interfaces;
using JDC;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JDC.IntegrationTests.Infrastructure
{
    /// <summary>
    /// Integration tests startup.
    /// </summary>
    public class IntegrationTestsStartup : Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntegrationTestsStartup"/> class.
        /// </summary>
        public IntegrationTestsStartup(IConfiguration configuration)
            : base(configuration)
        {
        }

        /// <inheritdoc/>
        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            services.AddDbContext<IntegrationTestsDbContext>(x =>
                x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            ConfigureDataBuilders(services);
        }

        private static void ConfigureDataBuilders(IServiceCollection services)
        {
            services.AddScoped<IDataBuilder, UnitOfWorkBuilder>();
        }
    }
}
