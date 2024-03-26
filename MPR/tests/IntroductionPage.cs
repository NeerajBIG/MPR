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
    public class IntroductionPageShould : Base
    {
        [Test]
        //[Ignore("Ignore GetStartedButton test")]
        public void VerifyPageLaunching()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            IntroductionPageObject introPage = new IntroductionPageObject(getDriver());

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("usernameValid");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("passwordValid");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();            

            menuPage.getbtnContinue().Click();

            introPage.getclkIntroduction().Click();

            string introductionPageHeading = introPage.getheadingText().Text;
            string introductionPageHeadingExpected = "Welcome to DMBA's online benefits enrollment system";
            Assert.That(introductionPageHeading, Is.EqualTo(introductionPageHeadingExpected));
        }

        [Test]
        //[Ignore("Ignore test")]
        public void VerifyPageHeader()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            IntroductionPageObject introPage = new IntroductionPageObject(getDriver());

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("usernameValid");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("passwordValid");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            menuPage.getbtnContinue().Click();

            introPage.getclkIntroduction().Click();

            string introductionPageHeading = introPage.getheadingText().Text;
            string introductionPageHeadingExpected = "Welcome to DMBA's online benefits enrollment system";
            Assert.That(introductionPageHeading, Is.EqualTo(introductionPageHeadingExpected));
        }

        [Test]
        //[Ignore("Ignore test")]
        public void VerifyPageContent()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            IntroductionPageObject introPage = new IntroductionPageObject(getDriver());

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("usernameValid");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("passwordValid");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            menuPage.getbtnContinue().Click();

            introPage.getclkIntroduction().Click();

            string step1PageContent = introPage.getcontentText().Text;
            string step1PageContentExpected = @"Your employer has authorized DMBA to manage your health and financial employment benefits. DMBA's mission is to be best in class in serving and assisting you on your journey toward physical health and financial security. DMBA has been named the top benefits administrator in the state of Utah for the last seven years, and we hope you will come to think of us a trusted partner and advocate, committed to helping you achieve your health and financial goals.";
            Assert.That(step1PageContent, Is.EqualTo(step1PageContentExpected));
        }
    }
}