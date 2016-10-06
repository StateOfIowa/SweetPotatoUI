using OpenQA.Selenium;

namespace SweetPotatoUI.DriverImplementation.Selenium
{
    internal class SeleniumUrlElement : SeleniumElement
    {
        internal SeleniumUrlElement(IWebDriver webdriver, By by, SeleniumBrowser seleniumBrowser) : 
            base(webdriver, by, seleniumBrowser)
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

        public override void Fill(string inputValue)
        {
            GetWebElement().SendKeys(inputValue);
        }

        public override void Clear()
        {
            GetWebElement().Clear();
        }
    }
}