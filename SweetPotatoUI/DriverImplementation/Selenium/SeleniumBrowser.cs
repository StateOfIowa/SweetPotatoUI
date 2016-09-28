using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace SweetPotatoUI.DriverImplementation.Selenium
{
    public class SeleniumBrowser : IAutomationBrowser
    {
        private static IWebDriver _webDriver;
        private readonly ISeleniumElementFactory _seleniumElementFactory;
        private readonly ISweetPotatoSettings _sweetPotatoSettings;

        public SeleniumBrowser(ISeleniumDriverFactory seleniumDriverFactory, ISweetPotatoSettings sweetPotatoSettings,
            ISeleniumElementFactory seleniumElementFactory)
        {
            _seleniumElementFactory = seleniumElementFactory;
            _sweetPotatoSettings = sweetPotatoSettings;
            var browserType = sweetPotatoSettings.GetBrowserType();
            _webDriver = seleniumDriverFactory.Create(browserType, sweetPotatoSettings.GetDriverPath());
            Elements = new List<ElementLookup>();
        }

        private ICollection<ElementLookup> Elements { get; set; }

        public IAutomationElement FindElementById(string id)
        {
            var by = By.Id(id);
            return FindElement(by);
        }

        public IAutomationElement FindElementByName(string name)
        {
            var by = By.Name(name);
            return FindElement(by);
        }

        public IAutomationElement FindElementByLinkText(string linkText)
        {
            var by = By.LinkText(linkText);

            return FindElement(by);
        }

        public IAutomationElement FindElementByTagName(string tagText)
        {
            var by = By.TagName(tagText);

            return FindElement(by);
        }

        public IAutomationElement FindElementByLinkCssSelector(string cssSelector)
        {
            var by = By.CssSelector(cssSelector);

            return FindElement(by);
        }

        public IAutomationElement FindElementByXPath(string xpath)
        {
            var by = By.XPath(xpath);

            return FindElement(by);
        }

        public string PageSource()
        {
            return _webDriver.PageSource;
        }

        public void Dispose()
        {
            _webDriver.Dispose();
        }

        public void NavigateTo(string url)
        {
            _webDriver.Url = url;
        }

        public string GetTitle()
        {
            return _webDriver.Title;
        }

        public string GetWindowHandle()
        {
            return _webDriver.CurrentWindowHandle;
        }

        public void FocusOnWindow(string windowHandle)
        {
            _webDriver.SwitchTo().Window(windowHandle);
        }

        public void AddElement(ElementLookup element)
        {
            Elements.Add(element);
        }

        public ICollection<ElementLookup> GetElements
        {
            get { return Elements; }
        }

        public void MoveToTopOfCurrentPage()
        {
            ((IJavaScriptExecutor) _webDriver).ExecuteScript("window.scrollTo(0, -250);");
        }

        public void MoveToBottomOfCurrentPage()
        {
            ((IJavaScriptExecutor) _webDriver).ExecuteScript("window.scrollTo(0, 250);");
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return _webDriver.FindElements(by);
        }

        private IAutomationElement FindElement(By by)
        {
            return _seleniumElementFactory.Create(_webDriver, by, this);
        }

        public bool IsElementHighlighterEnabled()
        {
            return _sweetPotatoSettings.IsElementHighlighterEnabled();
        }

        public TimeSpan GetElementWaitTimeSpan()
        {
            var waitTimeMilliseconds = _sweetPotatoSettings.GetWaitTimeMilliseconds();

            return new TimeSpan(0, 0, 0, 0, Convert.ToInt16(waitTimeMilliseconds));
        }
    }
}