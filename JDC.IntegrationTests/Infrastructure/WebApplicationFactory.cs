using System.Linq;
using JDC.DataAccess.Data;
using JDC.IntegrationTests.Helpers;
using JDC.IntegrationTests.Infrastructure.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JDC.IntegrationTests.Infrastructure
{
    /// <inheritdoc/>
    public class WebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContext = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                if (dbContext is not null)
                {
                    services.Remove(dbContext);
                }

                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryJournalTest");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                services.AddAntiforgery(t =>
                {
                    t.Cookie.Name = AntiForgeryTokenExtractor.AntiForgeryCookieName;
                    t.FormFieldName = AntiForgeryTokenExtractor.AntiForgeryFieldName;
                });

                var provider = services.BuildServiceProvider();

                using var scope = provider.CreateScope();
                using var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                Utilities.InitializeDbForTests(appContext);
            });
        }
    }
}
