using MPR.pageObjects;
using MPR.utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Configuration;
using System.Data;

namespace MPR.tests
{
    // -------------------------------------------------------------------------------
    /// <summary>
    /// This is the description of the method.
    /// </summary>
    /// <param name="firstParameter">This is the first parameter.</param>
    /// <returns>This is the description of the return value.</returns>
    /// Hello World again 123
    // -------------------------------------------------------------------------------

    public class LoginPageShould : Base
    {
        [Test]
        //[Ignore("Ignore test")]
        public void VerifyLandingPageLaunching()
        {
            string landingPageTitle = driver.Title;
            string landingPageTitleExpected = "DMBA.com";
            Assert.That(landingPageTitle, Is.EqualTo(landingPageTitleExpected));
        }
        [Test]
        //[Ignore("Ignore test")]
        public void VerifyLoginPageLaunching()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            loginPage.getloginLink().Click();

            string loginPageTitle = driver.Title;
            string loginPageTitleExpected = "DMBA Login";
            Assert.That(loginPageTitle, Is.EqualTo(loginPageTitleExpected));
            Thread.Sleep(2000);
        }
        //[Test, TestCaseSource("AddTestDataConfig")]
        [Test]
        public void VerifyUserLoginValidCred()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("usernameValid");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("passwordValid");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            string[] usernameValidList = getDataParser().extractDataArray("dataSet");
            foreach (var item in usernameValidList)
            {
                TestContext.Progress.WriteLine(item);
            }

            string btnLogoutText = loginPage.getbtnLogoutText().Text;
            //TestContext.Progress.WriteLine(btnLogoutText);
            string btnLogoutTextExpected = "LOG OUT";
            Assert.That(btnLogoutText, Is.EqualTo(btnLogoutTextExpected));
            Thread.Sleep(5000);
        }
        [Test]
        //[Ignore("Ignore test")]
        public void VerifyUserLoginInvalidCred()
        {

            var mName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            TestContext.Progress.WriteLine(mName);

            LoginPageObject loginPage = new LoginPageObject(getDriver());
            loginPage.getloginLink().Click();

            string usernameInvalid = getDataParser().extractData("usernameInvalid");
            loginPage.getusername().SendKeys(usernameInvalid);

            string passwordInvalid = getDataParser().extractData("passwordInvalid");
            loginPage.getpassword().SendKeys(passwordInvalid);

            loginPage.getsubmit().Click();

            string txtError = loginPage.gettxtError().Text;
            TestContext.Progress.WriteLine(txtError);
            string txtErrorExpected1 = "Unable to sign in";
            string txtErrorExpected2 = "Your account is locked. Please contact your administrator.";
            Assert.That(txtError, Is.EqualTo(txtErrorExpected1));
            //Assert.That(txtError, Is.EqualTo(txtErrorExpected2));
            Thread.Sleep(5000);
        }

        //public static IEnumerable<TestCaseData> AddTestDataConfig()
        //{
        //    yield return new TestCaseData(getDataParser().extractData("username"), getDataParser().extractData("password"));
        //    yield return new TestCaseData(getDataParser().extractData("usernameInvalid"), getDataParser().extractData("passwordInvalid"));

        //}

    }
}