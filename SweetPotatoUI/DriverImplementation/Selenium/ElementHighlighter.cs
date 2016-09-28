using System;
using OpenQA.Selenium;

namespace SweetPotatoUI.DriverImplementation.Selenium
{
    public class ElementHighlighter
    {
        private readonly By _by;
        private readonly IWebDriver _driver;
        private readonly string _originalColorValue;
        private readonly TimeSpan _timeSpan;

        public ElementHighlighter(IWebDriver driver, By by, TimeSpan timeSpan)
        {
            _driver = driver;
            _by = by;
            _timeSpan = timeSpan;
            _originalColorValue = SeleniumHelper.FindWebElement(_driver, _by, _timeSpan)
                .GetCssValue("background-color");
        }

        internal void HighlightElementBackground()
        {
            var setBackgroundColorScript = 
                @"arguments[0].style.backgroundColor='DarkOrange'";
            ExecuteJavaScript(setBackgroundColorScript);
        }

        internal void RestoreBackgroundColor()
        {
            var restoreBackgroundColorScript = 
                string.Format(@"arguments[0].style.backgroundColor='{0}'",
                _originalColorValue);
            ExecuteJavaScript(restoreBackgroundColorScript);
        }

        private void ExecuteJavaScript(string setBackgroundColorScript)
        {
            var js = _driver as IJavaScriptExecutor;
            js.ExecuteScript(setBackgroundColorScript,
                SeleniumHelper.FindWebElement(_driver, _by, _timeSpan));
        }
    }
}