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
    public class AboutMePageShould : Base
    {
        [Test]
        //[Ignore("Ignore test")]
        public void VerifyAboutMePageLaunching()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            AboutMePageObject aboutMePage = new AboutMePageObject(getDriver());
            WelcomePageObject welcomePage = new WelcomePageObject(getDriver());

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("validUser.username");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("validUser.password");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            try { 
                menuPage.getbtnContinue().Click();
            }
            catch (Exception ex)
            {
                //TestContext.Progress.WriteLine("Continue button not found.");
                welcomePage.getbtnGetStartedText().Click();
                menuPage.getstartBtn().Click();
                menuPage.getnextBtn().Click();
                menuPage.getmenuBtn().Click();
            }
            menuPage.getintroductionText().Click();
            menuPage.getnextBtn().Click();

            string aboutMePageHeading = aboutMePage.getheadingText().Text;
            string aboutMePageHeadingExpected = "About Me";
            Assert.That(aboutMePageHeading, Is.EqualTo(aboutMePageHeadingExpected));
        }        
        
        [Test]
        //[Ignore("Ignore test")]
        public void VerifyStep2aAlertMessage()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            AboutMePageObject aboutMePage = new AboutMePageObject(getDriver());
            WelcomePageObject welcomePage = new WelcomePageObject(getDriver());


            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("validUser.username");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("validUser.password");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            try
            {
                menuPage.getbtnContinue().Click();
            }
            catch (Exception ex)
            {
                //TestContext.Progress.WriteLine("Continue button not found.");
                welcomePage.getbtnGetStartedText().Click();
                menuPage.getstartBtn().Click();
                menuPage.getnextBtn().Click();
                menuPage.getmenuBtn().Click();
            }
            menuPage.getintroductionText().Click();
            menuPage.getnextBtn().Click();

            String stepPostion = null;
            try
            {
                stepPostion = aboutMePage.getPageType1().Text;
                stepPostion = "Step2a";
            }
            catch (NoSuchElementException)
            {
                try
                {
                    stepPostion = aboutMePage.getnoRadioSelection().Text;
                    stepPostion = "Step2b";
                }
                catch (NoSuchElementException)
                {
                    stepPostion = "Step2c";
                }
            }
            if (stepPostion.Equals("Step2a") || stepPostion.Equals("Step2b"))
            {
                //--verify alert message with invalid credentials
                string ssnInvalid = getDataParser().extractData("aboutMeUser.ssnInvalid");
                aboutMePage.getssnTextbox().SendKeys(ssnInvalid);

                string dobInvalid = getDataParser().extractData("aboutMeUser.dobInvalid");
                aboutMePage.getdobTextbox().SendKeys(dobInvalid);

                aboutMePage.getsubmitBtn().Click();

                string alertTxt = aboutMePage.getalertTxt().Text;
                string alertTxtExpected = @"The information entered above does not match that provided by your employer. Please ensure you have entered your information correctly. If the information you entered is correct, please contact your employer. Once your employer has corrected the information, you may complete the benefits enrollment process.";
                Assert.That(alertTxt, Is.EqualTo(alertTxtExpected));
            }
            else if (stepPostion.Equals("Step2c"))
            {
                TestContext.Progress.WriteLine("Inside Step 2c");
            }
        }

        [Test]
        //[Ignore("Ignore test")]
        public void VerifyStep2aSubmission()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            AboutMePageObject aboutMePage = new AboutMePageObject(getDriver());
            WelcomePageObject welcomePage = new WelcomePageObject(getDriver());

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("validUser.username");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("validUser.password");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            try
            {
                menuPage.getbtnContinue().Click();
            }
            catch (Exception ex)
            {
                //TestContext.Progress.WriteLine("Continue button not found.");
                welcomePage.getbtnGetStartedText().Click();
                menuPage.getstartBtn().Click();
                menuPage.getnextBtn().Click();
                menuPage.getmenuBtn().Click();
            }
            menuPage.getintroductionText().Click();
            menuPage.getnextBtn().Click();

            String stepPostion = null;
            try
            {
                stepPostion = aboutMePage.getPageType1().Text;
                stepPostion = "Step2a";
            }
            catch (NoSuchElementException)
            {
                try
                {
                    stepPostion = aboutMePage.getnoRadioSelection().Text;
                    stepPostion = "Step2b";
                }
                catch (NoSuchElementException)
                {
                    stepPostion = "Step2c";
                }
            }
            if (stepPostion.Equals("Step2a") || stepPostion.Equals("Step2b"))
            {
                //--verify success with valid credentials
                string ssnValid = getDataParser().extractData("validUser.ssnValid");
                aboutMePage.getssnTextbox().Clear();
                aboutMePage.getssnTextbox().SendKeys(ssnValid);

                string dobValid = getDataParser().extractData("validUser.dobValid");
                aboutMePage.getdobTextbox().Clear();
                aboutMePage.getdobTextbox().SendKeys(dobValid);

                aboutMePage.getsubmitBtn().Click();

                string aboutMeStep2bTxt = aboutMePage.getaboutMeStep2bTxt().Text;
                string aboutMeStep2bTxtExpected = "Information Provided by Your Employer:";
                Assert.That(aboutMeStep2bTxt, Is.EqualTo(aboutMeStep2bTxtExpected));
            }
            else if (stepPostion.Equals("Step2c"))
            {
                TestContext.Progress.WriteLine("Inside Step 2c");
            }
            Thread.Sleep(4000);
        }

        [Test]
        //[Ignore("Ignore test")]
        public void VerifyStep2bAlertMessage()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            AboutMePageObject aboutMePage = new AboutMePageObject(getDriver());
            WelcomePageObject welcomePage = new WelcomePageObject(getDriver());

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("validUser.username");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("validUser.password");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            try
            {
                menuPage.getbtnContinue().Click();
            }
            catch (Exception ex)
            {
                //TestContext.Progress.WriteLine("Continue button not found.");
                welcomePage.getbtnGetStartedText().Click();
                menuPage.getstartBtn().Click();
                menuPage.getnextBtn().Click();
                menuPage.getmenuBtn().Click();
            }
            menuPage.getintroductionText().Click();
            menuPage.getnextBtn().Click();
            String stepPostion = null;
            try
            {
                stepPostion = aboutMePage.getPageType1().Text;
                stepPostion = "Step2a";
            }
            catch (NoSuchElementException)
            {
                try
                {
                    stepPostion = aboutMePage.getnoRadioSelection().Text;
                    stepPostion = "Step2b";
                }
                catch (NoSuchElementException)
                {
                    stepPostion = "Step2c";
                }
            }
            if (stepPostion.Equals("Step2a") || stepPostion.Equals("Step2b"))
            {
                //--verify success with valid credentials
                string ssnValid = getDataParser().extractData("validUser.ssnValid");
                aboutMePage.getssnTextbox().Clear();
                aboutMePage.getssnTextbox().SendKeys(ssnValid);

                string dobValid = getDataParser().extractData("validUser.dobValid");
                aboutMePage.getdobTextbox().Clear();
                aboutMePage.getdobTextbox().SendKeys(dobValid);

                aboutMePage.getsubmitBtn().Click();

                menuPage.getnextBtn().Click();
                string alertStep2Txt = aboutMePage.getalertStep2bTxt().Text;
                string alertStep2TxtExpected = @"Please select if the information entered by your employer is correct.";
                Assert.That(alertStep2Txt, Is.EqualTo(alertStep2TxtExpected));

            }
            else if (stepPostion.Equals("Step2c"))
            {
                TestContext.Progress.WriteLine("Inside Step 2c");
            }
        }

        [Test]
        //[Ignore("Ignore test")]
        public void VerifyPopupAlertMessageNoRadioSelection()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            AboutMePageObject aboutMePage = new AboutMePageObject(getDriver());
            WelcomePageObject welcomePage = new WelcomePageObject(getDriver());

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("validUser.username");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("validUser.password");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            try
            {
                menuPage.getbtnContinue().Click();
            }
            catch (Exception ex)
            {
                //TestContext.Progress.WriteLine("Continue button not found.");
                welcomePage.getbtnGetStartedText().Click();
                menuPage.getstartBtn().Click();
                menuPage.getnextBtn().Click();
                menuPage.getmenuBtn().Click();
            }

            menuPage.getintroductionText().Click();
            menuPage.getnextBtn().Click();

            String stepPostion = null;
            try
            {
                stepPostion = aboutMePage.getPageType1().Text;
                stepPostion = "Step2a";
            }
            catch (NoSuchElementException)
            {
                try
                {
                    stepPostion = aboutMePage.getnoRadioSelection().Text;
                    stepPostion = "Step2b";
                }
                catch (NoSuchElementException)
                {
                    stepPostion = "Step2c";
                }
            }
            if (stepPostion.Equals("Step2a") || stepPostion.Equals("Step2b"))
            {
                //--verify success with valid credentials
                string ssnValid = getDataParser().extractData("validUser.ssnValid");
                aboutMePage.getssnTextbox().Clear();
                aboutMePage.getssnTextbox().SendKeys(ssnValid);

                string dobValid = getDataParser().extractData("validUser.dobValid");
                aboutMePage.getdobTextbox().Clear();
                aboutMePage.getdobTextbox().SendKeys(dobValid);

                aboutMePage.getsubmitBtn().Click();

                aboutMePage.getnoRadioSelection().Click();

                menuPage.getnextBtn().Click();
                Thread.Sleep(2000);
                string popupHeaderNoRadioSelectionTxt = aboutMePage.getpopupHeaderNoRadioSelection().Text;
                string popupHeaderNoRadioSelectionTxtExpected = @"Contact Your Employer";
                Assert.That(popupHeaderNoRadioSelectionTxt, Is.EqualTo(popupHeaderNoRadioSelectionTxtExpected));

            }
            else if (stepPostion.Equals("Step2c"))
            {
                TestContext.Progress.WriteLine("Inside Step 2c");
            }
        }

        [Test]
        //[Ignore("Ignore test")]
        public void VerifySubmissionYesRadioSelection()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            AboutMePageObject aboutMePage = new AboutMePageObject(getDriver());
            WelcomePageObject welcomePage = new WelcomePageObject(getDriver());

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("validUser.username");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("validUser.password");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            try
            {
                menuPage.getbtnContinue().Click();
            }
            catch (Exception ex)
            {
                //TestContext.Progress.WriteLine("Continue button not found.");
                welcomePage.getbtnGetStartedText().Click();
                menuPage.getstartBtn().Click();
                menuPage.getnextBtn().Click();
                menuPage.getmenuBtn().Click();
            }

            menuPage.getintroductionText().Click();
            menuPage.getnextBtn().Click();

            //string[] usernameValidList = getDataParser().extractDataArray("dataSet");
            //foreach (var item in usernameValidList)
            //{
            //    TestContext.Progress.WriteLine(item);
            //}            

            String stepPostion = null;
            try
            {
                stepPostion = aboutMePage.getPageType1().Text;
                stepPostion = "Step2a";
            }
            catch (NoSuchElementException)
            {
                try
                {
                    stepPostion = aboutMePage.getnoRadioSelection().Text;
                    stepPostion = "Step2b";
                }
                catch (NoSuchElementException)
                {
                    stepPostion = "Step2c";
                }
            }
            if (stepPostion.Equals("Step2a") || stepPostion.Equals("Step2b"))
            {
                //--verify success with valid credentials
                string ssnValid = getDataParser().extractData("validUser.ssnValid");
                aboutMePage.getssnTextbox().Clear();
                aboutMePage.getssnTextbox().SendKeys(ssnValid);

                string dobValid = getDataParser().extractData("validUser.dobValid");
                aboutMePage.getdobTextbox().Clear();
                aboutMePage.getdobTextbox().SendKeys(dobValid);

                aboutMePage.getsubmitBtn().Click();

                aboutMePage.getyesRadioSelection().Click();

                menuPage.getnextBtn().Click();
                string headerTxtYesRadioSelection = aboutMePage.getheaderTxtYesRadioSelection().Text;
                string headerTxtYesRadioSelectionExpected = "Employee Personal Information";
                Assert.That(headerTxtYesRadioSelection, Is.EqualTo(headerTxtYesRadioSelectionExpected));
            }
            else if (stepPostion.Equals("Step2c"))
            {
                TestContext.Progress.WriteLine("Inside Step 2c");
            }
        }
    }
}