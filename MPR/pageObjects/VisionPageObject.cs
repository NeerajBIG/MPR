﻿using System;
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

        // Selection Verification
        string planLabelsxPath = "//td[@class='planTitleCell selectedPlanColor1' or @class='planTitleCell planColor1' or @class='waivePlanCell waiveColor' or @class='waivePlanCell selectedPlanColor1' ]";
        public string getPlanLabelsxPath()
        {
            return planLabelsxPath;
        }

        string planSelectionxPath = "//td[@class='planRadioCell']/input";
        public string getPlanSelectionxPath()
        {
            return planSelectionxPath;
        }

        string planSelectionIndicatorxPath = "//td[@class='selectionIndicator' and contains(text(),'CURRENT SELECTION')]";
        public string getPlanSelectionIndicatorxPath()
        {
            return planSelectionIndicatorxPath;
        }

        string planSelectionIndicatorColorxPath = "//td[@class='raisedRow selectedPlanColor1']";
        public string getPlanSelectionIndicatorColorxPath()
        {
            return planSelectionIndicatorColorxPath;
        }



        // Grid path variable
        string visionGridValuesxPath = "//table[@class='selectionTable']//tbody//tr[+INSERT_TABLE_ROW_INDEX+]/td[+INSERT_MATCHING_PLAN_INDEX+]//td";
        public string getVisionGridValuesxPath()
        {
            return visionGridValuesxPath;
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

        [FindsBy(How = How.XPath, Using = "//li[text()[contains(.,'Make a vision plan choice')]]")]
        private IWebElement noSelectionErrorMessage;
        public IWebElement getnoSelectionErrorMessage()
        {
            return noSelectionErrorMessage;

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
