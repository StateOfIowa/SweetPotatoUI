using System;
using System.Diagnostics;
using OpenQA.Selenium;

namespace SweetPotatoUI.DriverImplementation.Selenium
{
    internal class SeleniumButtonElement : SeleniumElement
    {
        internal SeleniumButtonElement(IWebDriver driver, By by, SeleniumBrowser seleniumBrowser)
            : base(driver, by, seleniumBrowser)
        {
        }

        public override string GetValue()
        {
            throw new InvalidOperationException(
                string.Format(
                    "Element with locator ['{0}'] is of type [<button>] and has no value attribute.", By));
        }

        public override string GetText()
        {
            var stopwatch = new Stopwatch();

            while (stopwatch.Elapsed < SeleniumBrowser.GetElementWaitTimeSpan())
            {
                var elementText = GetWebElement().Text;

                if (string.IsNullOrEmpty(elementText))
                {
                    continue;
                }

                return elementText;
            }
            return string.Empty;
        }

        public override void Fill(string fillValue)
        {
            throw new InvalidOperationException(string.Format("Element with locator ['{0}'] is of type [<button>] " +
                                                              "and cannot be Filled by the SweetPotatoUI Framework.", By));
        }

        public override void Clear()
        {
            throw new InvalidOperationException(
                string.Format(
                    "Element with locator ['{0}'] is of type [<button>] and cannot be " +
                    "Cleared by the SweetPotatoUI Framework.", By));
        }
    }
}