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
    public class ConfirmationPageObject
    {
        private IWebDriver driver;

        public ConfirmationPageObject(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//div[contains(@onclick,'Confirmation') and contains(.,'Confirmation')]")]
        private IWebElement clkConfirmation;
        public IWebElement getclkConfirmation()
        { 
            return clkConfirmation;
        }

        [FindsBy(How = How.XPath, Using = "//span[text()='Review Your Selections']")]
        private IWebElement headingText;
        public IWebElement getheadingText()
        {

            return headingText;
        }

        [FindsBy(How = How.XPath, Using = "//div[@class='row alert alert-warning']")]
        private IWebElement errorMsgBanner;
        public IWebElement getErrorMsgBanner()
        {

            return errorMsgBanner;
        }

        [FindsBy(How = How.XPath, Using = "//h3[text()='Dependents']//parent::div[@class='col-md-10 hidden-sm hidden-xs']//tbody/tr/td")]
        private IList<IWebElement> dependentsTableValues;
        public IList<IWebElement> getDependentsTableValues()
        {

            return dependentsTableValues;
        }

        [FindsBy(How = How.XPath, Using = "//h3[text()='Your Benefit Choices']//parent::div//tbody/tr")]
        private IList<IWebElement> benefitsTableValues;
        public IList<IWebElement> getBenefitsTableValues()
        {

            return benefitsTableValues;
        }



    }
    public class MyConfirmationReflectionClass
    {
        public string MyMethod()
        {
            return DateTime.Now.ToString();
        }
    }
}
