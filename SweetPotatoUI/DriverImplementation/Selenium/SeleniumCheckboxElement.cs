using System;
using OpenQA.Selenium;

namespace SweetPotatoUI.DriverImplementation.Selenium
{
    public class SeleniumCheckboxElement : SeleniumElement
    {
        private readonly By _by;

        public SeleniumCheckboxElement(IWebDriver driver, By by, SeleniumBrowser seleniumBrowser) :
            base(driver, by, seleniumBrowser)
        {
            _by = by;
        }

        public override string GetValue()
        {
            throw new InvalidOperationException(
                string.Format("The element with locator: [{0}] is of type [checkbox] " +
                              "and does not have a value attribute.", By));
        }

        public override string GetText()
        {
            throw new InvalidOperationException(
                string.Format("The element with locator: [{0}] is of type [checkbox] " +
                              "and has no associated text.", By));
        }

        public override void Fill(string inputValue)
        {
            switch (inputValue.ToLower())
            {
                case "true":
                    if (GetWebElement().Selected)
                    {
                        Console.WriteLine("Element with locator [{0}] is already selected, " +
                                          "no action was performed by the SweetPotatoUI " +
                                          "framework.", _by);
                    }
                    else
                    {
                        Click();
                    }
                    break;
                case "false":
                    if (!GetWebElement().Selected)
                    {
                        Console.WriteLine("Element with locator [{0}] is already not selected, " +
                                          "no action was performed by the SweetPotatoUI " +
                                          "framework.", _by);
                    }
                    else
                    {
                        Click();
                    }
                    break;
                default:
                    throw new Exception(
                        string.Format("Element with locator [{0}]  was attempted to be filled with input [{1}]." +
                                      "This input is invalid for checkbox elements. The valid inputs are [true] or [false]."
                            , _by, inputValue));
            }
        }

        public override void Clear()
        {
            var webElement = GetWebElement();

            if (webElement.Selected)
            {
                webElement.Click();
            }
            else
            {
                Console.WriteLine("Element with locator [{0}] is already not selected, " +
                                  "no action was performed by the SweetPotatoUI " +
                                  "framework.", _by);
            }
        }
    }
}