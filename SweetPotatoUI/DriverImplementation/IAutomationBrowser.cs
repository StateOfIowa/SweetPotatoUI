using System;
using System.Collections.Generic;

namespace SweetPotatoUI.DriverImplementation
{
    public interface IAutomationBrowser
    {
        ICollection<ElementLookup> GetElements { get; }
        IAutomationElement FindElementByName(string name);
        IAutomationElement FindElementById(string id);
        IAutomationElement FindElementByLinkText(string linkText);
        IAutomationElement FindElementByTagName(string tagText);
        IAutomationElement FindElementByLinkCssSelector(string cssSelector);
        IAutomationElement FindElementByXPath(string xpath);

        TimeSpan GetElementWaitTimeSpan();
        string PageSource();

        void Dispose();
        void NavigateTo(string url);
        string GetTitle();
        string GetWindowHandle();
        void FocusOnWindow(string windowHandle);
        void AddElement(ElementLookup elements);
        void MoveToTopOfCurrentPage();
        void MoveToBottomOfCurrentPage();
    }
}