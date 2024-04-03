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

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("usernameValid");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("passwordValid");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            menuPage.getbtnContinue().Click();

            aboutMePage.getclkAboutMe().Click();

            string aboutMePageHeading = aboutMePage.getheadingText().Text;
            string aboutMePageHeadingExpected = "About Me";
            Assert.That(aboutMePageHeading, Is.EqualTo(aboutMePageHeadingExpected));

        }        
        
        [Test]
        //[Ignore("Ignore test")]
        public void VerifyAlertInvalidInput()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            AboutMePageObject aboutMePage = new AboutMePageObject(getDriver());

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("usernameValid");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("passwordValid");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            menuPage.getbtnContinue().Click();

            aboutMePage.getclkAboutMe().Click();

            String stepPostion = aboutMePage.getstepPosition().Text;
            if(stepPostion.Contains("Step 1 of"))
            {
                TestContext.Progress.WriteLine("Inside Step 1");
                string ssnInvalid = getDataParser().extractData("ssnInvalid");
                aboutMePage.getssnTextbox().SendKeys(ssnInvalid);

                string dobInvalid = getDataParser().extractData("dobInvalid");
                aboutMePage.getdobTextbox().SendKeys(dobInvalid);

                aboutMePage.getsubmitBtn().Click();

                string alertTxt = aboutMePage.getalertTxt().Text;
                string alertTxtExpected = @"The information entered above does not match that provided by your employer. Please ensure you have entered your information correctly. If the information you entered is correct, please contact your employer. Once your employer has corrected the information, you may complete the benefits enrollment process.";
                Assert.That(alertTxt, Is.EqualTo(alertTxtExpected));
            }
            else if (stepPostion.Contains("Step 2 of"))
            {
                TestContext.Progress.WriteLine("Inside Step 2");
            }            
        }

        [Test]
        //[Ignore("Ignore test")]
        public void VerifyStep1Submission()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            AboutMePageObject aboutMePage = new AboutMePageObject(getDriver());

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("usernameValid");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("passwordValid");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            menuPage.getbtnContinue().Click();

            aboutMePage.getclkAboutMe().Click();

            String stepPostion = aboutMePage.getstepPosition().Text;
            if (stepPostion.Contains("Step 1 of"))
            {
                TestContext.Progress.WriteLine("Inside Step 1");
                string ssnInvalid = getDataParser().extractData("ssnValid");
                aboutMePage.getssnTextbox().SendKeys(ssnInvalid);

                string dobValid = getDataParser().extractData("dobValid");
                aboutMePage.getdobTextbox().SendKeys(dobValid);

                aboutMePage.getsubmitBtn().Click();

                string aboutMeStep2Txt = aboutMePage.getaboutMeStep2Txt().Text;
                string aboutMeStep2TxtExpected = @"Information Provided by Your Employer:";
                Assert.That(aboutMeStep2Txt, Is.EqualTo(aboutMeStep2TxtExpected));
            }
            else if (stepPostion.Contains("Step 2 of"))
            {
                TestContext.Progress.WriteLine("Inside Step 2");
            }            
        }

        [Test]
        //[Ignore("Ignore test")]
        public void VerifyStep2AlertMessage()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            AboutMePageObject aboutMePage = new AboutMePageObject(getDriver());

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("usernameValid");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("passwordValid");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            menuPage.getbtnContinue().Click();

            aboutMePage.getclkAboutMe().Click();

            String stepPostion = aboutMePage.getstepPosition().Text;
            if (stepPostion.Contains("Step 1 of"))
            {
                TestContext.Progress.WriteLine("Inside Step 1");
                string ssnInvalid = getDataParser().extractData("ssnValid");
                aboutMePage.getssnTextbox().SendKeys(ssnInvalid);

                string dobValid = getDataParser().extractData("dobValid");
                aboutMePage.getdobTextbox().SendKeys(dobValid);

                aboutMePage.getsubmitBtn().Click();

                menuPage.getnextBtn().Click();

                string alertStep2Txt = aboutMePage.getalertStep2aTxt().Text;
                string alertStep2TxtExpected = @"Please select if the information entered by your employer is correct.";
                Assert.That(alertStep2Txt, Is.EqualTo(alertStep2TxtExpected));
            }
            else if (stepPostion.Contains("Step 2 of"))
            {
                TestContext.Progress.WriteLine("Inside Step 2");
            }            
        }

        [Test]
        //[Ignore("Ignore test")]
        public void VerifyPopupAlertMessageNoSelection()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            AboutMePageObject aboutMePage = new AboutMePageObject(getDriver());

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("usernameValid");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("passwordValid");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            menuPage.getbtnContinue().Click();

            aboutMePage.getclkAboutMe().Click();

            String stepPostion = aboutMePage.getstepPosition().Text;
            TestContext.Progress.WriteLine("Inside Step 1");

            if (stepPostion.Contains("Step 1 of"))
            {
                TestContext.Progress.WriteLine("Inside Step 1");
                string ssnInvalid = getDataParser().extractData("ssnValid");
                aboutMePage.getssnTextbox().SendKeys(ssnInvalid);

                string dobValid = getDataParser().extractData("dobValid");
                aboutMePage.getdobTextbox().SendKeys(dobValid);

                aboutMePage.getsubmitBtn().Click();

                aboutMePage.getnoRadioSelection().Click();

                menuPage.getnextBtn().Click();
                Thread.Sleep(3000);
                string popupHeaderNoRadioSelectionTxt = aboutMePage.getpopupHeaderNoRadioSelection().Text;
                string popupHeaderNoRadioSelectionTxtExpected = @"Contact Your Employer";
                Assert.That(popupHeaderNoRadioSelectionTxt, Is.EqualTo(popupHeaderNoRadioSelectionTxtExpected));
            }
            else if (stepPostion.Contains("Step 2 of"))
            {
                TestContext.Progress.WriteLine("Inside Step 2");
            }            
        }

        [Test]
        //[Ignore("Ignore test")]
        public void VerifySubmissionYesSelection()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            AboutMePageObject aboutMePage = new AboutMePageObject(getDriver());

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("usernameValid");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("passwordValid");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            menuPage.getbtnContinue().Click();

            aboutMePage.getclkAboutMe().Click();

            String stepPostion = aboutMePage.getstepPosition().Text;
            TestContext.Progress.WriteLine("stepPostion is " + stepPostion);

            //string[] usernameValidArray = getDataParser().extractDataArray("usernameValid");
            string[] usernameValidList = getDataParser().extractDataArray("dataSet");
            foreach (var item in usernameValidList)
            {
                TestContext.Progress.WriteLine(item);
            }


            if (stepPostion.Contains("Step 1 of"))
            {
                TestContext.Progress.WriteLine("Inside Step 1");
                string ssnInvalid = getDataParser().extractData("ssnValid");
                aboutMePage.getssnTextbox().SendKeys(ssnInvalid);

                string dobValid = getDataParser().extractData("dobValid");
                aboutMePage.getdobTextbox().SendKeys(dobValid);

                aboutMePage.getsubmitBtn().Click();

                aboutMePage.getyesRadioSelection().Click();

                menuPage.getnextBtn().Click();
                string headerTxtYesRadioSelection = aboutMePage.getheaderTxtYesRadioSelection().Text;
                string headerTxtYesRadioSelectionExpected = "Employee Personal Information";
                Assert.That(headerTxtYesRadioSelection, Is.EqualTo(headerTxtYesRadioSelectionExpected));
            }
            else if (stepPostion.Contains("Step 2 of"))
            {
                TestContext.Progress.WriteLine("Inside Step 2");
            }
        }
    }
}