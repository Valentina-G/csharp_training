using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace addressbook_web_tests
{
    [TestFixture]
    public class GroupCreationTest
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        public GroupCreationTest(IWebDriver driver, StringBuilder verificationErrors, string baseURL, bool acceptNextAlert)
        {
            this.driver = driver;
            this.verificationErrors = verificationErrors;
            this.baseURL = baseURL;
            this.acceptNextAlert = acceptNextAlert;
        }

        public GroupCreationTest()
        {
        }

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "http://localhost/addressbook/";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void CreateNewGroupTest()
        {
            GoToHomePage();
            Login("admin", "secret");
            OpenGroupPage();
            InitiateNewGroupCreation();
            FeelGroupForm("Great", "repository", "names");
            SubmitGroupCreation();
            ReturnToTheGroupPage();
            LogOut();
        }

        private void LogOut()
        {
            driver.FindElement(By.LinkText("Logout")).Click();
            driver.FindElement(By.XPath("//html")).Click();
        }

        private void ReturnToTheGroupPage()
        {
            driver.FindElement(By.LinkText("groups")).Click();
            driver.FindElement(By.XPath("//html")).Click();
        }

        private void SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
        }

        private void FeelGroupForm(string name, string header, string footer)
        {
            //Fill group Name field
            driver.FindElement(By.Name("group_name")).Click();
            driver.FindElement(By.Name("group_name")).Clear();
            driver.FindElement(By.Name("group_name")).SendKeys(name);
            //Fill group Header field
            driver.FindElement(By.Name("group_header")).Click();
            driver.FindElement(By.Name("group_header")).Clear();
            driver.FindElement(By.Name("group_header")).SendKeys(header);
            //Fill group Footer field
            driver.FindElement(By.Name("group_footer")).Click();
            driver.FindElement(By.Name("group_footer")).Clear();
            driver.FindElement(By.Name("group_footer")).SendKeys(footer);
        }

        private void InitiateNewGroupCreation()
        {
            //New group creation initiate
            driver.FindElement(By.Name("new")).Click();
        }

        private void OpenGroupPage()
        {
            driver.FindElement(By.Name("searchstring")).Click();
            driver.FindElement(By.LinkText("groups")).Click();
        }

        private void Login(string username, string password)
        {
            driver.FindElement(By.Name("user")).Click();
            driver.FindElement(By.Name("user")).Clear();
            driver.FindElement(By.Name("user")).SendKeys(username);
            driver.FindElement(By.Name("pass")).Clear();
            driver.FindElement(By.Name("pass")).SendKeys(password);
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();
        }
             private void GoToHomePage()
        {
            //open home page
            driver.Navigate().GoToUrl(baseURL);
        }

        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
