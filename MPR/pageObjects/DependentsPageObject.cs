using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace MPR.pageObjects
{
    // -------------------------------------------------------------------------------
    /// <summary>
    /// This is the description of the method.
    /// </summary>
    /// <param name="firstParameter">This is the first parameter.</param>
    /// <returns>This is the description of the return value.</returns>
    // -------------------------------------------------------------------------------
    public class DependentsPageObject
    {
        private IWebDriver driver;
        public DependentsPageObject(IWebDriver driver) 
        {
            this.driver = driver;
            PageFactory.InitElements(driver,this);
        }

        [FindsBy(How = How.XPath, Using = "//span[text()='Manage Dependents']")]
        private IWebElement clkDependents;
        public IWebElement getclkDependents()
        {
            return clkDependents;
        }

        [FindsBy(How = How.XPath, Using = "//span[text()[contains(.,'Add Dependents')]]")]
        private IWebElement headingText;
        public IWebElement getheadingText()
        {
            return headingText;
        }

        [FindsBy(How = How.XPath, Using = "//a[text()='Add Dependent']")]
        private IWebElement addDependentBtn;
        public IWebElement getaddDependentBtn()
        {
            return addDependentBtn;
        }

        [FindsBy(How = How.XPath, Using = "//input[@id='FirstName']")]
        private IWebElement firstNameField;
        public IWebElement getfirstNameField()
        {
            return firstNameField;
        }

        [FindsBy(How = How.XPath, Using = "//input[@id='LastName']")]
        private IWebElement lastNameField;
        public IWebElement getlastNameField()
        {
            return lastNameField;
        }

        [FindsBy(How = How.XPath, Using = "//input[@id='SSN']")]
        private IWebElement SSN;
        public IWebElement getSSN()
        {
            return SSN;
        }

        [FindsBy(How = How.XPath, Using = "//input[@id='BirthDate']")]
        private IWebElement birthDate;
        public IWebElement getbirthDate()
        {
            return birthDate;
        }

        [FindsBy(How = How.XPath, Using = "//input[@id='Gender_SelectedValue' and @value='M']")]
        private IWebElement radioBtnMale;
        public IWebElement getradioBtnMale()
        {
            return radioBtnMale;
        }

        [FindsBy(How = How.XPath, Using = "//input[@id='Gender_SelectedValue' and @value='F']")]
        private IWebElement radioBtnFemale;
        public IWebElement getradioBtnFemale()
        {
            return radioBtnFemale;
        }

        [FindsBy(How = How.XPath, Using = "//input[@id='Relationship_SelectedValue']")]
        private IWebElement relationshipDropdown;
        public IWebElement getrelationshipDropdown()
        {
            return relationshipDropdown;
        }

        [FindsBy(How = How.XPath, Using = "//div[@id='divDependentModalShell']/div/div/div[3]/button[2]']")]
        private IWebElement continuePopUpBtn;
        public IWebElement getcontinuePopUpBtn()
        {
            return continuePopUpBtn;
        }

        [FindsBy(How = How.XPath, Using = "//div[@id='divDependentModalShell']/div/div/div[3]/button[2]']")]
        private IWebElement closePopUpBtn;
        public IWebElement getclosePopUpBtn()
        {
            return closePopUpBtn;
        }

        [FindsBy(How = How.XPath, Using = "//div[@id='divDependentModalShell']/div/div/div[3]/a']")]
        private IWebElement backPopUpBtn;
        public IWebElement getbackPopUpBtn()
        {
            return backPopUpBtn;
        }

        [FindsBy(How = How.XPath, Using = "//input[@id='Married_SelectedValue' and @value='Y']")]
        private IWebElement radioBtnMarriedYes;
        public IWebElement getradioBtnMarriedYes()
        {
            return radioBtnMale;
        }

        [FindsBy(How = How.XPath, Using = "//input[@id='Married_SelectedValue' and @value='N']")]
        private IWebElement radioBtnMarriedNo;
        public IWebElement getradioBtnMarriedNo()
        {
            return radioBtnMarriedNo;
        }

        [FindsBy(How = How.XPath, Using = "//input[@id='Covered_SelectedValue' and @value='Y']")]
        private IWebElement radioBtnEnrolledYes;
        public IWebElement getradioBtnEnrolledYes()
        {
            return radioBtnMale;
        }

        [FindsBy(How = How.XPath, Using = "//input[@id='Covered_SelectedValue' and @value='N']")]
        private IWebElement radioBtnEnrolledNo;
        public IWebElement getradioBtnEnrolledNo()
        {
            return radioBtnEnrolledNo;
        }

        [FindsBy(How = How.XPath, Using = "//input[@id='OtherInsurance_SelectedValue' and @value='Y']")]
        private IWebElement radioBtnOtherCoverageYes;
        public IWebElement getradioBtnOtherCoverageYes()
        {
            return radioBtnMale;
        }

        [FindsBy(How = How.XPath, Using = "//input[@id='OtherInsurance_SelectedValue' and @value='N']")]
        private IWebElement radioBtnOtherCoverageNo;
        public IWebElement getradioBtnOtherCoverageNo()
        {
            return radioBtnOtherCoverageNo;
        }

        [FindsBy(How = How.XPath, Using = "//a[text()='Edit']")]
        private IWebElement editBtn;
        public IWebElement geteditBtn()
        {
            return editBtn;
        }

        [FindsBy(How = How.XPath, Using = "//a[text()='Delete']")]
        private IWebElement deleteBtn;
        public IWebElement getdeleteBtn()
        {
            return deleteBtn;
        }
        
        /*
        [FindsByAll(How = How.XPath, Using = "//div[@id='divDependents']/table/tbody/tr")]
        private IWebElement dependentsList;
        public IWebElement getdependentsList()
        {
            return dependentsList;
        }
        */


    }
}
