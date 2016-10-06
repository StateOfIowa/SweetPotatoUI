using System;
using OpenQA.Selenium;

namespace SweetPotatoUI.DriverImplementation.Selenium
{
    internal class SeleniumResetElement : SeleniumElement
    {
        internal SeleniumResetElement(IWebDriver webdriver, By by, SeleniumBrowser seleniumBrowser) : 
            base(webdriver, by, seleniumBrowser)
        {
        }

        public override string GetValue()
        {
            return GetWebElement().GetAttribute("value");
        }

        public override string GetText()
        {
            throw new InvalidOperationException("An Element with tag type of [<reset>] cannot be filled by " +
                                                "the SweetPotatoUI framework.");
        }

        public override void Fill(string inputValue)
        {
            GetWebElement().SendKeys(inputValue);
        }

        public override void Clear()
        {
            throw new InvalidOperationException("An Element with tag type of [<reset>] cannot be Cleared by " +
                                                "the SweetPotatoUI framework.");
        }
    }
}