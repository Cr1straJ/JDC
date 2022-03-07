using System.Net.Http;
using Newtonsoft.Json;

namespace JDC.IntegrationTests.Infrastructure.Helpers
{
    /// <summary>
    /// Http response message extension methods.
    /// </summary>
    internal static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Gets response result from content.
        /// </summary>
        /// <typeparam name="T">Response result type.</typeparam>
        /// <param name="responseMessage">Http response message.</param>
        /// <returns>Response result.</returns>
        internal static T GetResponseResult<T>(this HttpResponseMessage responseMessage)
        {
            return JsonConvert.DeserializeObject<T>(responseMessage.Content.ReadAsStringAsync().Result);
        }
    }
}
