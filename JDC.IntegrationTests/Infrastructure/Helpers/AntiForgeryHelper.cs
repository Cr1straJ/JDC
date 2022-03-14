using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using JDC.IntegrationTests.Infrastructure.Models;
using Microsoft.Net.Http.Headers;

namespace JDC.IntegrationTests.Infrastructure.Helpers
{
    public static class AntiForgeryHelper
    {
        public static async Task<(string fieldValue, string cookieValue)> ExtractAntiForgeryValues(HttpResponseMessage response)
        {
            var cookie = ExtractAntiForgeryCookieValueFrom(response);
            var token = ExtractAntiForgeryToken(await response.Content.ReadAsStringAsync());

            return (token, cookie);
        }

        private static string ExtractAntiForgeryCookieValueFrom(HttpResponseMessage response)
        {
            string antiForgeryCookie = response.Headers.GetValues("Set-Cookie")
                .FirstOrDefault(x => x.Contains(AntiForgeryTokenExtractor.AntiForgeryCookieName));

            if (antiForgeryCookie is null)
            {
                throw new ArgumentException($"Cookie '{AntiForgeryTokenExtractor.AntiForgeryCookieName}' not found in HTTP response", nameof(response));
            }

            string antiForgeryCookieValue = SetCookieHeaderValue.Parse(antiForgeryCookie).Value.ToString();

            return antiForgeryCookieValue;
        }

        private static string ExtractAntiForgeryToken(string htmlBody)
        {
            var requestVerificationTokenMatch = Regex.Match(htmlBody, $@"\<input name=""{AntiForgeryTokenExtractor.AntiForgeryFieldName}"" type=""hidden"" value=""([^""]+)"" \/\>");
            
            if (requestVerificationTokenMatch.Success)
            {
                return requestVerificationTokenMatch.Groups[1].Captures[0].Value;
            }

            throw new ArgumentException($"Anti forgery token '{AntiForgeryTokenExtractor.AntiForgeryFieldName}' not found in HTML", nameof(htmlBody));
        }
    }
}
