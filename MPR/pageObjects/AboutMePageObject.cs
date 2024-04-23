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
    public class AboutMePageObject
    {
        private IWebDriver driver;
        public AboutMePageObject(IWebDriver driver) 
        {
            this.driver = driver;
            PageFactory.InitElements(driver,this);
        }

        [FindsBy(How = How.XPath, Using = "//span[text()='About Me']")]
        private IWebElement clkAboutMe;
        public IWebElement getclkAboutMe()
        {
            return clkAboutMe;
        }              

        [FindsBy(How = How.XPath, Using = "//form[@id='frmPersonalInfo']")]
        private IWebElement PageType2;
        public IWebElement getPageType2()
        {
            return PageType2;
        }

        [FindsBy(How = How.XPath, Using = "//span[text()='About Me']")]
        private IWebElement headingText;
        public IWebElement getheadingText()
        {
            return headingText;
        }

        [FindsBy(How = How.XPath, Using = "//div[@id='divEnterDobSSN']")]
        private IWebElement PageType1;
        public IWebElement getPageType1()
        {
            return PageType1;
        }

        [FindsBy(How = How.Id, Using = "enteredSSN")]
        private IWebElement ssnTextbox;
        public IWebElement getssnTextbox()
        {
            return ssnTextbox;
        }

        [FindsBy(How = How.Id, Using = "enteredDOB")]
        private IWebElement dobTextbox;
        public IWebElement getdobTextbox()
        {
            return dobTextbox;
        }

        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Submit')]")]
        private IWebElement submitBtn;
        public IWebElement getsubmitBtn()
        {
            return submitBtn;
        }

        [FindsBy(How = How.XPath, Using = "//div[@class='alert alert-info']/p")]
        private IWebElement alertTxt;
        public IWebElement getalertTxt()
        {
            return alertTxt;
        }

        [FindsBy(How = How.XPath, Using = "//h4[text()='Information Provided by Your Employer:']")]
        private IWebElement aboutMeStep2bTxt;
        public IWebElement getaboutMeStep2bTxt()
        {
            return aboutMeStep2bTxt;
        }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Please select if the information entered by your employer is correct.')]")]
        private IWebElement alertStep2bTxt;
        public IWebElement getalertStep2bTxt()
        {
            return alertStep2bTxt;
        }

        [FindsBy(How = How.XPath, Using = "//input[@value='Y']")]
        private IWebElement yesRadioSelection;
        public IWebElement getyesRadioSelection()
        {
            return yesRadioSelection;
        }

        [FindsBy(How = How.XPath, Using = "//input[@value='N']")]
        private IWebElement noRadioSelection;
        public IWebElement getnoRadioSelection()
        {
            return noRadioSelection;
        }

        [FindsBy(How = How.XPath, Using = "//h4[text()='Contact Your Employer']")]
        private IWebElement popupHeaderNoRadioSelection;
        public IWebElement getpopupHeaderNoRadioSelection()
        {
            return popupHeaderNoRadioSelection;
        }

        [FindsBy(How = How.XPath, Using = "//h4[text()='Employee Personal Information']")]
        private IWebElement headerTxtYesRadioSelection;
        public IWebElement getheaderTxtYesRadioSelection()
        {
            return headerTxtYesRadioSelection;
        }

        [FindsBy(How = How.XPath, Using = "//h1[@class='page-header row']/span[2]")]
        private IWebElement stepPosition;
        public IWebElement getstepPosition()
        {
            return stepPosition;
        }

        [FindsBy(How = How.XPath, Using = "//input[@id='ZipCode']")]
        private IWebElement zipCode;
        public IWebElement getzipCode()
        {
            return zipCode;
        }

        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Next')]")]
        private IWebElement nextBtn;
        public IWebElement getnextBtn()
        {
            return nextBtn;
        }

        [FindsBy(How = How.XPath, Using = "//a[text()[contains(.,'Prev')]]")]
        private IWebElement prevBtn;
        public IWebElement getprevBtn()
        {
            return prevBtn;
        }




    }
}
