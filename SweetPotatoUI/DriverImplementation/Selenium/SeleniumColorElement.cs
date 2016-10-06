using System;
using OpenQA.Selenium;

namespace SweetPotatoUI.DriverImplementation.Selenium
{
    internal class SeleniumColorElement : SeleniumElement
    {
        internal SeleniumColorElement(IWebDriver webdriver, By by, SeleniumBrowser seleniumBrowser) : 
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

        public override void Fill(string searchCriteria)
        {
            throw new InvalidOperationException(
                string.Format("The element with locator [{0}] is an input of type [<color>]. " +
                              "Currently the SweetPotatoUI Framework does not support the Filling " +
                              "of this type of input. ", By));
        }

        public override void Clear()
        {
            throw new InvalidOperationException(
                string.Format("The element with locator [{0}] is an input of type [<color>]. " +
                              "Currently the SweetPotatoUI Framework does not support the Clearing " +
                              "of this type of input.", By));
        }
    }
}