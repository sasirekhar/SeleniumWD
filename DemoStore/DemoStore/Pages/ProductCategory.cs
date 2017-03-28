using DemoStore.GeneralLib;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DemoStore.Pages
{
    class ProductCategory
    {
        #region Object Repository
        private By UI_GridIcon
        {
            get
            {
                return By.Name("Grid");
            }
        }

        private By UI_ListIcon
        {
            get
            {
                return By.Name("Grid");
            }
        }

        private By UI_Products
        {
            get
            {
                // CssSelector search for word begin with use ^
                // to search for a word ending with use $
                return By.CssSelector("*[class^='default_product_display product_view_']"); 
            }
        }
        private By UI_ProductTitle
        {
            get
            {
                return By.ClassName("prodtitle");
            }
        }
        private By UI_ProductAddToCart
        {
            get
            {
                return By.Name("Buy");
            }
        }
        #endregion



        GlobalLib globalLib = new GlobalLib();



        #region Functions
        public void AccessItemFromGrid(string productName)
        {
            try
            {
                //GlobalLib.driverWait.Until(ExpectedConditions.ElementIsVisible(UI_GridIcon));
                IList<IWebElement> products = GlobalLib.driver.FindElements(UI_Products);
                foreach (IWebElement product in products)
                {
                    IWebElement productTitle = product.FindElement(UI_ProductTitle);
                    if (productTitle.Text.ToLower().Contains(productName.ToLower()))
                    {
                        product.FindElement(UI_ProductAddToCart).Click();
                        Thread.Sleep(2000);
                        GlobalLib.driverWait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("continue_shopping")));
                        GlobalLib.driver.FindElement(By.ClassName("continue_shopping")).Click();
                        Thread.Sleep(2000);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        #endregion
    }
}
