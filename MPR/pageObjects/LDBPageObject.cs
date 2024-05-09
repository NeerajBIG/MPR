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
    public class LDBPageObject
    {
        private IWebDriver driver;

        public LDBPageObject(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//div[contains(@onclick,'GroupTermLife') and contains(.,'Disability')]")]
        private IWebElement clkLDB;
        public IWebElement getclkLDB()
        { 
            return clkLDB;
        }

        [FindsBy(How = How.XPath, Using = "//span[text()='Life and Disability Benefits']")]
        private IWebElement headingText;
        public IWebElement getheadingText()
        {
            return headingText;
        }

        [FindsBy(How = How.XPath, Using = "//span[text()='Beneficiaries for Group Term Life']")]
        private IWebElement secondPageHeadingText;
        public IWebElement getSecondPageHeadingText()
        {
            return secondPageHeadingText;
        }


        // Hyper link related
        [FindsBy(How = How.XPath, Using = "//title")]
        private IWebElement verifyPDFTitle;
        public IWebElement getVerifyPDFTitle()
        {
            return verifyPDFTitle;
        }

        // Hyper links
        [FindsBy(How = How.XPath, Using = "//a[text()='Where can I find more information?']")]
        private IList<IWebElement> hyperlnks;
        public IList<IWebElement> getHyperlnks()
        {
            return hyperlnks;
        }

        // estimated monthly benefit
        [FindsBy(How = How.XPath, Using = "//*[@id='frmGroupTermLife']/p[1]/span")]
        private IWebElement disabilityPlanText;
        public IWebElement getDisabilityPlanText()
        {
            return disabilityPlanText;
        }

        // divide equally radio button
        [FindsBy(How = How.XPath, Using = "//span[text()[contains(.,'Divide Equally')]]//input")]
        private IWebElement rblDivideEqually;
        public IWebElement getRblDivideEqually()
        {
            return rblDivideEqually;
        }

        // Primary Beneficiaries Link
        [FindsBy(How = How.XPath, Using = "//a[text()='Designate Primary']")]
        private IWebElement primaryBeneficiariesBtn;
        public IWebElement getPrimaryBeneficiariesBtn()
        {
            return primaryBeneficiariesBtn;
        }

        [FindsBy(How = How.XPath, Using = "//a[text()='Designate beneficiary.']")]
        private IWebElement designatePrimaryBeneficiariesLnk;
        public IWebElement getDesignatePrimaryBeneficiariesLnk()
        {
            return designatePrimaryBeneficiariesLnk;
        }

        [FindsBy(How = How.XPath, Using = "//a[text()='Designate a trust or charitable organization.']")]
        private IWebElement designatePrimaryBeneficiariesAltLnk;
        public IWebElement getDesignatePrimaryBeneficiariesAltLnk()
        {
            return designatePrimaryBeneficiariesAltLnk;
        }

        [FindsBy(How = How.XPath, Using = "//input[@name='ContactName']")]
        private IWebElement txtFullName;
        public IWebElement getTxtFullName()
        {
            return txtFullName;
        }

        [FindsBy(How = How.XPath, Using = "//select[@name='Relationship.SelectedValue']")]
        private IWebElement ddlRelationship;
        public IWebElement getDdlRelationship()
        {
            return ddlRelationship;
        }

        [FindsBy(How = How.XPath, Using = "//label[text()[contains(.,'Male')]]//input")]
        private IWebElement rblMale;
        public IWebElement getRblMale()
        {
            return rblMale;
        }

        [FindsBy(How = How.XPath, Using = "//label[text()[contains(.,'Female')]]//input")]
        private IWebElement rblFemale;
        public IWebElement getRblFemalee()
        {
            return rblFemale;
        }

        [FindsBy(How = How.XPath, Using = "//input[@name='BirthDate']")]
        private IWebElement txtBirthDate;
        public IWebElement getTxtBirthDate()
        {
            return txtBirthDate;
        }

        [FindsBy(How = How.XPath, Using = "//input[@name='SSN']")]
        private IWebElement txtSSN;
        public IWebElement getTxtSSN()
        {
            return txtSSN;
        }

        [FindsBy(How = How.XPath, Using = "//input[@name='Phone']")]
        private IWebElement txtPhoneNumber;
        public IWebElement getPhoneNumber()
        {
            return txtPhoneNumber;
        }





        // Alternate Beneficiaries link
        [FindsBy(How = How.XPath, Using = "//a[text()='Designate Alternate']")]
        private IWebElement alternateBeneficiariesBtn;
        public IWebElement getAlternateBeneficiariesBtn()
        {
            return alternateBeneficiariesBtn;
        }




    }
    public class MyLDBReflectionClass
    {
        public string MyMethod()
        {
            return DateTime.Now.ToString();
        }
    }
}
