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
                case "textarea":
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
                case "submit":
                {
                    return new SeleniumSubmitElement(webdriver, by, seleniumBrowser);
                }
                case "textarea":
                case "text":
                {
                    return new SeleniumTextElement(webdriver, by, seleniumBrowser);
                }
                //html 5 elements...
                case "color":
                {
                    return new SeleniumColorElement(webdriver, by, seleniumBrowser);
                }
                case "date":
                {
                    return new SeleniumDateElement(webdriver, by, seleniumBrowser);
                }
                case "datetime-local":
                {
                    return new SeleniumDateTimeLocalElement(webdriver, by, seleniumBrowser);
                }
                case "email":
                {
                    return new SeleniumEmailElement(webdriver, by, seleniumBrowser);
                }
                case "month":
                {
                    return new SeleniumMonthElement(webdriver, by, seleniumBrowser);
                }
                case "number":
                {
                    return new SeleniumNumberElement(webdriver, by, seleniumBrowser);
                }
                case "range":
                {
                    return new SeleniumRangeElement(webdriver, by, seleniumBrowser);
                }
                case "reset":
                {
                    return new SeleniumResetElement(webdriver, by, seleniumBrowser);
                }
                case "search":
                {
                    return new SeleniumSearchElement(webdriver, by, seleniumBrowser);
                }
                case "tel":
                {
                    return new SeleniumTelElement(webdriver, by, seleniumBrowser);
                }
                case "time":
                {
                    return new SeleniumTimeElement(webdriver, by, seleniumBrowser);
                }
                case "url":
                {
                    return new SeleniumUrlElement(webdriver, by, seleniumBrowser);
                }
                case "week":
                {
                    return new SeleniumWeekElement(webdriver, by, seleniumBrowser);
                }
                default:
                {
                    return new SeleniumGenericElement(webdriver, by, seleniumBrowser);
                }
            }
        }
    }
}
