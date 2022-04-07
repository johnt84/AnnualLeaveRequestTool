using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Threading;

namespace AnnualLeaveRequestBlazorServerAutomationUITest
{
    [TestClass]
    public class AutomatedUITests
    {
        IConfigurationRoot config = null;

        ChromeDriver driver = null;

        [TestMethod]
        public void CreateAnAnnualLeaveRequestSuccess()
        {
            Initialise();

            driver.Navigate().GoToUrl($"{config["BlazorServerAnnualLeaveRequestApp"]}AnnualLeave/Overview");

            int numberOfRowsInTableBeforeCreate = driver.FindElements(By.XPath("//table[@id='tbAnnualLeaveRequests']//tr")).Count;

            driver.Navigate().GoToUrl($"{config["BlazorServerAnnualLeaveRequestApp"]}AnnualLeave/Create");

            Thread.Sleep(4000);

            IWebElement dtStartDate = driver.FindElement(By.Id("dtStartDate"));
            dtStartDate.SendKeys("06042022");

            IWebElement dtReturnDate = driver.FindElement(By.Id("dtReturnDate"));
            dtReturnDate.SendKeys("07042022");

            IWebElement ddlPaidLeaveType = driver.FindElement(By.Id("ddlPaidLeaveType"));
            ddlPaidLeaveType.SendKeys("Paid");

            IWebElement ddlLeaveType = driver.FindElement(By.Id("ddlLeaveType"));
            ddlLeaveType.SendKeys("Annual Leave");

            IWebElement txtNotes = driver.FindElement(By.Id("txtNotes"));
            txtNotes.SendKeys("This is an Automation UI Test called CreateAnAnnualLeaveRequestSuccess");

            IWebElement btnSubmit = driver.FindElement(By.Id("btnSubmit"));
            btnSubmit.Click();

            Thread.Sleep(4000);

            Assert.AreEqual($"{config["BlazorServerAnnualLeaveRequestApp"]}AnnualLeave/Overview", driver.Url);

            int numberOfRowsInTableAfterCreate = driver.FindElements(By.XPath("//table[@id='tbAnnualLeaveRequests']//tr")).Count;

            Assert.AreEqual(numberOfRowsInTableBeforeCreate + 1, numberOfRowsInTableAfterCreate);

            driver.Quit();
        }

        [TestMethod]
        public void DeleteAnAnnualLeaveRequestSuccess()
        {
            Initialise();

            driver.Navigate().GoToUrl($"{config["BlazorServerAnnualLeaveRequestApp"]}AnnualLeave/Overview");

            int numberOfRowsInTableBeforeCreate = driver.FindElements(By.XPath("//table[@id='tbAnnualLeaveRequests']//tr")).Count;

            driver.Navigate().GoToUrl($"{config["BlazorServerAnnualLeaveRequestApp"]}AnnualLeave/Delete");

            Thread.Sleep(4000);

            IWebElement dtStartDate = driver.FindElement(By.Id("dtStartDate"));
            dtStartDate.SendKeys("06042022");

            IWebElement dtReturnDate = driver.FindElement(By.Id("dtReturnDate"));
            dtReturnDate.SendKeys("07042022");

            IWebElement ddlPaidLeaveType = driver.FindElement(By.Id("ddlPaidLeaveType"));
            ddlPaidLeaveType.SendKeys("Paid");

            IWebElement ddlLeaveType = driver.FindElement(By.Id("ddlLeaveType"));
            ddlLeaveType.SendKeys("Annual Leave");

            IWebElement txtNotes = driver.FindElement(By.Id("txtNotes"));
            txtNotes.SendKeys("This is an Automation UI Test called CreateAnAnnualLeaveRequestSuccess");

            IWebElement btnSubmit = driver.FindElement(By.Id("btnSubmit"));
            btnSubmit.Click();

            Thread.Sleep(4000);

            Assert.AreEqual($"{config["BlazorServerAnnualLeaveRequestApp"]}AnnualLeave/Overview", driver.Url);

            int numberOfRowsInTableAfterCreate = driver.FindElements(By.XPath("//table[@id='tbAnnualLeaveRequests']//tr")).Count;

            Assert.AreEqual(numberOfRowsInTableBeforeCreate + 1, numberOfRowsInTableAfterCreate);

            driver.Quit();
        }

        private void Initialise()
        {
            config = new ConfigurationBuilder()
               .SetBasePath($"{Directory.GetCurrentDirectory()}/../../..")
               .AddJsonFile("appsettings.json")
               .Build();

            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }
    }
}