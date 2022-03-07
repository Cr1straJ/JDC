using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace JDC.IntegrationTests.Infrastructure
{
    /// <inheritdoc/>
    public class WebApplicationFactory : WebApplicationFactory<IntegrationTestsStartup>
    {
        /// <inheritdoc/>
        protected override IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder().ConfigureWebHost((builder) =>
            {
                builder.UseStartup<IntegrationTestsStartup>();
                builder.UseEnvironment("Testing");
            });
        }
    }
}
