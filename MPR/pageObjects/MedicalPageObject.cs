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
            PageFactory.InitElements(driver,this);
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
        private IWebElement txtDeseretPremier;
        public IWebElement gettxtDeseretPremier()
        {
            return txtDeseretPremier;
        }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Deseret Value')]")]
        private IWebElement txtDeseretValue;
        public IWebElement gettxtDeseretValue()
        {
            return txtDeseretValue;
        }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Deseret Select')]")]
        private IWebElement txtDeseretSelect;
        public IWebElement gettxtDeseretSelect()
        {
            return txtDeseretSelect;
        }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Deseret Protect')]")]
        private IWebElement txtDeseretProtect;
        public IWebElement gettxtDeseretProtect()
        {
            return txtDeseretProtect;
        }


    }
}
