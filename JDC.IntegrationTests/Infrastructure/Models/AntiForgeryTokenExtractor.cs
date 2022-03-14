namespace JDC.IntegrationTests.Infrastructure.Models
{
    public static class AntiForgeryTokenExtractor
    {
        public static string AntiForgeryFieldName { get; } = "AntiForgeryTokenField";

        public static string AntiForgeryCookieName { get; } = "AntiForgeryTokenCookie";
    }
}
