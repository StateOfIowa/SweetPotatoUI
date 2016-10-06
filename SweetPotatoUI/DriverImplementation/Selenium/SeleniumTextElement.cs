using System;
using OpenQA.Selenium;

namespace SweetPotatoUI.DriverImplementation.Selenium
{
    public class SeleniumTextElement : SeleniumElement
    {
        public SeleniumTextElement(IWebDriver driver, By by, SeleniumBrowser seleniumBrowser)
            : base(driver, by, seleniumBrowser)
        {
        }

        public override string GetValue()
        {
            return GetWebElement().GetAttribute("value");
        }

        public override string GetText()
        {
            throw new InvalidOperationException(
                string.Format("The element with locator [{0}] is an input of type [text] and" +
                              "does not have text that can be retrieved. Try checking the value attribute instead.", By));
        }

        public override void Fill(string inputValue)
        {
            var attemptCount = 0;
            while (attemptCount < 5)
            {
                Clear();
                GetWebElement().SendKeys(inputValue);
                if (GetValue() == inputValue)
                {
                    return;
                }
                attemptCount++;
            }

            throw new Exception(string.Format(
                "After attempting to fill The element with locator [{0}]  5 times, " +
                "the actual value: [{1}] returned by the element " +
                "did not match the expected fill value: [{2}]", By,
                GetValue(), inputValue));
        }

        public override void Clear()
        {
            GetWebElement().Clear();
        }
    }
}