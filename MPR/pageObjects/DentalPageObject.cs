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
    public class DentalPageObject
    {
        private IWebDriver driver;

        // -------------- Label Paths --------------------------

        string waiveMedicalCoveragexPath = "//td[text()[contains(.,'WAIVE')]]";
        public string getWAIVECoveragexPath()
        {
            return waiveMedicalCoveragexPath;
        }

        string DentalCoveragexPath = "//td[@id='tdDENTDPlanTitle']";
        public string getDENTALCoveragexPath()
        {
            return DentalCoveragexPath;
        }

        string PlusCoveragexPath = "//td[@id='tdDENTHPlanTitle']";
        public string getPLUSCoveragexPath()
        {
            return PlusCoveragexPath;
        }


        // ---------------------- Rate Paths --------------------------------

        string DentalCoverageRatexPath = "//td[@id='tdDENTDPlanPremium']/div/span[1]";
        public string getDENTALCoverageRatexPath()
        {
            return DentalCoverageRatexPath;
        }

        string PlusCoverageRatexPath = "//td[@id='tdDENTHPlanPremium']/div/span[1]";
        public string getPLUSCoverageRatexPath()
        {
            return PlusCoverageRatexPath;
        }

        // Selection Verification
        string planLabelsxPath = "//td[@class='planTitleCell selectedPlanColor1' or @class='planTitleCell planColor1' ]";
        public string getPlanLabelsxPath()
        {
            return planLabelsxPath;
        }

        string planSelectionIndicatorxPath = "//td[@class='selectionIndicator']";
        public string getPlanSelectionIndicatorxPath()
        {
            return planSelectionIndicatorxPath;
        }

        string planSelectionxPath = "//td[@class='planRadioCell']/input";
        public string getPlanSelectionxPath()
        {
            return planSelectionxPath;
        }

        string planSelectionIndicatorColorxPath = "//td[@class='raisedRow selectedPlanColor1' or @class='raisedRow whiteColor']";
        public string getPlanSelectionIndicatorColorxPath()
        {
            return planSelectionIndicatorColorxPath;
        }

        // Grid path variable. INSERT_TABLE_ROW_INDEX -> Row locations of the content on the web page. INSERT_MATCHING_PLAN_INDEX -> matching column index of the plan.
        string dentalGridValuesxPath = "//table[@class='selectionTable']//tbody//tr[+INSERT_TABLE_ROW_INDEX+]/td[+INSERT_MATCHING_PLAN_INDEX+]//td";
        public string getDentalGridValuesxPath()
        {
            return dentalGridValuesxPath;
        }





        public DentalPageObject(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//span[text()='Dental']")]
        private IWebElement clkDental;
        public IWebElement getclkDental()
        { 
            return clkDental;
        }

        [FindsBy(How = How.XPath, Using = "//span[text()='Dental']")]
        private IWebElement headingText;
        public IWebElement getheadingText()
        {
            return headingText;
        }

        [FindsBy(How = How.XPath, Using = "//li[text()[contains(.,'Make a dental plan choice')]]")]
        private IWebElement noSelectionErrorMessage;
        public IWebElement getnoSelectionErrorMessage()
        {
            return noSelectionErrorMessage;

        }



    }
    public class MyDentalReflectionClass
    {
        public string MyMethod()
        {
            return DateTime.Now.ToString();
        }
    }
}
