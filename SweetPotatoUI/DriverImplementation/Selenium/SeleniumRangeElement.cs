using System;
using OpenQA.Selenium;

namespace SweetPotatoUI.DriverImplementation.Selenium
{
    internal class SeleniumRangeElement : SeleniumElement
    {
        public SeleniumRangeElement(IWebDriver webdriver, By by, SeleniumBrowser seleniumBrowser) : 
            base(webdriver, by, seleniumBrowser)
        {
        }

        public override string GetValue()
        {
            return GetWebElement().GetAttribute("value");
        }

        public override string GetText()
        {
            return GetWebElement().Text;
        }

        public override void Fill(string inputValue)
        {
            var inputValueInt = Convert.ToInt32(inputValue);
            var minValue = Convert.ToInt32(GetWebElement().GetAttribute("min"));
            var maxValue = Convert.ToInt32(GetWebElement().GetAttribute("max"));
           
            if (inputValueInt < minValue || inputValueInt > maxValue)
            {
                throw new InvalidOperationException(string.Format("Could not set [<input type='range'/>] to a value outside of it's range specified by " +
                                                    "the [min] and [max] attributes. The set value was: [{0}]. The min value was: [{1}] " +
                                                                  "The max value was [{2}]", inputValue, minValue, maxValue));
            }

            if (GetWebElement().GetAttribute("value")==null)
            {
                 throw new InvalidOperationException(string.Format("Cannot set range element value attribute as element with locator [{0}] " +
                                                     "does not appear to have a value atribute defined.",By ));
            }

            var javascriptExecutor = SeleniumBrowser as IJavaScriptExecutor;
            var script = string.Format("arguments[0].setAttribute('value', {0})", inputValue);
            javascriptExecutor.ExecuteScript(script, GetWebElement());
        }

        public override void Clear()
        {
            GetWebElement().Clear();
        }

    }
}