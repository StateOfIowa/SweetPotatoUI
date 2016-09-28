using AutoMoq;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;
using SweetPotatoUI;
using SweetPotatoUI.DriverImplementation.Selenium;
using SweetPotatoUI.Enums;

namespace SweetPotatoUI_UnitTests.DriverImplementation.Selenium
{
    [TestFixture]
    public class SeleniumBrowserTests
    {
        [SetUp]
        public void Setup()
        {
            _autoMoqer = new AutoMoqer();
            _autoMoqer.GetMock<ISeleniumDriverFactory>()
                .Setup(s => s.Create(It.IsAny<BrowserType>(), It.IsAny<string>()))
                .Returns(_autoMoqer.GetMock<IWebDriver>().Object);

            _seleniumBrowser = _autoMoqer.Resolve<SeleniumBrowser>();
        }

        private SeleniumBrowser _seleniumBrowser;
        private AutoMoqer _autoMoqer;

        [Test]
        public void AddElement_addsElementToElementList()
        {
            var elementLookup = new ElementLookup
            {
                PageName = "TestPage",
                Alias = "TestElement",
                Id = "testElement"
            };

            _seleniumBrowser.AddElement(elementLookup);

            Assert.AreEqual(1, _seleniumBrowser.GetElements.Count);
        }

        [Test]
        public void Dispose_disposesWebDriver()
        {
            _seleniumBrowser.Dispose();

            _autoMoqer
                .GetMock<IWebDriver>()
                .Verify(v => v.Dispose());
        }

        [Test]
        public void FindElementById_callSeleniumElementFactoryWithIdLocator()
        {
            _seleniumBrowser.FindElementById("testElement");

            _autoMoqer.GetMock<ISeleniumElementFactory>()
                .Verify(
                    v =>
                        v.Create(It.IsAny<IWebDriver>(), It.Is<By>(b => b.ToString() == "By.Id: testElement"),
                            It.IsAny<SeleniumBrowser>()));
        }

        [Test]
        public void FindElementByLinkCssSelector_callSeleniumElementFactoryWithTagNameLocator()
        {
            _seleniumBrowser.FindElementByLinkCssSelector("testElement");

            _autoMoqer.GetMock<ISeleniumElementFactory>()
                .Verify(
                    v =>
                        v.Create(It.IsAny<IWebDriver>(), It.Is<By>(b => b.ToString() == "By.CssSelector: testElement"),
                            It.IsAny<SeleniumBrowser>()));
        }

        [Test]
        public void FindElementByLinkText_callSeleniumElementFactoryWithLinkTextLocator()
        {
            _seleniumBrowser.FindElementByLinkText("testElement");

            _autoMoqer.GetMock<ISeleniumElementFactory>()
                .Verify(
                    v =>
                        v.Create(It.IsAny<IWebDriver>(), It.Is<By>(b => b.ToString() == "By.LinkText: testElement"),
                            It.IsAny<SeleniumBrowser>()));
        }

        [Test]
        public void FindElementByName_callSeleniumElementFactoryWithNameLocator()
        {
            _seleniumBrowser.FindElementByName("testElement");

            _autoMoqer.GetMock<ISeleniumElementFactory>()
                .Verify(v => v.Create(It.IsAny<IWebDriver>(), It.Is<By>(b => b.ToString() == "By.Name: testElement"),
                    It.IsAny<SeleniumBrowser>()));
        }

        [Test]
        public void FindElementByTagName_callSeleniumElementFactoryWithTagNameLocator()
        {
            _seleniumBrowser.FindElementByTagName("testElement");

            _autoMoqer.GetMock<ISeleniumElementFactory>()
                .Verify(
                    v =>
                        v.Create(It.IsAny<IWebDriver>(), It.Is<By>(b => b.ToString() == "By.TagName: testElement"),
                            It.IsAny<SeleniumBrowser>()));
        }

        [Test]
        public void FindElementByXPath_callSeleniumElementFactoryWithXPathLocator()
        {
            _seleniumBrowser.FindElementByXPath("testElement");

            _autoMoqer.GetMock<ISeleniumElementFactory>()
                .Verify(
                    v =>
                        v.Create(It.IsAny<IWebDriver>(), It.Is<By>(b => b.ToString() == "By.XPath: testElement"),
                            It.IsAny<SeleniumBrowser>()));
        }

        [Test]
        public void GetTitle_returnsTitleFromWebDriver()
        {
            _autoMoqer.GetMock<IWebDriver>()
                .Setup(s => s.Title)
                .Returns("Title");

            var title = _seleniumBrowser.GetTitle();

            Assert.AreEqual("Title", title);
        }

        [Test]
        public void GetWindowHandle_returnsWebDriverWindowHandle()
        {
            _autoMoqer.GetMock<IWebDriver>()
                .Setup(s => s.CurrentWindowHandle)
                .Returns("CurrentWindowHandle");

            var windowHandle = _seleniumBrowser.GetWindowHandle();

            Assert.AreEqual("CurrentWindowHandle", windowHandle);
        }

        [Test]
        public void NavigateTo_setsWebDriverUrl()
        {
            _seleniumBrowser.NavigateTo("http://www.google.com");

            _autoMoqer.GetMock<IWebDriver>()
                .VerifySet(driver => driver.Url = "http://www.google.com");
        }

        [Test]
        public void PageSource_returnsWebDriverPageSource()
        {
            _autoMoqer.GetMock<IWebDriver>()
                .Setup(s => s.PageSource)
                .Returns("Foo Html");

            Assert.AreEqual("Foo Html", _seleniumBrowser.PageSource());
        }
    }
}