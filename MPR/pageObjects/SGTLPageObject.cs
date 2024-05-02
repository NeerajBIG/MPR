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


    }
    public class MySGTLReflectionClass
    {
        public string MyMethod()
        {
            return DateTime.Now.ToString();
        }
    }
}
