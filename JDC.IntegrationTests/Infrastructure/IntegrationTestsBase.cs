using System.Net.Http;
using System.Threading.Tasks;
using JDC.IntegrationTests.Infrastructure.Helpers;
using JDC.IntegrationTests.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Net.Http.Headers;
using Xunit;

namespace JDC.IntegrationTests.Infrastructure
{
    public class IntegrationTestsBase : IClassFixture<WebApplicationFactory>
    {
        protected readonly HttpClient Client;

        public IntegrationTestsBase(WebApplicationFactory factory)
        {
            Client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        protected async Task<(string, HttpRequestMessage)> GetRequestWithAntiForgeryToken(HttpMethod method, string responseUri, string requestUri)
        {
            var initResponse = await Client.GetAsync(responseUri);
            var (fieldValue, cookieValue) = await AntiForgeryHelper.ExtractAntiForgeryValues(initResponse);
            var request = new HttpRequestMessage(method, requestUri);

            request.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryTokenExtractor.AntiForgeryCookieName, cookieValue).ToString());

            return (fieldValue, request);
        }
    }
}
