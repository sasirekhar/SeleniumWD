using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using DemoStore.Pages;
using DemoStore.GeneralLib;

namespace DemoStore.Scripts
{
    /// <summary>
    /// Summary description for TestCase1
    /// </summary>
    [TestClass]
    public class TestCase1
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        //// Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext) { }

        //// Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        // public static void MyClassCleanup() {}
        //
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
         public void MyTestInitialize()
        {
            GlobalLib.CloseAllBrowsers();
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
         public void MyTestCleanup()
        {
        }

        #endregion

        GlobalLib globalLib = new GlobalLib();
        MyAccount myAccount = new MyAccount();
        Home home = new Home();

        [TestMethod] [Priority(1)]
        public void TestMethod1()
        {
            // Launch Browser and URL
            globalLib.SelectBrowserAndNavigate();
        }


        [TestMethod] [Priority(2)]
        public void TestMethod2()
        {
            ProductCategory productCategory = new ProductCategory();

            home.AccessMenuItems("All Product", "");

            productCategory.AccessItemFromGrid("iPod");

            home.AccessCart();
        }

        [TestMethod][Priority(3)]
        public void TestMethod3()
        {
            home.AccessMyAccount();

            myAccount.Login("srtestuser", "srtestuser1*");

            myAccount.AccessLinks("Your Download");

            myAccount.Logout();
        }

        [TestMethod] [Priority(4)]
        public void TestMethod4()
        {
            GlobalLib.driver.Close();
        }
    }
}
