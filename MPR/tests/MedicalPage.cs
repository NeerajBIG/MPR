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

        [Test]
        //[Ignore("Ignore test")]
        public void VerifyMedicalPageLinkToNewTab()
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

            // Get current window handle
            string originalWindow = driver.CurrentWindowHandle;
            medicalPage.getcomparePlanlink().Click();

            foreach (string window in driver.WindowHandles)
            {
                if (originalWindow != window)
                {
                    driver.SwitchTo().Window(window);
                    break;
                }
            }

            // Did we switch to new pop up tab successfully?
            string medicalPagePopUpHeading = medicalPage.getpopUpHeading().Text;
            string medicalPagePopUpHeadingExpected = "2024 Plan Comparison Information";
            Assert.That(medicalPagePopUpHeading, Is.EqualTo(medicalPagePopUpHeadingExpected));

            // verify that plan type is medical
            string MedicalPagePopUpDropdownSelection = medicalPage.getplanTypeSelection().Text;
            string MedicalPagePopUpDropdownSelectionExpected = "Medical";
            Assert.That(MedicalPagePopUpDropdownSelection, Is.EqualTo(MedicalPagePopUpDropdownSelectionExpected));

            // verify that zip code is equal to zipcode entered on about me page
            string MedicalPagePopUpZipCodeTextBox = medicalPage.getzipCode().GetAttribute("Value");
            string MedicalPagePopUpZipCodeTextBoxExpect = getDataParser().extractData("zipcode2Valid");
            Assert.That(MedicalPagePopUpZipCodeTextBox, Is.EqualTo(MedicalPagePopUpZipCodeTextBoxExpect));

            // Verify DP Plan Exists on Page.
            string MedicalPagePlanDPremier = medicalPage.gettxtDeseretPremier().Text;
            string MedicalPagePlanDPremierExpected = "Deseret Premier";
            Assert.That(MedicalPagePlanDPremier, Is.EqualTo(MedicalPagePlanDPremierExpected));

            // Verify DV Plan Exists on Page.
            string MedicalPagePlanDValue = medicalPage.gettxtDeseretValue().Text;
            string MedicalPagePlanDValueExpected = "Deseret Value";
            Assert.That(MedicalPagePlanDValue, Is.EqualTo(MedicalPagePlanDValueExpected));

            // Verify DS Plan Exists on Page.
            string MedicalPagePlanDSelect = medicalPage.gettxtDeseretSelect().Text;
            string MedicalPagePlanDSelectExpected = "Deseret Select";
            Assert.That(MedicalPagePlanDSelect, Is.EqualTo(MedicalPagePlanDSelectExpected));

            // Verify DP Plan Exists on Page.
            string MedicalPagePlanDProtect = medicalPage.gettxtDeseretProtect().Text;
            string MedicalPagePlanDProtectExpected = "Deseret Protect";
            Assert.That(MedicalPagePlanDProtect.Contains(MedicalPagePlanDProtectExpected));
        }

        [Test]
        //[Ignore("Ignore test")]
        public void VerifyMedicalPagePlanChangeWithZipCodeChange()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            AboutMePageObject aboutMePage = new AboutMePageObject(getDriver());
            MedicalPageObject medicalPage = new MedicalPageObject(getDriver());

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("usernameValid2");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("passwordValid");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            menuPage.getbtnContinue().Click();

            medicalPage.getclkMedical().Click();
            string originalWindow = driver.CurrentWindowHandle;

            // change ZipCode for TC_0118
            driver.SwitchTo().NewWindow(WindowType.Tab);
            driver.Url = "https://demo2.dmba.com/DMBA_Enrollment/IE/AboutMe/PersonalInfo";
            aboutMePage.getzipCode().SendKeys(getDataParser().extractData("zipcodeValid118"));
            aboutMePage.getnextBtn().Click();
            driver.Close();

            // refresh page and check if plan is correct.
            driver.SwitchTo().Window(originalWindow);
            driver.Navigate().Refresh();


            // Verify DP Plan Exists on Page.
            string MedicalPagePlanDPremier = medicalPage.gettxtDeseretPremier().Text;
            string MedicalPagePlanDPremierExpected = "Deseret Premier";
            Assert.That(MedicalPagePlanDPremier, Is.EqualTo(MedicalPagePlanDPremierExpected));

            // Verify DV Plan Exists on Page.
            string MedicalPagePlanDValue = medicalPage.gettxtDeseretValue().Text;
            string MedicalPagePlanDValueExpected = "Deseret Value";
            Assert.That(MedicalPagePlanDValue, Is.EqualTo(MedicalPagePlanDValueExpected));

            // Verify DS Plan Exists on Page.
            string MedicalPagePlanDSelect = medicalPage.gettxtDeseretSelect().Text;
            string MedicalPagePlanDSelectExpected = "Deseret Select";
            Assert.That(MedicalPagePlanDSelect, Is.EqualTo(MedicalPagePlanDSelectExpected));

            // Verify DP Plan Exists on Page.
            string MedicalPagePlanDProtect = medicalPage.gettxtDeseretProtect().Text;
            string MedicalPagePlanDProtectExpected = "Deseret Protect";
            Assert.That(MedicalPagePlanDProtect.Contains(MedicalPagePlanDProtectExpected));








        }
    }
}