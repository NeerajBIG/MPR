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
    public class MedicalPageObject
    {
        private IWebDriver driver;

        // -------------- Label Paths for Medical Page --------------------------

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

        // ---------------------- Rate Paths For Medical Page --------------------------------

        string premierMedicalCoverageRatexPath = "//td[@id='tdHPPlanPremium']/div/span[1]";
        public string getPREMIERmedicalCoverageRatexPath()
        {
            return premierMedicalCoverageRatexPath;
        }

        string selectMedicalCoverageRatexPath = "//td[@id='tdDSPlanPremium']/div/span[1]";
        public string getSELECTmedicalCoverageRatexPath()
        {
            return selectMedicalCoverageRatexPath;
        }

        string valueMedicalCoverageRatexPath = "//td[@id='tdLPPlanPremium']/div/span[1]";
        public string getVALUEmedicalCoverageRatexPath()
        {
            return valueMedicalCoverageRatexPath;
        }

        string protectMedicalCoverageRatexPath = "//td[@id='tdP1PlanPremium']/div/span[1]";
        public string getPROTECTmedicalCoverageRatexPath()
        {
            return protectMedicalCoverageRatexPath;
        }

        string choiceMedicalCoverageRatexPath = "//td[@id='tdHMPlanPremium']/div/span[1]";
        public string getCHOICEmedicalCoverageRatexPath()
        {
            return choiceMedicalCoverageRatexPath;
        }

        string kaiserMedicalCoverageRatexPath = "//td[@id='tdKHPlanPremium']/div/span[1]";
        public string getKAISERmedicalCoverageRatexPath()
        {
            return kaiserMedicalCoverageRatexPath;
        }

        string permanenteMedicalCoverageRatexPath = "//td[@id='tdKNPlanPremium' or @id='tdKSPlanPremium']/div/span[1]";
        public string getPERMANENTEmedicalCoverageRatexPath()
        {
            return permanenteMedicalCoverageRatexPath;
        }

        // Plan Paths for full comparison link
        string premierComparisonLinkCoveragexPath = "//span[text()[contains(.,'Deseret Premier')] and contains(@id,'ctl00_ContentPlaceHolderMain_planCompare_lvPlanComparison_lblPlan')]";
        public string getPREMIERComparisonLinkCoveragexPath()
        {
            return premierComparisonLinkCoveragexPath;
        }

        string selectComparisonLinkCoveragexPath = "//span[text()[contains(.,'Deseret Select')] and contains(@id,'ctl00_ContentPlaceHolderMain_planCompare_lvPlanComparison_lblPlan')]";
        public string getSELECTComparisonLinkCoveragexPath()
        {
            return selectComparisonLinkCoveragexPath;
        }

        string valueComparisonLinkCoveragexPath = "//span[text()[contains(.,'Deseret Value')] and contains(@id,'ctl00_ContentPlaceHolderMain_planCompare_lvPlanComparison_lblPlan')]";
        public string getVALUEComparisonLinkCoveragexPath()
        {
            return valueComparisonLinkCoveragexPath;
        }

        string protectComparisonLinkCoveragexPath = "//span[text()[contains(.,'Deseret Protect')] and contains(@id,'ctl00_ContentPlaceHolderMain_planCompare_lvPlanComparison_lblPlan')]";
        public string getPROTECTComparisonLinkCoveragexPath()
        {
            return protectComparisonLinkCoveragexPath;
        }

        string choiceComparisonLinkCoveragexPath = "//span[text()[contains(.,'Deseret Choice Hawaii')] and contains(@id,'ctl00_ContentPlaceHolderMain_planCompare_lvPlanComparison_lblPlan')]";
        public string getCHOICEComparisonLinkCoveragexPath()
        {
            return choiceComparisonLinkCoveragexPath;
        }

        string kaiserComparisonLinkCoveragexPath = "//span[text()[contains(.,'Kaiser of Hawaii')] and contains(@id,'ctl00_ContentPlaceHolderMain_planCompare_lvPlanComparison_lblPlan')]";
        public string getKAISERComparisonLinkCoveragexPath()
        {
            return kaiserComparisonLinkCoveragexPath;
        }
        
        string permanenteComparisonLinkCoveragexPath = "//span[text()[contains(.,'Kaiser Permanente')] and contains(@id,'ctl00_ContentPlaceHolderMain_planCompare_lvPlanComparison_lblPlan')]";
        public string getPERMANENTEComparisonLinkCoveragexPath()
        {
            return permanenteComparisonLinkCoveragexPath;
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

        // ------------------------------------------------------ Plan Paths for full comparison link -----------------------------------------------------
        /*
        //  ---------------------------------------------------------- contract premier values ------------------------------------------------------------------
        string premierContractOfficeVisitValuesxPath = "//table[@class='selectionTable']//tbody//tr[8]/td[2]//td[1]";
        public string getPREMIERContractOfficeVisitValuesMedicalCoveragexPath()
        {
            return premierContractOfficeVisitValuesxPath;
        }

        string premierContractAnnualDeductibleValuesxPath = "//table[@class='selectionTable']//tbody//tr[9]/td[2]//td[1]";
        public string getPREMIERContractAnnualDeductibleValuesMedicalCoveragexPath()
        {
            return premierContractAnnualDeductibleValuesxPath;
        }

        string premierContractCoinsuranceValuesxPath = "//table[@class='selectionTable']//tbody//tr[10]/td[2]//td[1]";
        public string getPREMIERContractCoinsuranceValuesMedicalCoveragexPath()
        {
            return premierContractCoinsuranceValuesxPath;
        }

        string premierContractOutOfPocketMaxValuesxPath = "//table[@class='selectionTable']//tbody//tr[11]/td[2]//td[1]";
        public string getPREMIERContractOutOfPocketMaxValuesMedicalCoveragexPath()
        {
            return premierContractOutOfPocketMaxValuesxPath;
        }


        */

        // Grid path variable
        string medicalGridValuesxPath = "//table[@class='selectionTable']//tbody//tr[+INSERT_TABLE_ROW_INDEX+]/td[+INSERT_LABEL_INDEX+]//td[+INSERT_CONTRACT_COLUMN_SELECTION+]";
        public string getMedicalGridValuesxPath()
        {
            return medicalGridValuesxPath;
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

        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'full plan comparison')]")]
        private IWebElement fullPlanComparisonlink;
        public IWebElement getfullPlanComparisonlink()
        {
            return fullPlanComparisonlink;
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

        [FindsBy(How = How.XPath, Using = "//li[text()[contains(.,'Make a medical plan choice')]]")]
        private IWebElement noSelectionErrorMessage;
        public IWebElement getnoSelectionErrorMessage()
        {
            return noSelectionErrorMessage;

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
