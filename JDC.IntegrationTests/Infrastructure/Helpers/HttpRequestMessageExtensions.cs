using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace JDC.IntegrationTests.Infrastructure.Helpers
{
    /// <summary>
    /// Http request message extension methods.
    /// </summary>
    internal static class HttpRequestMessageExtensions
    {
        private const string JsonContentType = "application/json";
        private static readonly JsonSerializerSettings JsontSettings = new ()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        };

        /// <summary>
        /// Adds content to Http Request Message.
        /// </summary>
        /// <typeparam name="T">Content model type.</typeparam>
        /// <param name="requestMessage">Http Request Message.</param>
        /// <param name="content">Content model.</param>
        internal static void AddContent<T>(this HttpRequestMessage requestMessage, T content)
        {
            if (content is not null)
            {
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(content, JsontSettings), Encoding.UTF8, JsonContentType);
            }
        }
    }
}
