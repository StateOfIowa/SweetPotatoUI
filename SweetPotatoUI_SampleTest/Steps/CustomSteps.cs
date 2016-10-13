using SweetPotatoUI;
using SweetPotatoUI.DriverImplementation;
using SweetPotatoUI_SampleTests.Settings;
using TechTalk.SpecFlow;

namespace SweetPotatoUI_SampleTests.Steps
{
    [Binding]
    public sealed class CustomSteps
    {
        [Given(@"the AutomationDriver has started using the configurations specified in App Config")]
        public static void GivenTheAutomationDriverHasStartedUsingConfigurationSpecifiedInAppConfig()
        {
            var automationBrowserFactory = new AutomationBrowserFactory();
            var sweetPotatoSettings = new AppConfigSweetPotatoSettings();

            var automationBrowser = automationBrowserFactory.CreateBrowser(sweetPotatoSettings);
            ScenarioContext.Current.Set(automationBrowser, Constants.AutomationBrowserKey);
        }
    }
}