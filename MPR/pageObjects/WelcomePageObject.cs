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
    public class WelcomePageObject
    {
        private IWebDriver driver;
        public WelcomePageObject(IWebDriver driver) 
        {
            this.driver = driver;
            PageFactory.InitElements(driver,this);
        }

        [FindsBy(How = How.XPath, Using = "//*[text() = 'WELCOME TO BENEFITS ENROLLMENT']")]
        private IWebElement welcomePageHeading;
        public IWebElement getWelcomePageHeading()
        {
            return welcomePageHeading;
        }

        [FindsBy(How = How.XPath, Using = "//a[@class='ieBtn ieBtn-primary btn-lg health-dark']")]
        private IWebElement btnGetStartedText;
        public IWebElement getbtnGetStartedText()
        {
            return btnGetStartedText;
        }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'CONTINUE']")]
        private IWebElement btnContinueText;
        public IWebElement getbtnContinueText()
        {
            return btnContinueText;
        }



    }
}
