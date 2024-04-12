using MPR.pageObjects;
using MPR.utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Security.Policy;
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
    public class DependentsPageShould : Base
    {

        [Test]
        //[Ignore("Ignore test")]
        public void VerifyDependentsPageLaunching()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            DependentsPageObject dependentsPage = new DependentsPageObject(getDriver());

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("medicalUser.username");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("medicalUser.password");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            menuPage.getbtnContinue().Click();

            dependentsPage.getclkDependents().Click();

            string medicalPageHeading = dependentsPage.getheadingText().Text;
            string medicalPageHeadingExpected = "Add Dependents";
            Assert.That(medicalPageHeading, Is.EqualTo(medicalPageHeadingExpected));
        }


    }
}