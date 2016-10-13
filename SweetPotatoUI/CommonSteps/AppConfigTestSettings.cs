using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using SweetPotatoUI.DriverImplementation.Selenium;
using SweetPotatoUI.Enums;

namespace SweetPotatoUI.CommonSteps
{
    public class AppConfigTestSettings : ISweetPotatoSettings
    {
        private readonly BrowserType _browserTypeEnum;
        private readonly DriverType _driverTypeEnum;

        public AppConfigTestSettings(BrowserType browserTypeEnum, DriverType driverTypeEnum)
        {
            _browserTypeEnum = browserTypeEnum;
            _driverTypeEnum = driverTypeEnum;
        }

        public BrowserType GetBrowserType()
        {
            return _browserTypeEnum;
        }

        public DriverType GetDriverType()
        {
            return _driverTypeEnum;
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
            var assemblyName = ConfigurationManager.AppSettings["ImplementingAssemblyName"];

            if (string.IsNullOrEmpty(assemblyName))
            {
                throw new NullReferenceException("Your App.config file does not specify a value for " +
                                                 "[ImplementingAssemblyName]. This value must provided if you are" +
                                                 "using the base step implementation to start the automation browser.");
            }

            return assemblyName;
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