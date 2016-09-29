using OpenQA.Selenium;

namespace SweetPotatoUI.DriverImplementation.Selenium
{
    public class SeleniumElementHighlighter
    {
        private readonly IWebDriver _driver;
        private readonly IWebElement _element;
        private readonly string _originalColorValue;

        public SeleniumElementHighlighter(IWebDriver driver, IWebElement element)
        {
            _driver = driver;
            _element = element;
            _originalColorValue = element.GetCssValue("background-color");
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
            try
            {
                ExecuteJavaScript(restoreBackgroundColorScript);
            }
            catch (StaleElementReferenceException)
            {
            }
        }

        private void ExecuteJavaScript(string setBackgroundColorScript)
        {
            var js = _driver as IJavaScriptExecutor;
            js.ExecuteScript(setBackgroundColorScript, _element);
        }
    }
}