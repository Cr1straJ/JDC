using JDC.AutomatedUITests.Infrastructure;
using Xunit;

namespace JDC.AutomatedUITests.ViewsTests
{
    public class HomeTests : AutomatedUITestBase
    {
        [Fact]
        public void Index_WhenExecuted_ReturnsIndexView()
        {
            Driver.Navigate()
                .GoToUrl("https://localhost:44336/Home/Index");

            Assert.Equal("Главная - JDC", Driver.Title);
            Assert.Contains("Зарегестрировать", Driver.PageSource);
        }

        [Fact]
        public void Documentation_WhenExecuted_ReturnsDocumentationView()
        {
            Driver.Navigate()
                .GoToUrl($"https://localhost:44336/Home/Documentation");

            Assert.Equal("Документация - JDC", Driver.Title);
            Assert.Contains(@"<h1 class=""mb-1"">Введение</h1>", Driver.PageSource);
        }
    }
}
