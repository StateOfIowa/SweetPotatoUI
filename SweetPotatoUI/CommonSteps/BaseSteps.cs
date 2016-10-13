using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using SweetPotatoUI.DriverImplementation;
using SweetPotatoUI.Enums;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SweetPotatoUI.CommonSteps
{
    [Binding]
    public static class BaseSteps
    {
        public static T GetFromScenarioContext<T>()
        {
            return ScenarioContext.Current.Get<T>();
        }

        public static object GetFromScenarioContext(string key)
        {
            object value;

            ScenarioContext.Current.TryGetValue(key, out value);

            return value;
        }

        public static T GetFromScenarioContext<T>(string key)
        {
            return ScenarioContext.Current.Get<T>(key);
        }

        public static void SetToScenarioContext<T>(T value)
        {
            ScenarioContext.Current.Set(value);
        }

        public static IAutomationBrowser GetAutomationBrowser()
        {
            var automationBrowser = GetFromScenarioContext(Constants.AutomationBrowserKey);

            return automationBrowser as IAutomationBrowser;
        }

        public static void SetAutomationBrowser(IAutomationBrowser automationBrowser)
        {
            ScenarioContext.Current.Set(automationBrowser, Constants.AutomationBrowserKey);
        }

        public static IAutomationElement FindElement(string pageName, string elementLookupAlias)
        {
            ElementLookup elementLookup;

            try
            {
                elementLookup = GetAutomationBrowser()
                    .GetElements
                    .SingleOrDefault(e => e.Alias == elementLookupAlias
                                          && e.PageName == pageName);
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException(
                    string.Format("Element with alias [{0}] was found multiple times in your Page Elements features. " +
                                  "There can only be one occurrence of the same element alias and pagename combination in your " +
                                  "page elements features ", elementLookupAlias));
            }

            if (elementLookup == null)
            {
                throw new Exception(
                    string.Format(
                        "Element with alias [{0}] was not found for page name [{1}] in your Page Elements table.",
                        elementLookupAlias, pageName));
            }

            if (!string.IsNullOrWhiteSpace(elementLookup.Id))
            {
                return GetAutomationBrowser().FindElementById(elementLookup.Id);
            }

            if (!string.IsNullOrWhiteSpace(elementLookup.Name))
            {
                return GetAutomationBrowser().FindElementByName(elementLookup.Name);
            }

            if (!string.IsNullOrWhiteSpace(elementLookup.TagName))
            {
                return GetAutomationBrowser().FindElementByTagName(elementLookup.TagName);
            }

            if (!string.IsNullOrWhiteSpace(elementLookup.LinkText))
            {
                return GetAutomationBrowser().FindElementByLinkText(elementLookup.LinkText);
            }

            if (!string.IsNullOrWhiteSpace(elementLookup.PartialLinkText))
            {
                return GetAutomationBrowser().FindElementByPartialLinkText(elementLookup.PartialLinkText);
            }

            if (!string.IsNullOrWhiteSpace(elementLookup.CssSelector))
            {
                return GetAutomationBrowser().FindElementByLinkCssSelector(elementLookup.CssSelector);
            }

            if (!string.IsNullOrWhiteSpace(elementLookup.XPath))
            {
                return GetAutomationBrowser().FindElementByXPath(elementLookup.XPath);
            }

            return null;
        }

        [Given(@"Page Elements Are Loaded")]
        public static void GivenPageElementsAreLoaded(Table table)
        {
            Console.WriteLine("*********** [Loading Elements] ***********");
            var elements = table.CreateSet<ElementLookup>().ToList();

            foreach (var elementLookup in elements)
            {
                if (GetAutomationBrowser().GetElements.Contains(elementLookup))
                {
                    continue;
                }

                GetAutomationBrowser().AddElement(elementLookup);
            }
        }

        [Given(@"'(.*)' Page Elements have been loaded")]
        public static void GivenFeaturePageElementsHaveBeenLoaded(string featureFileName)
        {
            var implementingAssemblyName = ScenarioContext.Current.Get<string>(Constants.ImplementingAssemblyName);
            var implementingAssembly = Assembly.Load(implementingAssemblyName);

            if (implementingAssembly == null)
            {
                throw new Exception(string.Format("Could not load Assembly [{0}].", implementingAssemblyName));
            }

            try
            {
                var featureFile = implementingAssembly.GetTypes()
                    .First(t => t.Name == featureFileName + "Feature");

                var featureFileInstance = Activator.CreateInstance(featureFile);

                var featureSetupMethod = featureFile.GetMethod("FeatureSetup");
                var featureBackgroundMethod = featureFile.GetMethod("FeatureBackground");

                featureSetupMethod.Invoke(featureFileInstance, null);
                featureBackgroundMethod.Invoke(featureFileInstance, null);
            }
            catch (InvalidOperationException)
            {
                throw new Exception(
                    "The Page Element Feature name you attempted to load was not found in the project." +
                    "Please verify the PageElements feature name you passed in to the [Given '[PageElementsFeatureNameHere]' Page Elements have been loaded]" +
                    "matches the name defined in that feature file and that you only have one feature file of that name.");
            }
        }

        [Given(@"I have started the browser '(.*)' using the driver '(.*)'")]
        public static void GivenIHaveStartedTheBrowserUsingTheDriver(string browserType, string driverType)
        {
            var browserTypeEnum = (BrowserType) Enum.Parse(typeof(BrowserType), browserType);
            var driverTypeEnum = (DriverType) Enum.Parse(typeof(DriverType), driverType);

            var sweetPotatoSettings = new AppConfigTestSettings(browserTypeEnum, driverTypeEnum);
            var automationBrowserFactory = new AutomationBrowserFactory();

            var automationBrowser = automationBrowserFactory.CreateBrowser(sweetPotatoSettings);

            ScenarioContext.Current.Set(automationBrowser, "AutomationBrowserKey");
        }

        [When(@"I Navigate to '(.*)'")]
        public static void WhenINavigatedToUrl(string url)
        {
            GetAutomationBrowser().NavigateTo(url);
        }

        [When(@"I enter '(.*)' into the '(.*)' element of the '(.*)' page")]
        public static void WhenIEnterIntoTheElementOfThePage(string value, string elementAlias, string pageName)
        {
            var element = FindElement(pageName, elementAlias);

            using (element)
            {
                element.Fill(value);
            }
        }

        [When(@"I fill Element of the '(.*)' page")]
        public static void WhenIFillElementsOfThePage(string pageName, Table table)
        {
            foreach (var row in table.Rows)
            {
                var element = FindElement(pageName, row["ElementAlias"]);

                using (element)
                {
                    element.Fill(row["Data"]);
                }
            }
        }

        [When(@"I click the '(.*)' element of the '(.*)' page")]
        public static void WhenIClickTheElementOfThePage(string elementLookupAlias, string pageName)
        {
            using (var element = FindElement(pageName, elementLookupAlias))
            {
                element.Click();
            }
        }

        [When(@"I tab away from Element '(.*)' of the '(.*)' page")]
        public static void WhenITabAwayFromElement(string elementLookupAlias, string pageName)
        {
            var element = FindElement(pageName, elementLookupAlias);

            using (element)
            {
                element.TabAway();
            }
        }

        [When(@"I move to the top of the current page")]
        public static void WhenIMoveToTheTopOfTheCurrentPage()
        {
            var automationBrowser = GetAutomationBrowser();
            automationBrowser.MoveToTopOfCurrentPage();
        }

        [When(@"I move to the bottom of the current page")]
        public static void WhenIMoveToTheBottomOfTheCurrentPage()
        {
            var automationBrowser = GetAutomationBrowser();
            automationBrowser.MoveToBottomOfCurrentPage();
        }

        [When(@"I scroll to the '(.*)' element of the '(.*)'")]
        public static void WhenIScrollToTheElementOfThePage(string elementAlias, string pageName)
        {
            var automationElement = FindElement(pageName, elementAlias);

            using (automationElement)
            {
                automationElement.ScrollIntoView();
            }
        }

        [When(@"I Move to '(.*)' element of the '(.*)' page")]
        public static void WhenIMoveToElementOfThePage(string element, string pageName)
        {
            var automationElement = FindElement(pageName, element);

            using (automationElement)
            {
                automationElement.MoveTo();
            }
        }

        [Then(@"I see text on page '(.*)'")]
        public static void ThenISeeTextOnPage(string expectedText)
        {
            var pageSource = GetAutomationBrowser().PageSource();
            Assert.IsTrue(pageSource.Contains(expectedText),
                string.Format("The current page source: ['{0}'] did not contain the expected text: ['{1}'].", pageSource,
                    expectedText));
        }

        [Then(@"I see Page with Title '(.*)'")]
        public static void ThenISeePageWithTitleText(string expectedTitle)
        {
            var stopwatch = new Stopwatch();
            var actualTitle = string.Empty;
            var automationBrowser = GetAutomationBrowser();

            stopwatch.Start();

            while (stopwatch.Elapsed < automationBrowser.GetElementWaitTimeSpan())
            {
                actualTitle = automationBrowser.GetTitle();

                if (expectedTitle == actualTitle)
                {
                    break;
                }
            }

            Assert.AreEqual(expectedTitle, actualTitle,
                string.Format("The current page title: ['{0}'] did not match the expected title: ['{1}']", actualTitle,
                    expectedTitle));
        }

        [Then(@"I see Page with Title containing text '(.*)'")]
        public static void ThenISeePageWithTitleContainingText(string expectedText)
        {
            var title = GetAutomationBrowser().GetTitle();
            Assert.IsTrue(title.Contains(expectedText),
                string.Format("The current page title: ['{0}'] did not contain the expected text: ['{1}'].", title,
                    expectedText));
        }

        [Then(@"'(.*)' has Displayed Elements")]
        public static void ThenPageHasDisplayedElements(string pageName, Table table)
        {
            foreach (var row in table.Rows)
            {
                var elementLookupAlias = row["ElementAlias"];
                var automationElement = FindElement(pageName, elementLookupAlias);

                using (automationElement)
                {
                    Assert.IsTrue(automationElement.IsDisplayed(),
                        string.Format("Element with Alias: [{0}] was not Displayed.", elementLookupAlias));
                }
            }
        }

        [Then(@"'(.*)' has Hidden Elements")]
        public static void ThenPageHasHiddenElements(string pageName, Table table)
        {
            foreach (var row in table.Rows)
            {
                var elementLookupAlias = row["ElementAlias"];
                var automationElement = FindElement(pageName, elementLookupAlias);

                using (automationElement)
                {
                    Assert.IsFalse(automationElement.IsDisplayed(),
                        string.Format("Element with Alias: [{0}] not Hidden.", elementLookupAlias));
                }
            }
        }

        [Then(@"'(.*)' has Enabled Elements")]
        public static void ThenPageHasEnabledElements(string pageName, Table table)
        {
            foreach (var row in table.Rows)
            {
                var elementLookupAlias = row["ElementAlias"];
                var element = FindElement(pageName, elementLookupAlias);

                using (element)
                {
                    Assert.IsTrue(element.IsEnabled(),
                        string.Format("Element with Alias: [{0}] was not Enabled.", elementLookupAlias));
                }
            }
        }

        [Then(@"'(.*)' has Disabled Elements")]
        public static void ThenPageHasDisabledElements(string pageName, Table table)
        {
            foreach (var row in table.Rows)
            {
                var elementLookupAlias = row["ElementAlias"];
                var automationElement = FindElement(pageName, elementLookupAlias);

                using (automationElement)
                {
                    Assert.IsFalse(automationElement.IsEnabled(),
                        string.Format("Element with Alias: [{0}] was not Disabled.", elementLookupAlias));
                }
            }
        }

        [Then(@"'(.*)' has Selected Elements")]
        public static void ThenPageHasSelectedElements(string pageName, Table table)
        {
            foreach (var row in table.Rows)
            {
                var elementLookupAlias = row["ElementAlias"];
                var automationElement = FindElement(pageName, elementLookupAlias);

                using (automationElement)
                {
                    Assert.IsTrue(automationElement.IsSelected(),
                        string.Format("Element with Alias: [{0}] was not Selected", elementLookupAlias));
                }
            }
        }

        [Then(@"'(.*)' has non Selected Elements")]
        public static void ThenPageHasNonSelectedElements(string pageName, Table table)
        {
            foreach (var row in table.Rows)
            {
                var elementLookupAlias = row["ElementAlias"];
                var automationElement = FindElement(pageName, elementLookupAlias);

                using (automationElement)
                {
                    Assert.IsFalse(automationElement.IsSelected(),
                        string.Format("Element with Alias: [{0}] was Selected", elementLookupAlias));
                }
            }
        }

        [Then(@"'(.*)' has elements with Exact Text")]
        public static void ThenPageHasElementWithExactText(string pageName, Table table)
        {
            foreach (var row in table.Rows)
            {
                var automationElement = FindElement(pageName, row["ElementAlias"]);
                using (automationElement)
                {
                    var elementText = automationElement.GetText();

                    Assert.AreEqual(row["Text"], elementText, string.Format(
                        "The element with alias: [{0}] did not match text: ['{1}'] as expected.  Actual text was: ['{2}']",
                        row["ElementAlias"], row["Text"], elementText));
                }
            }
        }

        [Then(@"'(.*)' has elements that contain Text")]
        public static void ThenPageHasElementsThatContainsText(string pageName, Table table)
        {
            foreach (var row in table.Rows)
            {
                var element = FindElement(pageName, row["ElementAlias"]);

                using (element)
                {
                    var elementText = element.GetText();

                    Assert.IsTrue(elementText.Contains(row["Text"]),
                        string.Format(
                            "The element with alias: [{0}] did not contain text: ['{1}'] as expected.  Actual text was: ['{2}']",
                            row["ElementAlias"], row["Text"], elementText));
                }
            }
        }

        [Then(@"'(.*)' has elements with Exact Value")]
        public static void ThenPageHasElementWithExactValue(string pageName, Table table)
        {
            foreach (var row in table.Rows)
            {
                var actualValue = string.Empty;
                var stopwatch = new Stopwatch();

                stopwatch.Start();

                while (stopwatch.Elapsed < GetAutomationBrowser().GetElementWaitTimeSpan())
                {
                    var element = FindElement(pageName, row["ElementAlias"]);

                    using (element)
                    {
                        actualValue = element.GetValue();
                        if (actualValue == row["Value"])
                        {
                            break;
                        }
                    }
                }

                Assert.AreEqual(row["Value"], actualValue, string.Format(
                    "The element with alias: [{0}] did not match value: ['{1}'] as expected.  Actual value was: ['{2}']",
                    row["ElementAlias"], row["Value"], actualValue));
            }
        }

        [Then(@"'(.*)' has elements that contain Value")]
        public static void ThenPageHasElementsThatContainsValue(string pageName, Table table)
        {
            foreach (var row in table.Rows)
            {
                var element = FindElement(pageName, row["ElementAlias"]);

                string elementValue;

                using (element)
                {
                    elementValue = element.GetValue();
                }

                Assert.IsTrue(elementValue.Contains(row["Value"]), string.Format(
                    "The element with alias: [{0}] did not match value: ['{1}'] as expected.  Actual value was: ['{2}']",
                    row["ElementAlias"], row["Value"], elementValue));
            }
        }

        public static string GetWindowHandle()
        {
            return GetAutomationBrowser().GetWindowHandle();
        }

        public static void FocusOnWindow(string windowHandle)
        {
            GetAutomationBrowser().FocusOnWindow(windowHandle);
        }

        [AfterScenario]
        private static void Teardown()
        {
            GetAutomationBrowser().Dispose();
        }
    }
}