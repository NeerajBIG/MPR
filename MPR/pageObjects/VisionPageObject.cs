using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using static System.Net.Mime.MediaTypeNames;

namespace MPR.pageObjects
{
    // -------------------------------------------------------------------------------
    /// <summary>
    /// This is the description of the method.
    /// </summary>
    /// <param name="firstParameter">This is the first parameter.</param>
    /// <returns>This is the description of the return value.</returns>
    // -------------------------------------------------------------------------------
    public class VisionPageObject
    {
        private IWebDriver driver;

        // -------------- Label Paths --------------------------

        string waiveMedicalCoveragexPath = "//td[text()[contains(.,'WAIVE')]]";
        public string getWAIVECoveragexPath()
        {
            return waiveMedicalCoveragexPath;
        }

        string withoutCoveragexPath = "//td[@id='tdVISIONPlanTitle']";
        public string getWITHOUTCoveragexPath()
        {
            return withoutCoveragexPath;
        }

        string withCoveragexPath = "//td[@id='tdVISIONEEPlanTitle']";
        public string getWITHCoveragexPath()
        {
            return withCoveragexPath;
        }


        // ---------------------- Rate Paths --------------------------------

        string withoutCoverageRatexPath = "//td[@id='tdVISIONPlanPremium']/div/span[1]";
        public string getWITHOUTCoverageRatexPath()
        {
            return withoutCoverageRatexPath;
        }

        string withCoverageRatexPath = "//td[@id='tdVISIONEEPlanPremium']/div/span[1]";
        public string getWITHCoverageRatexPath()
        {
            return withCoverageRatexPath;
        }


        public VisionPageObject(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//span[text()='Vision']")]
        private IWebElement clkDental;
        public IWebElement getclkVision()
        { 
            return clkDental;
        }

        [FindsBy(How = How.XPath, Using = "//span[text()='Vision (VSP)']")]
        private IWebElement headingText;
        public IWebElement getheadingText()
        {
            return headingText;
        }

    }
    public class MyVisionReflectionClass
    {
        public string MyMethod()
        {
            return DateTime.Now.ToString();
        }
    }
}
