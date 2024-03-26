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
    public class LoginPageObject
    {
        private IWebDriver driver;
        public LoginPageObject(IWebDriver driver) 
        {
            this.driver = driver;
            PageFactory.InitElements(driver,this);
        }

        [FindsBy(How = How.Id, Using = "ctl00_headerMain_lnkLogin")]
        private IWebElement loginLink;
        public IWebElement getloginLink()
        {
            return loginLink;
        }

        [FindsBy(How = How.Id, Using = "input28")]
        private IWebElement username;
        public IWebElement getusername()
        {
            return username;
        }

        [FindsBy(How = How.Id, Using = "input36")]
        private IWebElement password;
        public IWebElement getpassword()
        {
            return password;
        }

        [FindsBy(How = How.XPath, Using = "//input[@type='submit']")]
        private IWebElement submit;
        public IWebElement getsubmit()
        {
            return submit;
        }

        [FindsBy(How = How.XPath, Using = "//*[text() = 'LOG OUT']/parent::li/a")]
        private IWebElement btnLogoutText;
        public IWebElement getbtnLogoutText()
        {
            return btnLogoutText;
        }

        [FindsBy(How = How.XPath, Using = "//span[@class='icon error-16']/parent::div/p")]
        private IWebElement txtError;
        public IWebElement gettxtError()
        {
            return txtError;
        }

    }
}
