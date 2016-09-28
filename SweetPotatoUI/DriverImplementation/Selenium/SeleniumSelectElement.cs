using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SweetPotatoUI.DriverImplementation.Selenium
{
    public class SeleniumSelectElement : SeleniumElement
    {
        public SeleniumSelectElement(IWebDriver driver, By by, SeleniumBrowser seleniumBrowser)
            : base(driver, by, seleniumBrowser)
        {
        }

        public override string GetValue()
        {
            var selectedOption = GetSelectElement().SelectedOption;
            var attribute = selectedOption.GetAttribute("value");

            return attribute;
        }

        private SelectElement GetSelectElement()
        {
            return new SelectElement(SeleniumHelper.FindWebElement(Driver, By,
                SeleniumBrowser.GetElementWaitTimeSpan()));
        }

        public IList<IWebElement> GetAllSelectedValues()
        {
            return GetSelectElement().AllSelectedOptions;
        }

        public override string GetText()
        {
            var selectedOption = GetSelectElement().SelectedOption;

            return selectedOption.Text;
        }

        public override void Fill(string selectValue)
        {
            PickFromASelectOption(selectValue);
        }

        public override void Clear()
        {
            GetSelectElement().DeselectAll();
        }

        public List<string> GetAllOptionValues()
        {
            var selectOptions = GetSelectElement().Options;
            var optionList = new List<string>();

            foreach (var selectOption in selectOptions)
            {
                optionList.Add(selectOption.GetAttribute("value"));
            }

            return optionList;
        }

        private void PickFromASelectOption(string selectValue)
        {
            try
            {
                Wait.Until(ExpectedConditions.ElementExists(By));
            }
            catch (TimeoutException)
            {
                throw new NoSuchElementException(
                    string.Format(
                        "The value: [{0}] was not found in the available options the Element with locator: [{1}].",
                        selectValue, By));
            }

            GetSelectElement().SelectByText(selectValue);
        }
    }
}