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
    /// Test Scenario: Verify login functionality.
    /// Purpose: 
    /// 1) To ensure that users can successfully log in to the application with valid credentials.
    /// 2) To ensure that users cannot log in to the application with Invalid credentials. Any error messages indicating login failure should not be displayed. 
    /// </summary>
    /// <testcase1="VerifyLandingPageLaunching">This will verify successful launch of landing page.</testcase1>
    /// <expectedcondition>User should be able to launch the application on any web browser.</expectedcondition>
    /// <testcase2="VerifyLoginPageLaunching">This will verify successful launch of login page.</testcase2>
    /// <expectedcondition>User should be able to open the login page.</expectedcondition>
    /// <testcase3="VerifyUserLoginValidCred">This will verify login attempt with valid credentials.</testcase3>
    /// <expectedcondition>User should be able to login with valid credentials.</expectedcondition>
    /// <testcase4="VerifyUserLoginInvalidCred">This will verify unsuccessful login attempt with Invalid credentials.</testcase4>
    /// <expectedcondition>User should not be able to login with Invalid credentials. An error message should display</expectedcondition>
    // -------------------------------------------------------------------------------

    [Parallelizable(ParallelScope.Self)]
    public class LoginPageShould : Base
    {
        [Test, Order(1)]
        //[Ignore("Ignore test")]
        public void VerifyLandingPageLaunching()
        {
            string landingPageTitle = driver.Title;
            string landingPageTitleExpected = "DMBA.com";
            Assert.That(landingPageTitle, Is.EqualTo(landingPageTitleExpected));
        }

        [Test, Order(2)]
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
        [Test, Order(3)]
        public void VerifyUserLoginValidCred()
        {
            string[] usernameValidList = getDataParser().extractDataArray("newUser.dataSet");
            int i = 0;
            foreach (var item in usernameValidList)
            {
                LoginPageObject loginPage = new LoginPageObject(getDriver());
                loginPage.getloginLink().Click();

                loginPage.getusername().SendKeys(usernameValidList[i]);

                string passwordValid = getDataParser().extractData("newUser.password");
                
                loginPage.getpassword().SendKeys(passwordValid);

                loginPage.getsubmit().Click();

                string btnLogoutText = loginPage.getbtnLogoutText().Text;
                string btnLogoutTextExpected = "LOG OUT";
                Assert.That(btnLogoutText, Is.EqualTo(btnLogoutTextExpected));                
                Thread.Sleep(2000);
                loginPage.getbtnLogoutText().Click();
                i++;
            }         
        }

        [Test, Order(4)]
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
            if (txtError.Contains("Unable"))
            {
                Assert.That(txtError, Is.EqualTo(txtErrorExpected1));
            }
            else if (txtError.Contains("Your"))
            {
                Assert.That(txtError, Is.EqualTo(txtErrorExpected2));
            }
            else
            {
                Assert.Fail("Unknown error message displayed");
            }
        }
    }
}