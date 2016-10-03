using System;
using System.Diagnostics;
using OpenQA.Selenium;

namespace SweetPotatoUI.DriverImplementation.Selenium
{
    public class SeleniumButtonElement : SeleniumElement
    {
        public SeleniumButtonElement(IWebDriver driver, By by, SeleniumBrowser seleniumBrowser)
            : base(driver, by, seleniumBrowser)
        {
        }

        public override string GetValue()
        {
            throw new InvalidOperationException(
                string.Format(
                    "Element with locator ['{0}'] is of type [<button>] and has no value attribute. " +
                    "Try checking for Text instead.",
                    By));
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
            throw new InvalidOperationException("An Element with tag type of [<button>] cannot be filled by " +
                                                "the SweetPotatoUI framework. The following interactions are available: " +
                                                "['Click','GetText','TabAway']");
        }

        public override void Clear()
        {
            throw new InvalidOperationException(
                string.Format(
                    "Element with locator ['{0}'] is of type [<button>]. The SweetPotatoUI framwork does not " +
                    "support the Clearing of this type of element. The following interactions are available: " +
                    "['Click','GetText','TabAway']", By));
        }
    }
}