using System;
using OpenQA.Selenium;

namespace SweetPotatoUI.DriverImplementation.Selenium
{
    public class SeleniumElementFactory : ISeleniumElementFactory
    {
        public SeleniumElement Create(IWebDriver webdriver, By by, SeleniumBrowser seleniumBrowser)
        {
            var tagName = SeleniumHelper.FindWebElement(webdriver, by, seleniumBrowser.GetElementWaitTimeSpan()).TagName;

            switch (tagName)
            {
                case "button":
                {
                    return new SeleniumButtonElement(webdriver, by, seleniumBrowser);
                }
                case "input":
                {
                    return CreateInputElement(webdriver, by, seleniumBrowser);
                }
                case "select":
                {
                    return new SeleniumSelectElement(webdriver, by, seleniumBrowser);
                }
                default:
                {
                    return new SeleniumGenericElement(webdriver, by, seleniumBrowser);
                }
            }
        }

        private static SeleniumElement CreateInputElement(IWebDriver webdriver, By by, SeleniumBrowser seleniumBrowser)
        {
            var elementType =
                SeleniumHelper.FindWebElement(webdriver, by, seleniumBrowser.GetElementWaitTimeSpan())
                    .GetAttribute("type");
            switch (elementType)
            {
                case "button":
                {
                    return new SeleniumButtonElement(webdriver, by, seleniumBrowser);
                }
                case "checkbox":
                {
                    return new SeleniumCheckboxElement(webdriver, by, seleniumBrowser);
                }
                case "password":
                {
                    return new SeleniumPasswordElement(webdriver, by, seleniumBrowser);
                }
                case "radio":
                {
                    return new SeleniumRadioElement(webdriver, by, seleniumBrowser);
                }
                case "text":
                {
                    return new SeleniumTextElement(webdriver, by, seleniumBrowser);
                }
                case "submit":
                {
                    return new SeleniumSubmitElement(webdriver, by, seleniumBrowser);
                }
                default:
                {
                    return new SeleniumGenericElement(webdriver, by, seleniumBrowser);
                }
            }
        }
    }
}