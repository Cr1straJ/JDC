using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace JDC.AutomatedUITests.Infrastructure
{
    public class AutomatedUITestBase : IDisposable
    {
        protected readonly IWebDriver Driver;

        public AutomatedUITestBase()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.AddArgument("--ignore-certificate-errors");
            options.AcceptInsecureCertificates = true;

            Driver = new FirefoxDriver(options);
        }

        public void Dispose()
        {
            Driver.Quit();
            Driver.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
