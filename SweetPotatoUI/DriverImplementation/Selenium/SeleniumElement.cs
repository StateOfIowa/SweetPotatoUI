using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SweetPotatoUI.DriverImplementation.Selenium
{
    public abstract class SeleniumElement : IAutomationElement
    {
        private readonly SeleniumElementHighlighter _seleniumElementHighlighter;
        private readonly SafeHandle _handle = new SafeFileHandle(IntPtr.Zero, true);
        private readonly SeleniumBrowser _seleniumBrowser;

        private bool _disposed;

        protected SeleniumElement(IWebDriver driver, By by, SeleniumBrowser seleniumBrowser)
        {
            Driver = driver;
            By = by;
            _seleniumBrowser = seleniumBrowser;

            Wait = new WebDriverWait(Driver, _seleniumBrowser.GetElementWaitTimeSpan());

            if (!_seleniumBrowser.IsElementHighlighterEnabled())
            {
                return;
            }

            _seleniumElementHighlighter = new SeleniumElementHighlighter(Driver, GetWebElement());
            _seleniumElementHighlighter.HighlightElementBackground();
        }

        protected IWebDriver Driver { get; set; }

        protected string GetTagName
        {
            get { return GetWebElement().TagName; }
        }

        protected By By { get; set; }

        protected WebDriverWait Wait { get; set; }

        protected SeleniumBrowser SeleniumBrowser
        {
            get { return _seleniumBrowser; }
        }

        public string GetAttribute(string attributeName)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            while (stopwatch.Elapsed < _seleniumBrowser.GetElementWaitTimeSpan())
            {
                var attribute = GetWebElement().GetAttribute(attributeName);

                if (!string.IsNullOrEmpty(attribute))
                {
                    return attribute;
                }

                if (attribute == null)
                {
                    continue;
                }

                return attribute;
            }

            throw new InvalidOperationException(
                string.Format("The attribute [{0}] does not exist on element with locator [{1}].",
                    attributeName, By));
        }

        public void Click()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            while (stopwatch.Elapsed < _seleniumBrowser.GetElementWaitTimeSpan())
            {
                try
                {
                    GetWebElement().Click();
                }
                catch (ElementNotVisibleException)
                {
                    continue;
                }
                catch (InvalidOperationException)
                {
                    continue;
                }
                catch (StaleElementReferenceException)
                {
                    continue;
                }
                return;
            }

            throw new InvalidOperationException(
                string.Format("The element with locator: <{0}> was not clickable at the time a click was requested.",
                    By));
        }

        public void ScrollIntoView()
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", GetWebElement());
        }

        public bool IsDisplayed()
        {
            var isDisplayed = GetWebElement().Displayed;
            return isDisplayed;
        }

        public bool IsEnabled()
        {
            var isEnabled = GetWebElement().Enabled;
            return isEnabled;
        }

        public abstract string GetValue();
        public abstract string GetText();
        public abstract void Fill(string searchCriteria);
        public abstract void Clear();

        public void MoveTo()
        {
            var actions = new Actions(Driver);
            var element = GetWebElement();
            actions.MoveToElement(element).Perform();
        }

        public void TabAway()
        {
            GetWebElement().SendKeys(Keys.Tab);
        }

        public void Dispose()
        {
            if (IsHighlightingEnabled())
            {
                _seleniumElementHighlighter.RestoreBackgroundColor();
            }
        }

        public bool IsSelected()
        {
            var isSelected = GetWebElement().Selected;
            return isSelected;
        }

        private bool IsHighlightingEnabled()
        {
            return _seleniumBrowser.IsElementHighlighterEnabled();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _handle.Dispose();
                _seleniumElementHighlighter.RestoreBackgroundColor();
            }

            _disposed = true;
        }

        protected IWebElement GetWebElement()
        {
            return SeleniumHelper.FindWebElement(Driver, By, _seleniumBrowser.GetElementWaitTimeSpan());
        }

        protected IWebElement IsElementDisplayedAndEnabled()
        {
            try
            {
                var element = GetWebElement();
                if (element.Displayed && element.Enabled)
                {
                    return element;
                }
                return null;
            }
            catch (TimeoutException)
            {
                throw new InvalidElementStateException(string.Format("The element with locator [{0}] did not become " +
                                                                     "displayed and enabled in the allotted time.", By));
            }
        }
    }
}