using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using SweetPotatoUI;
using SweetPotatoUI.DriverImplementation.Selenium;
using SweetPotatoUI.Enums;

namespace SweetPotatoUI_SampleTests.Settings
{
    internal class AppConfigSweetPotatoSettings : ISweetPotatoSettings
    {
        public BrowserType GetBrowserType()
        {
            var browswerType = ConfigurationManager.AppSettings["browserType"];

            if (string.IsNullOrEmpty(browswerType))
            {
                throw new Exception("You are required to set Configuration App Setting [DriverType].");
            }

            return (BrowserType) Enum.Parse(typeof(BrowserType), browswerType);
        }

        public DriverType GetDriverType()
        {
            var browserType = ConfigurationManager.AppSettings["driverType"];

            if (string.IsNullOrEmpty(browserType))
            {
                throw new Exception("You are required to set Configuration App Setting [BrowserType].");
            }

            return (DriverType) Enum.Parse(typeof(DriverType),
                browserType);
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
            var waitTimeMilliseconds = ConfigurationManager.AppSettings[Constants.WaitTimeMilliseconds];

            if (string.IsNullOrWhiteSpace(waitTimeMilliseconds))
            {
                throw new Exception("You are required to set Configuration App Setting [WaitTimeMilliseconds].");
            }

            return Convert.ToInt32(waitTimeMilliseconds);
        }

        public bool IsElementHighlighterEnabled()
        {
            var isElementHighlighterEnabled = ConfigurationManager.AppSettings[Constants.IsElementHighlighterEnabled];

            if (string.IsNullOrEmpty(isElementHighlighterEnabled))
            {
                throw new Exception("You are required to set Configuration App Setting [IsElementHighlighterEnabled].");
            }

            return Convert.ToBoolean(isElementHighlighterEnabled);
        }
    }
}