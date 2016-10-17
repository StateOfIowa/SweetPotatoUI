using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using SweetPotatoUI.DriverImplementation.Selenium;
using SweetPotatoUI.Enums;

namespace SweetPotatoUI.CommonSteps
{
    public class DefaultAppConfigSettings : ISweetPotatoSettings
    {
        private readonly BrowserType _browserTypeEnum;
        private readonly DriverType _driverTypeEnum;

        public DefaultAppConfigSettings(BrowserType browserTypeEnum, DriverType driverTypeEnum)
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
            var driverExecutablesPath = ConfigurationManager.AppSettings["DriverPath"];

            if (string.IsNullOrEmpty(driverExecutablesPath))
            {
                throw new NullReferenceException("Your App.config file does not specify a value for " +
                                                 "[DriverPath]. This value must provided if you are" +
                                                 "using the base step implementation to start the automation browser.");
            }

            var assemblyFile = new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath;
            var parentPath = assemblyFile + driverExecutablesPath;
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
            var waitTime = ConfigurationManager.AppSettings["WaitTimeMilliseconds"];

            if (string.IsNullOrEmpty(waitTime))
            {
                throw new NullReferenceException("Your App.config file does not specify a value for " +
                                                 "[waitTime]. This value must provided if you are" +
                                                 "using the base step implementation to start the automation browser.");
            }

            return Convert.ToInt32(waitTime);
        }

        public bool IsElementHighlighterEnabled()
        {
            var isHighlighterEnabled = ConfigurationManager.AppSettings["IsElementHighlighterEnabled"];

            if (string.IsNullOrEmpty(isHighlighterEnabled))
            {
                throw new NullReferenceException("Your App.config file does not specify a value for " +
                                                 "[isHighlighterEnabled]. This value must provided if you are" +
                                                 "using the base step implementation to start the automation browser.");
            }

            return Convert.ToBoolean(isHighlighterEnabled);
        }
    }
}