using System;
using System.Net.Http;
using JDC.IntegrationTests.Infrastructure.DataBuilder.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace JDC.IntegrationTests.Infrastructure
{
    /// <summary>
    /// Integration tests base.
    /// </summary>
    internal abstract class IntegrationTestsBase : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntegrationTestsBase"/> class.
        /// </summary>
        public IntegrationTestsBase()
        {
            /*IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Testing.json")
                .AddEnvironmentVariables()
                .Build();

            var server = new TestServer(new WebHostBuilder()
                .UseConfiguration(config)
                .UseStartup<IntegrationTestsStartup>());*/
            var factory = new WebApplicationFactory();
            Server = factory.Server;
            Client = Server.CreateClient();
            Client.BaseAddress = new Uri("http://localhost");

            Builder = Server.Services.GetService(typeof(IDataBuilder)) as IDataBuilder;
        }

        /// <summary>
        /// Test server.
        /// </summary>
        protected TestServer Server { get; }

        /// <summary>
        /// Client of test server.
        /// </summary>
        protected HttpClient Client { get; }

        /// <summary>
        /// Data builder.
        /// </summary>
        protected IDataBuilder Builder { get; }

        /// <summary>
        /// Disposes integration tests module.
        /// </summary>
        public void Dispose()
        {
            Builder.Clear();
            Client?.Dispose();
            Server?.Dispose();
        }
    }
}
