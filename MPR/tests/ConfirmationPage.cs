using MPR.pageObjects;
using MPR.utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V121.Page;
using OpenQA.Selenium.Interactions;
using System;
using System.Reflection;
using System.Reflection.Metadata;
using System.Security.Policy;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MPR.tests
{
    // -------------------------------------------------------------------------------
    /// <summary>
    /// This is the description of the method.
    /// </summary>
    /// <param name="firstParameter">This is the first parameter.</param>
    /// <returns>This is the description of the return value.</returns>
    // -------------------------------------------------------------------------------
    public class ConfirmationShould : Base
    {

        // -------------------------------------------------------------------------------
        /// <summary>
        /// Check Menu button to navigate to FSA works.
        /// </summary>
        /// <param name="firstParameter">This is the first parameter.</param>
        // -------------------------------------------------------------------------------
        [Test]
        //[Ignore("Ignore test")]
        public void VerifyConfirmationPageLaunching()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            FSAPageObject FSAPage = new FSAPageObject(getDriver());
            ConfirmationPageObject ConfirmationPage = new ConfirmationPageObject(getDriver());

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("medicalUser.username");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("medicalUser.password");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            menuPage.getbtnContinue().Click();

            ConfirmationPage.getclkConfirmation().Click();
            Thread.Sleep(1000);

            string ConfirmationPageHeading = ConfirmationPage.getheadingText().Text;
            string ConfirmationPageHeadingExpected = "Review Your Selections";
            Assert.That(ConfirmationPageHeading, Is.EqualTo(ConfirmationPageHeadingExpected));
        }


        // -------------------------------------------------------------------------------
        /// <summary>
        /// Check Menu button to navigate to FSA works.
        /// </summary>
        /// <param name="firstParameter">This is the first parameter.</param>
        // -------------------------------------------------------------------------------
        [Test]
        //[Ignore("Ignore test")]
        public void VerifyConfirmationPageDependentsGrid()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            FSAPageObject FSAPage = new FSAPageObject(getDriver());
            ConfirmationPageObject ConfirmationPage = new ConfirmationPageObject(getDriver());

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("medicalUser.username");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("medicalUser.password");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            menuPage.getbtnContinue().Click();
            Thread.Sleep(5000);

            ConfirmationPage.getclkConfirmation().Click();
            Thread.Sleep(5000);


            // Expected Confirmation Values


            string expectedMedicalPlan = "";
            string expectedMedicalPlanRate = "";
            string expectedDentalPlan = "";
            string expectedDentalPlanRate = "";
            string expectedVisionPlan = "";
            string expectedVisionPlanRate = "";


            IList<IWebElement> dependentRows = ConfirmationPage.getDependentsTableValues();
            foreach (IWebElement dependentRow in dependentRows)
            {
                Console.WriteLine(dependentRow.Text);
            }

            Thread.Sleep(5000);

            IList<IWebElement> benefitRows = ConfirmationPage.getBenefitsTableValues();
            foreach(IWebElement benefitRow in benefitRows)
            {
                Console.WriteLine(benefitRow.Text);
            }


        }








    }    
}