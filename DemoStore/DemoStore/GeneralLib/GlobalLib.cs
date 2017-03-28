using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;

namespace DemoStore.GeneralLib
{
    class GlobalLib
    {
        public static IWebDriver wDriver;
        public static WebDriverWait wDriverWait;

        public static IWebDriver driver
        {
            get
            {
                return wDriver;
            }
            set
            {
                wDriver = value;
            }
        }
        public static WebDriverWait driverWait
        {
            get
            {
                wDriverWait = new WebDriverWait(wDriver, TimeSpan.FromSeconds(2000));

                return wDriverWait;
            }
            set
            {
                wDriverWait = value;
            }
        }

        public static string readConfig(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public IWebDriver SelectBrowserAndNavigate()
        {
            if (readConfig("browser").ToUpper().Contains("FIRE"))
            {
                driver = new FirefoxDriver();
            }
            else
                if (readConfig("browser").ToUpper().Contains("CHROME"))
            {
                driver = new ChromeDriver(@"C:\Users\P2SI\Documents\Visual Studio 2015\Projects\SampleSelenium\packages\Selenium.WebDriver.ChromeDriver.2.28.0\driver");
            }
            else
                if (readConfig("browser").ToUpper().Contains("EDGE"))
            { }
            driver.Manage().Window.Maximize();
            string url = readConfig("url");
            driver.Navigate().GoToUrl(readConfig("url"));
            return driver;
        }

        public static void CloseAllBrowsers()
        {
            string closeBrowser = readConfig("browser");
            string processName = string.Empty;

            if (closeBrowser.ToUpper().Contains("FIREFOX"))
                processName = "firefox";
            else if (closeBrowser.ToUpper().Contains("CHROME"))
                processName = "chrome";
            else if (closeBrowser.ToUpper().Contains("SAFARI"))
                processName = "Safari";
            else if (closeBrowser.ToUpper().Contains("INTERNET"))
                processName = "iexplore";

            Process[] processNames = Process.GetProcessesByName(processName);
            foreach (Process item in processNames)
            {
                if (driver != null)
                    driver.Close();
                if (!item.HasExited)
                    item.Kill();
            }
        }

    }
}
