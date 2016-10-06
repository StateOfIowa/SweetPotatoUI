using System;
using OpenQA.Selenium;

namespace SweetPotatoUI.DriverImplementation.Selenium
{
    internal class SeleniumCheckboxElement : SeleniumElement
    {
        internal SeleniumCheckboxElement(IWebDriver driver, By by, SeleniumBrowser seleniumBrowser) :
            base(driver, by, seleniumBrowser)
        {
        }

        public override string GetValue()
        {
            throw new InvalidOperationException(
                string.Format("The element with locator: [{0}] is of type [<checkbox>] " +
                              "and does not have a value attribute.", By));
        }

        public override string GetText()
        {
            throw new InvalidOperationException(
                string.Format("The element with locator: [{0}] is of type [<checkbox>] " +
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
                                          "Framework.", By);
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
                                          "Framework.", By);
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
                            , By, inputValue));
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
                                  "Framework.", By);
            }
        }
    }
}