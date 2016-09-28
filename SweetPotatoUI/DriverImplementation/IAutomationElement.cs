using System;

namespace SweetPotatoUI.DriverImplementation
{
    public interface IAutomationElement : IDisposable
    {
        void Click();
        void TabAway();
        bool IsDisplayed();
        bool IsSelected();
        bool IsEnabled();

        string GetValue();
        string GetText();
        string GetAttribute(string attributeName);

        void Fill(string inputText);
        void MoveTo();
        void Clear();
        void ScrollIntoView();
    }
}