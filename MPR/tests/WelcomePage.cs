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

    [Parallelizable(ParallelScope.Self)]
    public class WelcomePageShould : Base
    {

        [Test]
        public void VerifyWelcomePageLaunching()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("usernameValid");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("passwordValid");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            WelcomePageObject welcomePage = new WelcomePageObject(getDriver());

            string welcomePageHeading = welcomePage.getWelcomePageHeading().Text;

            string welcomePageHeadingExpected = "WELCOME TO BENEFITS ENROLLMENT";
            Assert.That(welcomePageHeading, Is.EqualTo(welcomePageHeadingExpected));
        }
        [Test]
        [Ignore("Ignore GetStartedButton test")]
        public void VerifyGetStartedButton()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("usernameValid");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("passwordValid");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            WelcomePageObject welcomePage = new WelcomePageObject(getDriver());

            string btnGetStartedText = welcomePage.getbtnGetStartedText().Text;
            
            string btnGetStartedTextExpected = "GET STARTED";
            Assert.That(btnGetStartedText, Is.EqualTo(btnGetStartedTextExpected));
        }
        [Test]
        public void VerifyContinueButton()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("usernameValid");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("passwordValid");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            WelcomePageObject welcomePage = new WelcomePageObject(getDriver());

            string btnContinueText = welcomePage.getbtnContinueText().Text;

            string btnContinueTextExpected = "CONTINUE";
            Assert.That(btnContinueText, Is.EqualTo(btnContinueTextExpected));
        }
    }
}