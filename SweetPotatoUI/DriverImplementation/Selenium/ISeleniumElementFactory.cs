using OpenQA.Selenium;

namespace SweetPotatoUI.DriverImplementation.Selenium
{
    public interface ISeleniumElementFactory
    {
        SeleniumElement Create(IWebDriver webdriver, By by, SeleniumBrowser seleniumBrowser);
    }
}