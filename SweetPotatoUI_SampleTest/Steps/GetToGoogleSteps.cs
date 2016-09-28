using NUnit.Framework;
using SweetPotatoUI.CommonSteps;
using TechTalk.SpecFlow;

namespace SweetPotatoUI_SampleTests.Steps
{
    [Binding]
    public class GetToGoogleSteps
    {
        private const string GooglePageName = "GooglePage";

        [Given(@"I have Navigated to Google")]
        public void GivenIHaveNavigatedToGoogle()
        {
            BaseSteps.GetAutomationBrowser().NavigateTo("http://www.google.com");
        }

        [When(@"I enter '(.*)' into the '(.*)'")]
        public void WhenIEnterIntoTheSearchBox(string searchCriteria, string elementLookupAlias)
        {
            var element = BaseSteps.FindElement(GooglePageName, elementLookupAlias);

            using (element)
            {
                element.Fill(searchCriteria);
            }
        }

        [When(@"I click element '(.*)'")]
        public void WhenIClickElement(string elementAlias)
        {
            var automationElement = BaseSteps.FindElement(GooglePageName, elementAlias);

            using (automationElement)
            {
                automationElement.Click();
            }
        }

        [Then(@"element '(.*)' contains text '(.*)'")]
        public void ThenElementWithAliasContainsText(string elementAlias, string expectedText)
        {
            var element = BaseSteps.FindElement(GooglePageName, elementAlias);

            string actualText;

            using (element)
            {
                actualText = element.GetText();
            }

            Assert.IsTrue(actualText.Contains(expectedText));
        }

        [Then(@"the current page title is '(.*)'")]
        public static void ThenTheCurrentPageTitleIs(string pageTitle)
        {
            var actual = BaseSteps.GetAutomationBrowser().GetTitle();

            Assert.AreEqual(pageTitle, actual);
        }
    }
}