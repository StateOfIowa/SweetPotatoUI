using System;
using System.Diagnostics;
using OpenQA.Selenium;

namespace SweetPotatoUI.DriverImplementation.Selenium
{
    internal class SeleniumRadioElement : SeleniumElement
    {
        internal SeleniumRadioElement(IWebDriver webdriver, By by, SeleniumBrowser seleniumBrowser)
            : base(webdriver, by, seleniumBrowser)
        {
        }

        public override string GetValue()
        {
            var stopwatch = new Stopwatch();

            while (stopwatch.Elapsed < SeleniumBrowser.GetElementWaitTimeSpan())
            {
                var elementValue = GetWebElement().GetAttribute("value");

                if (string.IsNullOrEmpty(elementValue))
                {
                    continue;
                }
                return elementValue;
            }
            return string.Empty;
        }

        public override string GetText()
        {
            throw new InvalidOperationException(
                string.Format("The element with locator: [{0}] is of type [<checkbox>] " +
                              "and has no associated text.", By));
        }

        public override void Fill(string searchCriteria)
        {
            GetWebElement().Click();
        }

        public override void Clear()
        {
            throw new InvalidOperationException(
                string.Format(
                    "Element with locator ['{0}'] is of type [<radio>]. The SweetPotatoUI Framework does not " +
                    "support the Clearing of this type of element.", By));
        }
    }
}