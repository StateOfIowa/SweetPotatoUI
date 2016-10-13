using SweetPotatoUI.DriverImplementation.Selenium;
using SweetPotatoUI.Enums;
using TechTalk.SpecFlow;

namespace SweetPotatoUI.DriverImplementation
{
    public class AutomationBrowserFactory
    {
        public IAutomationBrowser CreateBrowser(ISweetPotatoSettings sweetPotatoSettings)
        {
            ScenarioContext.Current.Set(sweetPotatoSettings.GetExecutingAssemblyName(),
                Constants.ImplementingAssemblyName);
            //ScenarioContext.Current.Set(sweetPotatoSettings.GetWaitTimeMilliseconds(),
            //    Constants.WaitTimeMilliseconds);
            //ScenarioContext.Current.Set(sweetPotatoSettings.IsElementHighlighterEnabled(),
            //    Constants.IsElementHighlighterEnabled);

            switch (sweetPotatoSettings.GetDriverType())
            {
                case DriverType.Selenium:
                    var automationBrowser = GetSeleniumBrowser(sweetPotatoSettings);
                    SetAutomationBrowserToScenarioContext(automationBrowser);
                    return automationBrowser;
                default:
                    var seleniumBrowser = GetSeleniumBrowser(sweetPotatoSettings);
                    SetAutomationBrowserToScenarioContext(seleniumBrowser);
                    return seleniumBrowser;
            }
        }

        private static void SetAutomationBrowserToScenarioContext(IAutomationBrowser automationBrowser)
        {
            ScenarioContext.Current.Set(automationBrowser, Constants.AutomationBrowserKey);
        }

        private static IAutomationBrowser GetSeleniumBrowser(ISweetPotatoSettings sweetPotatoSettings)
        {
            var seleniumDriverFactory = new SeleniumDriverFactory();
            return new SeleniumBrowser(seleniumDriverFactory, sweetPotatoSettings, new SeleniumElementFactory());
        }
    }
}