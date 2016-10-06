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
            return GetWebElement().GetAttribute("value");
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
            var typeAttribute = GetWebElement().GetAttribute("type");

            throw new InvalidOperationException(
                string.Format(
                    "Element with locator ['{0}'] is of type [{1}] and has a type attribute of [{2}]. " +
                    "The SweetPotatoUI framework does not currently support filling this type of " +
                    "element. ", By, tagName, typeAttribute));
        }

        public override void Clear()
        {
            var tagName = GetTagName;
            var typeAttribute = GetWebElement().GetAttribute("type");

            throw new InvalidOperationException(
                string.Format(
                    "Element with locator ['{0}'] is of type [{1}] and has a type attribute of [{2}]. " +
                    "The SweetPotatoUI framework does not currently support the Clearing of this type of " +
                    "element.", By, tagName, typeAttribute));
        }
    }
}