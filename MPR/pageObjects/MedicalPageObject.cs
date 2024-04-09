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
        private IWebElement txtwaiveMedicalCoverage;
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

        [FindsBy(How = How.XPath, Using = "//td[text()[contains(.,'CHOICE')]]")]
        private IWebElement txtChoiceHawaii;
        public IWebElement gettxtChoiceHawaii()
        {
            return txtChoiceHawaii;
        }

        [FindsBy(How = How.XPath, Using = "//td[text()[contains(.,'KAISER')]]")]
        private IWebElement txtKaiserHawaii;
        public IWebElement gettxtKaiserHawaii()
        {
            return txtKaiserHawaii;
        }

        [FindsBy(How = How.XPath, Using = "//td[text()[contains(.,'PERMANENTE')]]")]
        private IWebElement txtDeseretPermanente;
        public IWebElement gettxtDeseretPermanente()
        {
            return txtDeseretPermanente;
        }

        // poitions of plans will mess this up possibly. I think I should use id's after all.
        [FindsBy(How = How.XPath, Using = "//td[text()[contains(.,'PREMIER')]]/parent::tr/parent::tbody/tr[4]/td[1]/div/span[1]")]
        private IWebElement txtDeseretPremierRate;
        public IWebElement gettxtDeseretPremierRate()
        {
            return txtDeseretPremierRate;

        }

        [FindsBy(How = How.XPath, Using = "//td[text()[contains(.,'SELECT')]]/parent::tr/parent::tbody/tr[4]/td[2]/div/span[1]")]
        private IWebElement txtDeseretSelectRate;
        public IWebElement gettxtDeseretSelectRate()
        {
            return txtDeseretSelectRate;

        }

        [FindsBy(How = How.XPath, Using = "//td[text()[contains(.,'VALUE')]]/parent::tr/parent::tbody/tr[4]/td[3]/div/span[1]")]
        private IWebElement txtDeseretValueRate;
        public IWebElement gettxtDeseretValueRate()
        {
            return txtDeseretValueRate;

        }

        [FindsBy(How = How.XPath, Using = "//td[text()[contains(.,'PROTECT')]]/parent::tr/parent::tbody/tr[4]/td[4]/div/span[1]")]
        private IWebElement txtDeseretProtectRate;
        public IWebElement gettxtDeseretProtectRate()
        {
            return txtDeseretProtectRate;

        }


    }
}
