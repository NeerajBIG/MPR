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
    public class MenuPageObject
    {
        private IWebDriver driver;
        public MenuPageObject(IWebDriver driver) 
        {
            this.driver = driver;
            PageFactory.InitElements(driver,this);
        }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'CONTINUE']")]
        private IWebElement btnContinue;
        public IWebElement getbtnContinue()
        {
            return btnContinue;
        }

        [FindsBy(How = How.XPath, Using = "//span[text() = 'Introduction']")]
        private IWebElement introductionText;
        public IWebElement getintroductionText()
        {
            return introductionText;
        }

        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Next')]")]
        private IWebElement nextBtn;
        public IWebElement getnextBtn()
        {
            return nextBtn;
        }

        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Menu')]")]
        private IWebElement menuBtn;
        public IWebElement getmenuBtn()
        {
            return menuBtn;
        }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Introduction')]/parent::div/parent::div/div[2]/div")]
        private IWebElement startBtn;
        public IWebElement getstartBtn()
        {
            return startBtn;
        }

        [FindsBy(How = How.XPath, Using = "//a[text()='Premium Details']")]
        private IWebElement premiumDetailsLink;
        public IWebElement getPremiumDetailsLink()
        {
            return premiumDetailsLink;
        }
                
        [FindsBy(How = How.XPath, Using = "//table[@id='tblPopoverContent']/tbody/tr")]
        private IList<IWebElement> premiumDetailsPopover;
        public IList<IWebElement> getPremiumDetailsPopoverContents()
        {
            return premiumDetailsPopover;
        }

        string premiumDetailsPopoverxPath = "//table[@id='tblPopoverContent']/tbody/tr";
        public string getpremiumDetailsPopoverxPath()
        {
            return premiumDetailsPopoverxPath;
        }   

        [FindsBy(How = How.XPath, Using = "//table[@id='tblPopoverContent']/tfoot/tr[1]/td[1]")]
        private IWebElement premiumDetailsPopoverFooter1xPath;
        public IWebElement getpremiumDetailsPopoverFooter1xPath()
        {
            return premiumDetailsPopoverFooter1xPath;
        }

        [FindsBy(How = How.XPath, Using = "//table[@id='tblPopoverContent']/tfoot/tr[1]/td[2]")]
        private IWebElement premiumDetailsPopoverFooter2xPath;
        public IWebElement getpremiumDetailsPopoverFooter2xPath()
        {
            return premiumDetailsPopoverFooter2xPath;
        }

        [FindsBy(How = How.XPath, Using = "//div[contains(@style,'#476A7D')]//span[text()]")]
        private IList<IWebElement> completedStepsByBGColor;
        public IList<IWebElement> getCompletedStepsByBGColor()
        {
            return completedStepsByBGColor;
        }

        [FindsBy(How = How.XPath, Using = "//div[contains(@style,'background-color: #A4C786;')]//span")]
        private IList<IWebElement> currentStepsByBGColor;
        public IList<IWebElement> getCurrentStepsByBGColor()
        {
            return currentStepsByBGColor;
        }

        [FindsBy(How = How.XPath, Using = "//div[contains(@onclick,'Dependents') and contains(.,'Manage Dependents')]")]
        private IWebElement clkManageDependents;
        public IWebElement getclkManageDependents()
        {
            return clkManageDependents;
        }




    }
}
