using OpenQA.Selenium;

namespace SweetPotatoUI.DriverImplementation.Selenium
{
    public class SeleniumSearchElement : SeleniumElement
    {
        public SeleniumSearchElement(IWebDriver webdriver, By by, SeleniumBrowser seleniumBrowser)
            : base(webdriver, by, seleniumBrowser)
        {
        }

        public override string GetValue()
        {
            return GetWebElement().GetAttribute("value");
        }

        public override string GetText()
        {
            return GetWebElement().Text;
        }

        public override void Fill(string searchCriteria)
        {
            GetWebElement().SendKeys(searchCriteria);
        }

        public override void Clear()
        {
            GetWebElement().Clear();
        }
    }
}