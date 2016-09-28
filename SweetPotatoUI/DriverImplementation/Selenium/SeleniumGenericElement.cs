using System;
using System.Diagnostics;
using OpenQA.Selenium;

namespace SweetPotatoUI.DriverImplementation.Selenium
{
    public class SeleniumGenericElement : SeleniumElement
    {
        public SeleniumGenericElement(IWebDriver webdriver, By by, SeleniumBrowser seleniumBrowser)
            : base(webdriver, by, seleniumBrowser)
        {
        }

        public override string GetValue()
        {
            var tagName = GetTagName;

            throw new InvalidOperationException(
                string.Format(
                    "Element with locator ['{0}'] is of type [{1}] and has no value attribute. " +
                    "Try checking for Text instead.",
                    By, tagName));
        }

        public override string GetText()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            while (stopwatch.Elapsed < SeleniumBrowser.GetElementWaitTimeSpan())
            {
                var elementText = GetWebElement().Text;
                if (!string.IsNullOrEmpty(elementText))
                {
                    return elementText;
                }
            }

            return string.Empty;
        }

        public override void Fill(string searchCriteria)
        {
            var tagName = GetTagName;
            throw new InvalidOperationException(
                string.Format(
                    "Element with locator ['{0}'] is of type [{1}]. The SweetPotatoUI framwork does not currently " +
                    "support filling this type of element. The currently supported interations with this " +
                    "element are: ['Click','GetText','TabAway']", By, tagName));
        }

        public override void Clear()
        {
            var tagName = GetTagName;
            throw new InvalidOperationException(
                string.Format(
                    "Element with locator ['{0}'] is of type [{1}]. The SweetPotatoUI framwork does not currently " +
                    "support the Clearing of this type of element. The currently supported interations with this " +
                    "element are: ['Click','GetText','TabAway']", By, tagName));
        }
    }
}