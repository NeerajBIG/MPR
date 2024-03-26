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

        [FindsBy(How = How.XPath, Using = "//span[text()='About Me']")]
        private IWebElement headingText;
        public IWebElement getheadingText()
        {
            return headingText;
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
        private IWebElement aboutMeStep2Txt;
        public IWebElement getaboutMeStep2Txt()
        {
            return aboutMeStep2Txt;
        }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Please select if the information entered by your employer is correct.')]")]
        private IWebElement alertStep2aTxt;
        public IWebElement getalertStep2aTxt()
        {
            return alertStep2aTxt;
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



    }
}
