using OpenQA.Selenium;
using SweetPotatoUI.Enums;

namespace SweetPotatoUI.DriverImplementation.Selenium
{
    public interface ISeleniumDriverFactory
    {
        IWebDriver Create(BrowserType driverType, string driverPath);
    }
}