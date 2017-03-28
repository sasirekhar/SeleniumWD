using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.Collections.Generic;

namespace DemoStore.Scripts
{
    [TestClass]
    public class TestCase2
    {
        [TestMethod]
        public void Yahoo_DeleteMails()
        {
            IWebDriver driver = new ChromeDriver(@"C:\Users\P2SI\Documents\Visual Studio 2015\Projects\SampleSelenium\packages\Selenium.WebDriver.ChromeDriver.2.28.0\driver");
            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl("http://www.yahoomail.com");
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 6));

            wait.Until(ExpectedConditions.ElementExists(By.Id("login-signin")));
            driver.FindElement(By.Name("username")).SendKeys("somebody");
            driver.FindElement(By.Id("login-signin")).Click();

            Thread.Sleep(2000);
            wait.Until(ExpectedConditions.ElementExists(By.Name("passwd")));
            driver.FindElement(By.Name("passwd")).SendKeys("password");
            driver.FindElement(By.ClassName("mbr-login-submit")).Click();

            wait.Until(ExpectedConditions.ElementExists(By.ClassName("search-buttons")));

            // XPath as  //*[contains(concat(' ',normalize-space(@class),' '),' country ') and contains(concat(' ',normalize-space(@class),' '),' name ')]
            //IList<IWebElement> rows = driver.FindElements(By.XPath("//*[@class='list-view-item-container ml-bg   tcLabel-t']")); //fixed start

            // CssSelector search for word begin with use ^
            // to search for a word ending with use $
            IList<IWebElement> rows = driver.FindElements(By.CssSelector("*[class^='list-view-item-container']"));

            int cnt = rows.Count;

            //for (int i = 0; i < cnt; i++)
            foreach (IWebElement row in rows)
            {
                //IWebElement froms = rows[i].FindElement(By.XPath("//*[@class='name first']"));
                IWebElement nameField = row.FindElement(By.ClassName("name-list"));
                string from = nameField.Text;
                if (from.Contains("White House"))
                {
                    IWebElement chkBox = row.FindElement(By.TagName("input"));
                    chkBox.Click();
                    bool chk_Selected = chkBox.Selected;
                }
            }

            // Delete
            IWebElement btnDelete = driver.FindElement(By.Id("btn-delete"));
            if (btnDelete.Enabled)
            {
                btnDelete.Click();
                driver.Navigate().Refresh();
            }

            // Signout
            IWebElement homeName = driver.FindElement(By.XPath("//td[@class='W(1%)']//ul//li//a//i"));
            IWebElement profileName = driver.FindElement(By.XPath("//td[@class='W(1%)']//ul//li[2]//a//i"));
            profileName.Click();

            // id yucs-signout
            driver.FindElement(By.Id("yucs-signout")).Click();

            driver.SwitchTo();
        }

        [TestMethod]
        public void FaceBookWithFireFox()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.Navigate().GoToUrl("http://facebook.com");

            driver.FindElement(By.Id("email")).SendKeys("someone@somewhere.com");
            driver.FindElement(By.Id("pass")).SendKeys("password" + Keys.Enter);

            wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("_1frb")));
            driver.FindElement(By.XPath("//div[@class='homeSideNav']/ul/li/a")).Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("*[class$=data-tab-key='friends']")));
            driver.FindElement(By.CssSelector("*[class$=data-tab-key='friends']")).Click();

            Thread.Sleep(5000);
            IList<IWebElement> friends = driver.FindElements(By.XPath("//div[@Class='fsl fwb fcb']/a"));

            int cnt = friends.Count;

            for (int i = 0; i < cnt; i++)
            {
                Console.WriteLine("Friend " + i + " : " + friends[i].Text);
                if (friends[i].Text.Contains("Palani"))
                {
                    friends[i].Click(); break;
                }
            }

        }

    }
}
