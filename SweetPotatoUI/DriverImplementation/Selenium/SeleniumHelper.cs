using System;
using System.Diagnostics;
using OpenQA.Selenium;

namespace SweetPotatoUI.DriverImplementation.Selenium
{
    public static class SeleniumHelper
    {
        public static IWebElement FindWebElement(IWebDriver webDriver, By by, TimeSpan waitTimeSpan)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            while (stopwatch.Elapsed < waitTimeSpan)
            {
                try
                {
                    return webDriver.FindElement(by);
                }
                catch (NoSuchElementException)
                {
                }
            }
            throw new NoSuchElementException(string.Format("Element [{0}] was not found with given locator.", by));
        }
    }
}