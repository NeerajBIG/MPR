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
    public class IntroductionPageObject
    {
        private IWebDriver driver;
        public IntroductionPageObject(IWebDriver driver) 
        {
            this.driver = driver;
            PageFactory.InitElements(driver,this);
        }

        [FindsBy(How = How.XPath, Using = "//span[text()='Introduction']")]
        private IWebElement clkIntroduction;
        public IWebElement getclkIntroduction()
        {
            return clkIntroduction;
        }

        [FindsBy(How = How.XPath, Using = "//h1[@class='page-header row']/span[1]")]
        private IWebElement headingText;
        public IWebElement getheadingText()
        {
            return headingText;
        }

        [FindsBy(How = How.XPath, Using = "//h1[@class='page-header row']/parent::div/p[1]")]
        private IWebElement contentText;
        public IWebElement getcontentText()
        {
            return contentText;
        }



    }
}
