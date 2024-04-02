using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using System.Configuration;
using MPR.utilities;
using AventStack.ExtentReports.Reporter;
using System.Security.Cryptography.X509Certificates;
using AventStack.ExtentReports;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium.DevTools.V120.Page;
using AventStack.ExtentReports.Listener.Entity;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using OpenQA.Selenium.Internal.Logging;
using AventStack.ExtentReports.Model;


namespace MPR.utilities
{
    public class Base
    {
        public IWebDriver driver;

        public ExtentReports extent;
        public ExtentTest testExtent;

        [OneTimeSetUp]
        public void SetupReport()
        {
            DateTime time = DateTime.Now;
            String reportName = TestContext.CurrentContext.Test.Name + "_" + time.ToString("MM_dd_yyyy_h_mm_ss")+".html";

            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            var reportPath = projectDirectory +"//Reports//"+reportName;
            extent = new ExtentReports();
            var htmlReporter = new ExtentSparkReporter(reportPath);
            extent.AttachReporter(htmlReporter);
            string projectDirectoryName = Directory.GetParent(workingDirectory).Parent.Parent.Name;
            extent.AddSystemInfo("Project Name", "DMBA-"+ projectDirectoryName);
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("Tester Name", "Neeraj");
            
            TestContext.Progress.WriteLine("########################## Initiating the Extent Report");

        }

        [SetUp]
        public void Setup()
        {            
            string browserName = ConfigurationManager.AppSettings["browser"];
            InitBrowser(browserName);

            string url1 = getDataParser().extractData("DemoUrl");
            driver.Url = url1;
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            testExtent = extent.CreateTest(TestContext.CurrentContext.Test.ClassName + " - " + TestContext.CurrentContext.Test.MethodName);
        }

        public IWebDriver getDriver()
        {
            return driver;
        }

        public void InitBrowser(string browserName)
        {
            switch (browserName) 
            {
                case "Firefox":
                    driver = new FirefoxDriver();
                    break;

                case "FirefoxHeadless":
                    FirefoxOptions optionFirefox = new FirefoxOptions();
                    optionFirefox.AddArgument("--headless");
                    driver = new FirefoxDriver(optionFirefox);
                    break;

                case "Chrome":
                    driver = new ChromeDriver();
                    break;

                case "ChromeHeadless":
                    ChromeOptions optionChrome = new ChromeOptions();
                    optionChrome.AddArgument("--headless");
                    driver = new ChromeDriver(optionChrome);
                    break;

                case "Edge":
                    driver = new EdgeDriver();
                    break;

                case "EdgeHeadless":
                    EdgeOptions optionEdge = new EdgeOptions();
                    optionEdge.AddArgument("--headless");
                    driver = new EdgeDriver(optionEdge);
                    break;
            }
        }

        public static JsonReader getDataParser()
        {
            return new JsonReader();
        }

        [TearDown]
        public void CloseBrowser()
        {
            string testCaseName = TestContext.CurrentContext.Test.Name;

            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var errorMessage = TestContext.CurrentContext.Result.Message;

            DateTime time = DateTime.Now;
            String fileName = testCaseName + "_" + time.ToString("MM_dd_yyyy_h_mm_ss") + ".png";

            if (status == TestStatus.Failed)
            {
                Screenshot file = ((ITakesScreenshot)driver).GetScreenshot();
                string workingDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
                file.SaveAsFile(projectDirectory + "\\Screenshots\\" + fileName);

                testExtent.Log(Status.Fail, "Test failed with error message : " + errorMessage);
                testExtent.Info(fileName, MediaEntityBuilder.CreateScreenCaptureFromPath(projectDirectory + "\\Screenshots\\" + fileName).Build());

            }
            else if (status == TestStatus.Passed)
            {

            }
            driver.Close();
            TestContext.Progress.WriteLine("########################## Closing the driver");

        }

        [OneTimeTearDown]
        public void FlushExtentReport()
        {
            
            extent.Flush();
            TestContext.Progress.WriteLine("########################## Flushing the Extent Report");

            string clearDataBeforeDays = ConfigurationManager.AppSettings["clearDataBeforeDays"];
            int clearDataBeforeDaysInt = Int32.Parse(clearDataBeforeDays);

            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string reportsDirectory = projectDirectory + "\\Reports";
            string screenshotsDirectory = projectDirectory + "\\Screenshots";

            var fileCountReports = (from file in Directory.EnumerateFiles(reportsDirectory, "*", SearchOption.AllDirectories)
                             select file).Count();
            var fileCountScreenshots = (from file in Directory.EnumerateFiles(screenshotsDirectory, "*", SearchOption.AllDirectories)
                                    select file).Count();                      

            DirectoryInfo dReports = new DirectoryInfo(reportsDirectory);
            FileInfo[] reportFiles = dReports.GetFiles("*");
            foreach (FileInfo file in reportFiles)
            {
                string fileToDelete = file.Name;
                string[] fileDateParts = fileToDelete.Split("_");


                string fileDate = fileDateParts[2];
                int fileDateInt = Int32.Parse(fileDate);

                string fileMonth = fileDateParts[1];
                int fileMonthInt = Int32.Parse(fileMonth);

                string fileYear = fileDateParts[3];
                int fileYearInt = Int32.Parse(fileYear);

                DateTime fileTimestamp = new DateTime(fileYearInt, fileMonthInt, fileDateInt);
                DateTime dateTimeNow = DateTime.Now;
                TimeSpan days = dateTimeNow - fileTimestamp;

                if (days.Days >= clearDataBeforeDaysInt)
                {
                    File.Delete(reportsDirectory + "\\" + file.Name);
                }

            }


            DirectoryInfo dScreenshots = new DirectoryInfo(screenshotsDirectory);
            FileInfo[] screenshotsFiles = dScreenshots.GetFiles("*");
            foreach (FileInfo file in screenshotsFiles)
            {
                string fileToDelete = file.Name;
                string[] fileDateParts = fileToDelete.Split("_");


                string fileDate = fileDateParts[2];
                int fileDateInt = Int32.Parse(fileDate);

                string fileMonth = fileDateParts[1];
                int fileMonthInt = Int32.Parse(fileMonth);

                string fileYear = fileDateParts[3];
                int fileYearInt = Int32.Parse(fileYear);

                DateTime fileTimestamp = new DateTime(fileYearInt, fileMonthInt, fileDateInt);
                DateTime dateTimeNow = DateTime.Now;
                TimeSpan days = dateTimeNow - fileTimestamp;

                if (days.Days >= clearDataBeforeDaysInt)
                {
                    File.Delete(screenshotsDirectory + "\\" + file.Name);
                }

            }

        }

    }
}
