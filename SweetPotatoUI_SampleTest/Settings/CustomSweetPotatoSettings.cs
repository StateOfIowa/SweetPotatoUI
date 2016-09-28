using System;
using System.IO;
using System.Reflection;
using SweetPotatoUI.DriverImplementation.Selenium;
using SweetPotatoUI.Enums;

namespace SweetPotatoUI_SampleTests.Settings
{
    internal class CustomSweetPotatoSettings : ISweetPotatoSettings
    {
        private readonly BrowserType _browserType;
        private readonly DriverType _driverType;

        public CustomSweetPotatoSettings(BrowserType browserType, DriverType driverType)
        {
            _browserType = browserType;
            _driverType = driverType;
        }

        public BrowserType GetBrowserType()
        {
            return _browserType;
        }

        public DriverType GetDriverType()
        {
            return _driverType;
        }

        public string GetDriverPath()
        {
            var assemblyFile = new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath;
            var parentPath = assemblyFile + "/../DriverExecutables/Selenium/";
            var directoryPath = Uri.UnescapeDataString(parentPath);

            return Path.GetDirectoryName(directoryPath);
        }

        public string GetExecutingAssemblyName()
        {
            var executingAssemblyName = Assembly.GetExecutingAssembly().FullName;

            return executingAssemblyName;
        }

        public int GetWaitTimeMilliseconds()
        {
            return 5000;
        }

        public bool IsElementHighlighterEnabled()
        {
            return true;
        }
    }
}