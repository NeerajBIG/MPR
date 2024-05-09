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
    public class SGTLPageObject
    {
        private IWebDriver driver;

        public SGTLPageObject(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//div[contains(@onclick,'GroupTermLife') and contains(.,'Supplemental')]")]
        private IWebElement clkSGTL;
        public IWebElement getclkSGTL()
        { 
            return clkSGTL;
        }

        [FindsBy(How = How.XPath, Using = "//span[text()='Supplemental Group Term Life (SGTL)']")]
        private IWebElement headingText;
        public IWebElement getheadingText()
        {

            return headingText;
        }

        // drop down for coverage amount
        [FindsBy(How = How.XPath, Using = "//select[@name='ContractAmtDropDown.SelectedValue']")]
        private IWebElement ddlCoverage;
        public IWebElement getDdlCoverage()
        {
            return ddlCoverage;
        }

        // SGTL form information for each person

        [FindsBy(How = How.XPath, Using = "//span[text()='FT']/parent::div/input")]
        private IWebElement txtHeightFT;
        public IWebElement getTxtHeightFT()
        {
            return txtHeightFT;
        }

        [FindsBy(How = How.XPath, Using = "//span[text()='IN']/parent::div/input[@maxlength='2']")]
        private IWebElement txtHeightIN;
        public IWebElement getTxtHeightIN()
        {
            return txtHeightIN;
        }

        [FindsBy(How = How.XPath, Using = "")]
        private IWebElement txtCurrentWeight;
        public IWebElement getTxtCurrentWeight()
        {
            return txtCurrentWeight;
        }


    }
    public class MySGTLReflectionClass
    {
        public string MyMethod()
        {
            return DateTime.Now.ToString();
        }
    }
}
