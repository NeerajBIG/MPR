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
    public class FSAPageObject
    {
        private IWebDriver driver;

        public FSAPageObject(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Flex') and contains(text(),'Spending') and contains(text(),'Accounts')]")]
        private IWebElement clkFSA;
        public IWebElement getclkFSA()
        { 
            return clkFSA;
        }

        [FindsBy(How = How.XPath, Using = "//span[text()='Flexible Spending Account (FSA)']")]
        private IWebElement headingText;
        public IWebElement getheadingText()
        {
            return headingText;
        }

        [FindsBy(How = How.XPath, Using = "//a[text()='Read More']")]
        private IWebElement readMoreBtn;
        public IWebElement getReadMoreBtn()
        {
            return readMoreBtn;
        }

        [FindsBy(How = How.XPath, Using = "//a[text()='Where can I find more information?']")]
        private IWebElement moreInformationLink;
        public IWebElement getmoreInformationLink()
        {
            return moreInformationLink;
        }

        [FindsBy(How = How.XPath, Using = "//pdf-viewer[@id='viewer']")]
        private IWebElement pdfViewer;
        public IWebElement getpdfViewer()
        {
            return pdfViewer;
        }


        [FindsBy(How = How.XPath, Using = "//div[@class='alert alert-danger']//li")]
        private IList<IWebElement> enterValuesErrorAlert;
        public IList<IWebElement> getenterValuesErrorAlert()
        {
            return enterValuesErrorAlert;
        }

        [FindsBy(How = How.XPath, Using = "//input[@id='NewMedicalAmount']")]
        private IWebElement healthCareFSATextBox;
        public IWebElement gethealthCareFSATextBox()
        {
            return healthCareFSATextBox;
        }

        [FindsBy(How = How.XPath, Using = "//input[@id='NewDependentAmount']")]
        private IWebElement dependentCareFSATextBox;
        public IWebElement getdependentCareFSATextBox()
        {
            return dependentCareFSATextBox;
        }


        //----------------------------------- modal verification -------------------------------------------
        // healthcare

        [FindsBy(How = How.XPath, Using = "//div[@id='divSelections']//div[@class='hidden-xs col-md-10']//th[contains(text(),'Healthcare')]//span")]
        private IWebElement healthCareFSAIconLink;
        public IWebElement gethealthCareFSAIconLink()
        {
            return healthCareFSAIconLink;
        }

        [FindsBy(How = How.XPath, Using = "//h4[@id='generic-title' and contains(text(),'Medical and Dental FSA')]")]
        private IWebElement healthCareFSAModalTitle;
        public IWebElement gethealthCareFSAModalTitle()
        {
            return healthCareFSAModalTitle;
        }

        // Dependent
        [FindsBy(How = How.XPath, Using = "//div[@id='divSelections']//div[@class='hidden-xs col-md-10']//th[contains(text(),'Dependent Care')]//span")]
        private IWebElement dependentCareFSAIconLink;
        public IWebElement getdependentCareFSAIconLink()
        {
            return dependentCareFSAIconLink;
        }

        [FindsBy(How = How.XPath, Using = "//h4[@id='generic-title' and contains(text(),'Dependent Care FSA')]")]
        private IWebElement dependentCareFSAModalTitle;
        public IWebElement getdependentCareFSAModalTitle()
        {
            return dependentCareFSAModalTitle;
        }
        // healthcareFSABenefitCard
        [FindsBy(How = How.XPath, Using = "//div[@id='divQuestion']//span[@class='glyphicon glyphicon-question-sign']")]
        private IWebElement healthCareFSABenefitCardIconLink;
        public IWebElement gethealthCareFSABenefitCardIconLink()
        {
            return healthCareFSABenefitCardIconLink;
        }

        [FindsBy(How = How.XPath, Using = "//h4[@id='generic-title' and contains(text(),'Healthcare FSA Benefit Card')]")]
        private IWebElement healthCareFSABenefitCardModalTitle;
        public IWebElement gethealthCareFSABenefitCardModalTitle()
        {
            return healthCareFSABenefitCardModalTitle;
        }

        // Close modal popup xpaths used by all modals
        [FindsBy(How = How.XPath, Using = "//div[@class='modal-footer']//button[@id='closeButton']")]
        private IWebElement modelCancelButton;
        public IWebElement getmodelCancelButton()
        {
            return modelCancelButton;
        }

        [FindsBy(How = How.XPath, Using = "//div[@id='divModalShell']//button[@id='modalXClose']")]
        private IWebElement modelCloseIcon;
        public IWebElement getmodelCloseIcon()
        {
            return modelCloseIcon;
        }

        // FSA Step 2a verification
        [FindsBy(How = How.XPath, Using = "//span[text()='Before proceeding, please review the policies below.']")]
        private IWebElement secondFSAPageVerification;
        public IWebElement getsecondFSAPageVerification()
        {
            return secondFSAPageVerification;
        }

        // FSA Monthly Deduction 
        [FindsBy(How = How.XPath, Using = "//div[@id='medDentMontlyAmt']")]
        private IWebElement healthCareFSAMonthlyDecuction;
        public IWebElement getHealthCareFSAMonthlyDecuction()
        {
            return healthCareFSAMonthlyDecuction;
        }

        [FindsBy(How = How.XPath, Using = "//div[@id='depCareMontlyAmt']")]
        private IWebElement dependentCareFSAMonthlyDecuction;
        public IWebElement getDependentCareFSAMonthlyDecuction()
        {
            return dependentCareFSAMonthlyDecuction;
        }

        // FSA Radio Buttons
        [FindsBy(How = How.XPath, Using = "//input[@id='chooseBennyCard-true']")]
        private IWebElement healthCareFSABenefitCardYesRadioButton;
        public IWebElement getHealthCareFSABenefitCardYesRadioButton()
        {
            return healthCareFSABenefitCardYesRadioButton;
        }

        [FindsBy(How = How.XPath, Using = "//input[@id='chooseBennyCard-false']")]
        private IWebElement healthCareFSABenefitCardNoRadioButton;
        public IWebElement getHealthCareFSABenefitCardNoRadioButton()
        {
            return healthCareFSABenefitCardNoRadioButton;
        }

        // FSA Radio Buttons
        [FindsBy(How = How.XPath, Using = "//input[@id='ConfirmChooseBennyCard-true']")]
        private IWebElement secondPageFSABenefitCardYesRadioButton;
        public IWebElement getSecondPageFSABenefitCardYesRadioButton()
        {
            return secondPageFSABenefitCardYesRadioButton;
        }

        // FSA Radio Buttons
        [FindsBy(How = How.XPath, Using = "//input[@id='ConfirmChooseBennyCard-false']")]
        private IWebElement secondPageFSABenefitCardNoRadioButton;
        public IWebElement getSecondPageFSABenefitCardNoRadioButton()
        {
            return secondPageFSABenefitCardNoRadioButton;
        }




        }
    public class MyFSAReflectionClass
    {
        public string MyMethod()
        {
            return DateTime.Now.ToString();
        }
    }
}
