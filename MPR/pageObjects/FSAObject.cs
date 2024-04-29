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
    public class FSAPageObject
    {
        private IWebDriver driver;

        public FSAPageObject(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Flex') and contains(text(),'Spending') and contains(text(),'Accounts')]")]
        private IWebElement clkDental;
        public IWebElement getclkFSA()
        { 
            return clkDental;
        }

        [FindsBy(How = How.XPath, Using = "//span[text()='Flexible Spending Account (FSA)']")]
        private IWebElement headingText;
        public IWebElement getheadingText()
        {
            return headingText;
        }

        [FindsBy(How = How.XPath, Using = "//a[text()='Read More']")]
        private IWebElement readMoreBtn;
        public IWebElement getReadMoreBtn()
        {
            return readMoreBtn;
        }

        [FindsBy(How = How.XPath, Using = "//a[text()='Where can I find more information?']")]
        private IWebElement moreInformationLink;
        public IWebElement getmoreInformationLink()
        {
            return moreInformationLink;
        }

        [FindsBy(How = How.XPath, Using = "//pdf-viewer[@id='viewer']")]
        private IWebElement pdfViewer;
        public IWebElement getpdfViewer()
        {
            return pdfViewer;
        }


        [FindsBy(How = How.XPath, Using = "//div[@class='alert alert-danger']//li")]
        private IList<IWebElement> enterValuesErrorAlert;
        public IList<IWebElement> getenterValuesErrorAlert()
        {
            return enterValuesErrorAlert;
        }

        [FindsBy(How = How.XPath, Using = "//input[@id='NewMedicalAmount']")]
        private IWebElement healthCareFSATextBox;
        public IWebElement gethealthCareFSATextBox()
        {
            return healthCareFSATextBox;
        }

        [FindsBy(How = How.XPath, Using = "//input[@id='NewDependentAmount']")]
        private IWebElement dependentCareFSATextBox;
        public IWebElement getdependentCareFSATextBox()
        {
            return dependentCareFSATextBox;
        }





    }
    public class MyFSAReflectionClass
    {
        public string MyMethod()
        {
            return DateTime.Now.ToString();
        }
    }
}
