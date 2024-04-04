using MPR.pageObjects;
using MPR.utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MPR.tests
{
    // -------------------------------------------------------------------------------
    /// <summary>
    /// This is the description of the method.
    /// </summary>
    /// <param name="firstParameter">This is the first parameter.</param>
    /// <returns>This is the description of the return value.</returns>
    // -------------------------------------------------------------------------------
    public class MedicalPageShould : Base
    {
        [Test]
        //[Ignore("Ignore test")]
        public void VerifyMedicalPageLaunching()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            MedicalPageObject medicalPage = new MedicalPageObject(getDriver());

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("usernameValid2");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("passwordValid");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            menuPage.getbtnContinue().Click();

            medicalPage.getclkMedical().Click();

            string medicalPageHeading = medicalPage.getheadingText().Text;
            string medicalPageHeadingExpected = "Medical";
            Assert.That(medicalPageHeading, Is.EqualTo(medicalPageHeadingExpected));

        }
    }
}