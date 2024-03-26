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



    }
}
