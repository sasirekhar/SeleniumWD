using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DemoStore.GeneralLib;

namespace DemoStore.Pages
{
    class MyAccount
    {

        #region Object Repository

        private By UI_Txt_Username
        {
            get
            {
                return By.Name("log");
            }
        }

        private By UI_Txt_Password
        {
            get
            {
                return By.Id("pwd");
            }
        }

        private By UI_Btn_Login
        {
            get
            {
                return By.Name("submit");
            }
        }

        private By UI_Lnk_TopNavs
        {
            get
            {
                return By.XPath(".//div[@class='user-profile-links']//a");
            }
        }

        private By UI_Lnk_RightNavs
        {
            get
            {
                return By.XPath(".//div[@id='sidebar-right']//div//aside//ul//li//a");
            }
        }
        private By UI_LogoutMessage
        {
            get
            {
                return By.ClassName("message");
            }
        }

        #endregion


        
        GlobalLib globalLib = new GlobalLib();



        #region Functions

        public void Login(string username, string password)
        {
            try
            {
                GlobalLib.driverWait.Until(ExpectedConditions.ElementIsVisible(UI_Txt_Username));
                GlobalLib.driver.FindElement(UI_Txt_Username).SendKeys(username);
                GlobalLib.driver.FindElement(UI_Txt_Password).SendKeys(password);
                GlobalLib.driver.FindElement(UI_Btn_Login).Submit();

                Thread.Sleep(1000);
                GlobalLib.driverWait.Until(ExpectedConditions.ElementExists(UI_Lnk_TopNavs));
                Assert.AreEqual(true, GlobalLib.driverWait.Until(ExpectedConditions.ElementExists(UI_Lnk_TopNavs)).Displayed);
            }
            catch(Exception e)
            {
                throw new Exception("Unable to Login - " + e.Message);
            }

        }

        /// <summary>
        /// Access Purchase History, Order Details or Download link on My Accounts page
        /// </summary>
        /// <param name="linkName"></param>
        public void AccessLinks(string linkName)
        {
            try
            {
                GlobalLib.driverWait.Until(ExpectedConditions.ElementIsVisible(UI_Lnk_TopNavs));
                IList<IWebElement> links = GlobalLib.driver.FindElements(UI_Lnk_TopNavs);
                foreach (IWebElement link in links)
                {
                    if (link.Text.Contains(linkName))
                    {
                        link.Click();
                        break;
                    }
                }
            }
            catch(Exception e)
            {
                throw new Exception("Unable to Access link - '" + linkName+ "' on My Account page - " + e.Message);
            }
        }

        public void Logout()
        {
            try
            {
                GlobalLib.driverWait.Until(ExpectedConditions.ElementIsVisible(UI_Lnk_RightNavs));
                IList<IWebElement> rightLinks = GlobalLib.driver.FindElements(UI_Lnk_RightNavs);      
                foreach (IWebElement rightLink in rightLinks)
                {
                    if (rightLink.Text.Contains("Log out"))
                    {
                        rightLink.Click();
                        break;
                    }
                }
                Thread.Sleep(2000);
                Assert.AreEqual(true, GlobalLib.driverWait.Until(ExpectedConditions.ElementIsVisible(UI_LogoutMessage)).Displayed);
            }
            catch (Exception e)
            {
                throw new Exception("Unable to Logout - " + e.Message);
            }
        }
        
        #endregion
    }
}
