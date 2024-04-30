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


        // Hyper link related
        [FindsBy(How = How.XPath, Using = "//title")]
        private IWebElement verifyPDFTitle;
        public IWebElement getVerifyPDFTitle()
        {
            return verifyPDFTitle;
        }
        // Group Term Life (GTF)
        [FindsBy(How = How.XPath, Using = "//a[contains(@href,'gtlstd.pdf')]")]
        private IWebElement moreGTFInformationLink;
        public IWebElement getmoreGTFInformationLink()
        {
            return moreGTFInformationLink;
        }
        // Disability Plan (DP)
        [FindsBy(How = How.XPath, Using = "//a[contains(@href,'disstd.pdf')]")]
        private IWebElement moreDPInformationLink;
        public IWebElement getmoreDPInformationLink()
        {
            return moreDPInformationLink;
        }
        // Occupational Accidental Death & Dismemberment (OAD&D)
        [FindsBy(How = How.XPath, Using = "//a[contains(@href,'oadstd.pdf')]")]
        private IWebElement moreOADDInformationLink;
        public IWebElement getmoreOADDInformationLink()
        {
            return moreOADDInformationLink;
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
