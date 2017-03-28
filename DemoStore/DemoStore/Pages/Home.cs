using DemoStore.GeneralLib;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DemoStore.Pages
{
    class Home
    {

        #region Object Repository
        private By UI_MenuItem
        {
            get
            {
                return By.XPath(".//*[@id='menu-main-menu']/li/a");
            }
        }

        private By UI_SubMenuItem
        {
            get
            {
                return By.XPath(".//*[@class='sub-menu']/li/a");
            }
        }
        private By UI_Cart
        {
            get
            {
                return By.XPath(".//div[@id='header_cart']/a");
            }
        }
        private By UI_MyAccount
        {
            get
            {
                return By.XPath(".//div[@id='account']/a");
            }
        }

        #endregion


        GlobalLib globalLib = new GlobalLib();

        #region Functions
        public void AccessMyAccount()
        {
            try
            {
                GlobalLib.driverWait.Until(ExpectedConditions.ElementToBeClickable(UI_MyAccount));
                GlobalLib.driver.FindElement(UI_MyAccount).Click();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void AccessMenuItems(string menuItem, string subMenuItem)
        {
            try
            {
                GlobalLib.driverWait.Until(ExpectedConditions.ElementIsVisible(UI_MenuItem));
                IList<IWebElement> menuItems = GlobalLib.driver.FindElements(UI_MenuItem);
                foreach (IWebElement mItem in menuItems)
                {
                    if (mItem.Text.Contains(menuItem))
                    {
                        mItem.Click();
                        if (subMenuItem != "" && (subMenuItem.Contains("Product") || subMenuItem.Contains("Category")))
                        {
                            Actions builder = new Actions(GlobalLib.driver);
                            builder.MoveToElement(mItem).Perform();
                            Thread.Sleep(1000);
                            //GlobalLib.driverWait.Until(ExpectedConditions.ElementToBeClickable(UI_SubMenuItem));
                            IList<IWebElement> subMenuItems = mItem.FindElements(UI_SubMenuItem);
                            foreach (IWebElement subMItm in subMenuItems)
                            {
                                if (subMItm.Text.Contains(subMenuItem))
                                {
                                    subMItm.Click();
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void AccessCart()
        {
            try
            {
                GlobalLib.driverWait.Until(ExpectedConditions.ElementToBeClickable(UI_Cart));
                GlobalLib.driver.FindElement(UI_Cart).Click();
                Thread.Sleep(1000);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion
    }
}
