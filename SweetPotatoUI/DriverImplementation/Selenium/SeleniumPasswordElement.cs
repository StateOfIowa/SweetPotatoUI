using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SweetPotatoUI.DriverImplementation.Selenium
{
    internal class SeleniumPasswordElement : SeleniumElement
    {
        internal SeleniumPasswordElement(IWebDriver webdriver, By by, SeleniumBrowser seleniumBrowser)
            : base(webdriver, by, seleniumBrowser)
        {
        }

        public override string GetValue()
        {
            throw new InvalidOperationException(string.Format(
                "The element with locator: [{0}] is of type [<password>]. You are not able to retrieve this value.", By));
        }

        public override string GetText()
        {
            throw new InvalidOperationException(string.Format(
                "The element with locator: [{0}] is of type [<password>]. You are not able to retrieve this text.", By));
        }

        public override void Clear()
        {
            GetWebElement().Clear();
        }

        public override void Fill(string inputValue)
        {
            try
            {
                Wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By));
            }
            catch (TimeoutException)
            {
                throw new InvalidOperationException(string.Format(
                    "The element with locator: [{0}] was never visible", By));
            }

            Clear();
            GetWebElement().SendKeys(inputValue);
        }
    }
}