using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;
using SweetPotatoUI.Enums;

namespace SweetPotatoUI.DriverImplementation.Selenium
{
    internal class SeleniumDriverFactory : ISeleniumDriverFactory
    {
        private static IWebDriver _webDriver;

        public IWebDriver Create(BrowserType browserType, string driverPath)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    _webDriver = new ChromeDriver(driverPath);
                    break;
                case BrowserType.InternetExplorer:
                    _webDriver = new InternetExplorerDriver(driverPath);
                    break;
                case BrowserType.PhantomJs:
                    _webDriver = new PhantomJSDriver(driverPath);
                    break;
                default:
                    _webDriver = new FirefoxDriver();
                    break;
            }
            return _webDriver;
        }
    }
}