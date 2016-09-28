using SweetPotatoUI.Enums;

namespace SweetPotatoUI.DriverImplementation.Selenium
{
    public interface ISweetPotatoSettings
    {
        BrowserType GetBrowserType();
        DriverType GetDriverType();
        string GetDriverPath();
        string GetExecutingAssemblyName();
        int GetWaitTimeMilliseconds();
        bool IsElementHighlighterEnabled();
    }
}