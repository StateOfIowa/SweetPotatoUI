using System;
using System.Diagnostics;
using OpenQA.Selenium;

namespace SweetPotatoUI.DriverImplementation.Selenium
{
    public class SeleniumSubmitElement : SeleniumElement
    {
        public SeleniumSubmitElement(IWebDriver webdriver, By by, SeleniumBrowser seleniumBrowser)
            : base(webdriver, by, seleniumBrowser)
        {
        }

        public override string GetValue()
        {
            return GetWebElement().GetAttribute("value");
        }

        public override string GetText()
        {
            throw new InvalidOperationException(
                string.Format(
                    "Element with locator ['{0}'] is of type [<submit>] and has no text content. " +
                    "Try checking for Value instead.", By));
        }

        public override void Fill(string searchCriteria)
        {
            throw new InvalidOperationException("An Element with tag type of [<submit>] cannot be filled by " +
                                                "the SweetPotatoUI Framework.");
        }

        public override void Clear()
        {
            throw new InvalidOperationException(
                string.Format(
                    "Element with locator ['{0}'] is of type [<submit>]. The SweetPotatoUI Framework does not " +
                    "support the Clearing of this type of element.", By));
        }
    }
}