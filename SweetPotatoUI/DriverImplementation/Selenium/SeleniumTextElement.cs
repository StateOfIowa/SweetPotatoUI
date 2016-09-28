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
            var webElement = GetWebElement();
            var attribute = webElement.GetAttribute("value");
            return attribute;
        }

        public override string GetText()
        {
            throw new InvalidOperationException(
                string.Format("The element with locator [{0}] is an input of type [text] and" +
                              "does not have a text attribute. Try checking the value attribute instead.", By));
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
            var element = Wait.Until(
                d => IsElementDisplayedAndEnabled());

            element.Clear();
        }
    }
}