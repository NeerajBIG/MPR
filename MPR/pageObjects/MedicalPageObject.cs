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
    public class MedicalPageObject
    {
        private IWebDriver driver;

        string waiveMedicalCoveragexPath = "//td[text()[contains(.,'WAIVE')]]";
        public string getWAIVEmedicalCoveragexPath()
        {
            return waiveMedicalCoveragexPath;
        }

        string premierMedicalCoveragexPath = "//td[text()[contains(.,'PREMIER')]]";
        public string getPREMIERmedicalCoveragexPath()
        {
            return premierMedicalCoveragexPath;
        }

        string selectMedicalCoveragexPath = "//td[text()[contains(.,'SELECT')]]";
        public string getSELECTmedicalCoveragexPath()
        {
            return selectMedicalCoveragexPath;
        }

        string valueMedicalCoveragexPath = "//td[text()[contains(.,'VALUE')]]";
        public string getVALUEmedicalCoveragexPath()
        {
            return valueMedicalCoveragexPath;
        }

        string protectMedicalCoveragexPath = "//td[text()[contains(.,'PROTECT')]]";
        public string getPROTECTmedicalCoveragexPath()
        {
            return protectMedicalCoveragexPath;
        }

        string choiceMedicalCoveragexPath = "//td[text()[contains(.,'CHOICE')]]";
        public string getCHOICEmedicalCoveragexPath()
        {
            return choiceMedicalCoveragexPath;
        }

        string kaiserMedicalCoveragexPath = "//td[text()[contains(.,'KAISER')]]";
        public string getKAISERmedicalCoveragexPath()
        {
            return kaiserMedicalCoveragexPath;
        }

        string permanenteMedicalCoveragexPath = "//td[text()[contains(.,'PERMANENTE')]]";
        public string getPERMANENTEmedicalCoveragexPath()
        {
            return permanenteMedicalCoveragexPath;
        }

        public MedicalPageObject(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//span[text()='Medical']")]
        private IWebElement clkMedical;
        public IWebElement getclkMedical()
        { 
            return clkMedical;
        }

        [FindsBy(How = How.XPath, Using = "//span[text()='Medical']")]
        private IWebElement headingText;
        public IWebElement getheadingText()
        {
            return headingText;
        }

        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Compare Plans')]")]
        private IWebElement comparePlanlink;
        public IWebElement getcomparePlanlink()
        {
            return comparePlanlink;
        }

        [FindsBy(How = How.XPath, Using = "//h1[@class='page-header']")]
        private IWebElement popUpHeading;
        public IWebElement getpopUpHeading()
        {
            return popUpHeading;
        }

        [FindsBy(How = How.XPath, Using = "//select[@id='ctl00_ContentPlaceHolderMain_ddlPlan']/option[@selected]")]
        private IWebElement planTypeSelection;
        public IWebElement getplanTypeSelection()
        {
            return planTypeSelection;
        }

        [FindsBy(How = How.XPath, Using = "//input[@id='ctl00_ContentPlaceHolderMain_txtZipCode_txtValidatedTextBox']")]
        private IWebElement zipCode;
        public IWebElement getzipCode()
        {
            return zipCode;
        }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Deseret Premier')]")]
        private IWebElement txtDeseretPremierPopUp;
        public IWebElement gettxtDeseretPremierPopUp()
        {
            return txtDeseretPremierPopUp;
        }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Deseret Value')]")]
        private IWebElement txtDeseretValuePopUp;
        public IWebElement gettxtDeseretValuePopUp()
        {
            return txtDeseretValuePopUp;
        }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Deseret Select')]")]
        private IWebElement txtDeseretSelectPopUp;
        public IWebElement gettxtDeseretSelectPopUp()
        {
            return txtDeseretSelectPopUp;
        }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Deseret Protect')]")]
        private IWebElement txtDeseretProtectPopUp;
        public IWebElement gettxtDeseretProtectPopUp()
        {
            return txtDeseretProtectPopUp;
        }


        [FindsBy(How = How.XPath, Using = "//td[text()[contains(.,'WAIVE')]]")]
        public IWebElement txtwaiveMedicalCoverage;
        public IWebElement gettxtWaiveMedicalCoverage()
        {
            return txtwaiveMedicalCoverage;
        }


        [FindsBy(How = How.XPath, Using = "//td[text()[contains(.,'PREMIER')]]")]
        private IWebElement txtDeseretPremier;
        public IWebElement gettxtDeseretPremier()
        {
            return txtDeseretPremier;
        }

        [FindsBy(How = How.XPath, Using = "//td[text()[contains(.,'SELECT')]]")]
        private IWebElement txtDeseretSelect;
        public IWebElement gettxtDeseretSelect()
        {
            return txtDeseretSelect;
        }

        [FindsBy(How = How.XPath, Using = "//td[text()[contains(.,'VALUE')]]")]
        private IWebElement txtDeseretValue;
        public IWebElement gettxtDeseretValue()
        {
            return txtDeseretValue;
        }

        [FindsBy(How = How.XPath, Using = "//td[text()[contains(.,'PROTECT')]]")]
        private IWebElement txtDeseretProtect;
        public IWebElement gettxtDeseretProtect()
        {
            return txtDeseretProtect;
        }

        // poitions of plans will mess this up possibly. I think I should use id's after all.
        [FindsBy(How = How.XPath, Using = "//td[@id='tdHPPlanPremium']/div/span[1]")]
        private IWebElement txtDeseretPremierRate;
        public IWebElement gettxtDeseretPremierRate()
        {
            return txtDeseretPremierRate;

        }
        

        [FindsBy(How = How.XPath, Using = "//td[@id='tdDSPlanPremium']/div/span[1]")]
        private IWebElement txtDeseretSelectRate;
        public IWebElement gettxtDeseretSelectRate()
        {
            return txtDeseretSelectRate;

        }

        [FindsBy(How = How.XPath, Using = "//td[@id='tdLPPlanPremium']/div/span[1]")]
        private IWebElement txtDeseretValueRate;
        public IWebElement gettxtDeseretValueRate()
        {
            return txtDeseretValueRate;

        }

        [FindsBy(How = How.XPath, Using = "//td[@id='tdP1PlanPremium']/div/span[1]")]
        private IWebElement txtDeseretProtectRate;
        public IWebElement gettxtDeseretProtectRate()
        {
            return txtDeseretProtectRate;

        }

        // poitions of plans will mess this up possibly. I think I should use id's after all.
        [FindsBy(How = How.XPath, Using = "//td[@id='tdHMPlanPremium']/div/span[1]")]
        private IWebElement txtDeseretChoiceRate;
        public IWebElement gettxtDeseretChoiceRate()
        {
            return txtDeseretChoiceRate;

        }

        [FindsBy(How = How.XPath, Using = "//td[@id='tdKHPlanPremium']/div/span[1]")]
        private IWebElement txtDeseretKaiserRate;
        public IWebElement gettxtDeseretKaiserRate()
        {
            return txtDeseretKaiserRate;

        }

        [FindsBy(How = How.XPath, Using = "//td[@id='tdKSPlanPremium']/div/span[1]")]
        private IWebElement txtDeseretPermanenteRate;
        public IWebElement gettxtDeseretPermanenteRate()
        {
            return txtDeseretPermanenteRate;

        }

        [FindsBy(How = How.XPath, Using = "//a[@id='readMoreButton']")]
        private IWebElement readMoreBtn;
        public IWebElement getreadMoreBtn()
        {
            return readMoreBtn;
        }

        [FindsBy(How = How.XPath, Using = "//input[@id='isInputCollapsed']")]
        private IWebElement readMoreBtnCollapseInput;
        public IWebElement getreadMoreBtnCollapseInput()
        {
            return readMoreBtnCollapseInput;

        }


    }
    public class MyReflectionClass
    {
        public string MyMethod()
        {
            return DateTime.Now.ToString();
        }
    }
}
