using AutomationFramework.pageObjects.ReportPortal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium.DevTools.V120.Page;

namespace AutomationFramework.Utilities
{
    public class Base
    {

        public ExtentReports extent;
        public ExtentTest test;


        public IWebDriver _driver;

        // Report file
        [OneTimeSetUp]
        public void Setup()
        {
            string workDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workDirectory).Parent.Parent.FullName;
            string reportPath = projectDirectory + "//index.html";
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            extent = new();
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Environment", ConfigurationManager.AppSettings["environment"]);
            extent.AddSystemInfo("username", ConfigurationManager.AppSettings["username"]);

        }

        public IWebDriver getDriver() { return _driver; }

        public static jsonReader getDataParser()
        {
            return new jsonReader();
        }

        [SetUp]
        public void StartBrowser() {

            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

           string browserName = ConfigurationManager.AppSettings["browser"];
            string webPage = ConfigurationManager.AppSettings["url"];
            SelectBrowser(browserName);

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            _driver.Manage().Window.Maximize();

            _driver.Url = webPage;

        }      

        public void SelectBrowser(string browserName)
        {
            switch (browserName)
            {
                case "Chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    _driver = new ChromeDriver();
                    break;

                case "Firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    _driver = new FirefoxDriver();
                    break;

                case "Edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    _driver = new EdgeDriver();
                    break;

            }
        }

        [TearDown]
        public void StopBrowser()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;

            DateTime time = DateTime.Now;
            string screenshotName = "Screenshot_" + time.ToString("h_mm_ss") + ".png";

            if(status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                test.Fail("TEST FAILED, screenshot attached ", CaptureScreenshot(_driver, screenshotName));
                test.Log(Status.Fail, "Test FAILED, Stacktrace:\n" + stackTrace);
            }
            else if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
            {
                test.Pass("TEST PASS");
            }

            extent.Flush();
            _driver.Quit();
        }

        public MediaEntityModelProvider CaptureScreenshot(IWebDriver _driver, string screenshotName)
        {
            ITakesScreenshot screenshotMode = (ITakesScreenshot)_driver;
            var screenshot = screenshotMode.GetScreenshot().AsBase64EncodedString;

           return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenshotName).Build();
        }

    }
}
