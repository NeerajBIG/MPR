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

            string usernameValid = getDataParser().extractData("medicalUser.username");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("medicalUser.password");
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
        public void VerifyMedicalPageReadMoreButton()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            MedicalPageObject medicalPage = new MedicalPageObject(getDriver());

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("medicalUser.username");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("medicalUser.password");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            menuPage.getbtnContinue().Click();

            medicalPage.getclkMedical().Click();

            // Verify that read only button works to show more text
            medicalPage.getreadMoreBtn().Click();
            string inputValueReadMore = medicalPage.getreadMoreBtnCollapseInput().GetAttribute("value");
            TestContext.Progress.WriteLine("Input Value is " + inputValueReadMore);
            string inputValueReadMoreExpected = "false";
            Assert.That(inputValueReadMore, Is.EqualTo(inputValueReadMoreExpected));

            // Verify that read only button works to less text
            medicalPage.getreadMoreBtn().Click();
            string inputValueReadLess = medicalPage.getreadMoreBtnCollapseInput().GetAttribute("value");
            TestContext.Progress.WriteLine("Input Value is " + inputValueReadLess);
            string inputValueReadLessExpected = "true";
            Assert.That(inputValueReadLess, Is.EqualTo(inputValueReadLessExpected));

        }

        [Test]
        //[Ignore("Ignore test")]
        public void VerifyMedicalPageLinkToNewTab()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            MedicalPageObject medicalPage = new MedicalPageObject(getDriver());

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("medicalUser.username");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("medicalUser.password");
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
            string MedicalPagePopUpZipCodeTextBoxExpect = getDataParser().extractData("medicalUser.zipcode2Valid");
            Assert.That(MedicalPagePopUpZipCodeTextBox, Is.EqualTo(MedicalPagePopUpZipCodeTextBoxExpect));

            // Verify DP Plan Exists on Page.
            string MedicalPagePlanDPremier = medicalPage.gettxtDeseretPremierPopUp().Text;
            string MedicalPagePlanDPremierExpected = "Deseret Premier";
            Assert.That(MedicalPagePlanDPremier, Is.EqualTo(MedicalPagePlanDPremierExpected));

            // Verify DV Plan Exists on Page.
            string MedicalPagePlanDValue = medicalPage.gettxtDeseretValuePopUp().Text;
            string MedicalPagePlanDValueExpected = "Deseret Value";
            Assert.That(MedicalPagePlanDValue, Is.EqualTo(MedicalPagePlanDValueExpected));

            // Verify DS Plan Exists on Page.
            string MedicalPagePlanDSelect = medicalPage.gettxtDeseretSelectPopUp().Text;
            string MedicalPagePlanDSelectExpected = "Deseret Select";
            Assert.That(MedicalPagePlanDSelect, Is.EqualTo(MedicalPagePlanDSelectExpected));

            // Verify DP Plan Exists on Page.
            string MedicalPagePlanDProtect = medicalPage.gettxtDeseretProtectPopUp().Text;
            string MedicalPagePlanDProtectExpected = "Deseret Protect";
            Assert.That(MedicalPagePlanDProtect, Does.Contain(MedicalPagePlanDProtectExpected));
        }



        [Test]
        //[Ignore("Ignore test")]
        public void VerifyMedicalPagePremium()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            MedicalPageObject medicalPage = new MedicalPageObject(getDriver());
            AboutMePageObject aboutMePage = new AboutMePageObject(getDriver());

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("medicalUser.username");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("medicalUser.password");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            menuPage.getbtnContinue().Click();

            // Enter in Zip Code Test Data
            aboutMePage.getclkAboutMe().Click();
            aboutMePage.getzipCode().Clear();
            string zc = getDataParser().extractData("medicalUser.zipcodeValidCase_1");
            TestContext.Progress.WriteLine("To get json string from array " + zc);
            aboutMePage.getzipCode().SendKeys(zc);
            aboutMePage.getnextBtn().Click();

            // Open New Tab.Go To Medical Step
            driver.SwitchTo().NewWindow(WindowType.Tab);
            driver.Url = getDataParser().extractData("medicalUser.MedicalStepURL");

            // Compare Rate on Medical Step 
            // Refactor Code to a loop for code quality
            string PremiumRate125 = getDataParser().extractData("medicalUser.PremierAmountCase_1_1");
            TestContext.Progress.WriteLine("To get json string from array" + PremiumRate125);
            string DeseretPremierRate = medicalPage.gettxtDeseretPremierRate().Text;
            Assert.That(PremiumRate125, Is.EqualTo(DeseretPremierRate));

            string SelectRate125 = getDataParser().extractData("medicalUser.SelectAmountCase_1_2");
            TestContext.Progress.WriteLine("To get json string from array" + PremiumRate125);
            string DeseretSelectRate125 = medicalPage.gettxtDeseretSelectRate().Text; 
            Assert.That(SelectRate125, Is.EqualTo(DeseretSelectRate125));

            string ValueRate125 = getDataParser().extractData("medicalUser.ValueAmountCase_1_3");
            TestContext.Progress.WriteLine("To get json string from array" + ValueRate125);
            string DeseretValueRate = medicalPage.gettxtDeseretValueRate().Text;
            Assert.That(ValueRate125, Is.EqualTo(DeseretValueRate));

            string ProtectRate125 = getDataParser().extractData("medicalUser.ProtectAmountCase_1_4");
            TestContext.Progress.WriteLine("To get json string from array" + ProtectRate125);
            string DeseretProtectRate = medicalPage.gettxtDeseretProtectRate().Text;
            Assert.That(ProtectRate125, Is.EqualTo(DeseretProtectRate));

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

            string usernameValid = getDataParser().extractData("medicalUser.username");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("medicalUser.password");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            menuPage.getbtnContinue().Click();
            string[] testData = getDataParser().extractDataArray("medicalUser.PlanChangeWithZipCodeChangeTestData");

            foreach (var testItem in testData)
            {
                TestContext.Progress.WriteLine("----------- " + testItem + " ---------------");
                // Search for element
                // change ZipCode for TC_0118
                driver.SwitchTo().NewWindow(WindowType.Tab);
                driver.Url = getDataParser().extractData("AboutMeStepURL");
                string zc = getDataParser().extractData("medicalUser." + testItem + ".zipcode");
                TestContext.Progress.WriteLine("Testing ZipCode: " + zc);
                aboutMePage.getzipCode().Clear();
                aboutMePage.getzipCode().SendKeys(zc);
                aboutMePage.getnextBtn().Click();

                // Open New Tab.Go To Medical Step
                driver.SwitchTo().NewWindow(WindowType.Tab);
                driver.Url = getDataParser().extractData("medicalUser.MedicalStepURL");

                
                string[] planList = getDataParser().extractDataArray("medicalUser." + testItem + ".plansToValidate");
                foreach (string item in planList)
                {
                    // Search for element
                    if (item == "WAIVE")
                    {
                        TestContext.Progress.WriteLine("Testing If Statement " + item);
                        try
                        {
                            string getValueToCompare = medicalPage.gettxtwaiveMedicalCoverage().Text;
                            // Compare Values for Plan Labels
                            Assert.That(getValueToCompare, Does.Contain(item).IgnoreCase);
                            TestContext.Progress.WriteLine(getValueToCompare + " contains " + item);
                        }
                        catch (Exception ex)
                        {
                            TestContext.Progress.WriteLine(ex);
                        }
                    }
                    else if (item == "PREMIER")
                    {
                        TestContext.Progress.WriteLine("Testing If Statement " + item);
                        try
                        {
                            string getValueToCompare = medicalPage.gettxtDeseretPremier().Text;
                            // Compare Values for Plan Labels
                            Assert.That(getValueToCompare, Does.Contain(item).IgnoreCase);
                            TestContext.Progress.WriteLine(getValueToCompare + " contains " + item);

                            // Compare Value for Plan Rate
                            string getRateAmount = medicalPage.gettxtDeseretPremierRate().Text;
                            string RateAmountExpected = getDataParser().extractData("medicalUser." + testItem + ".PREMIER");
                            TestContext.Progress.WriteLine("Testing if " + getRateAmount + " is equal to " + RateAmountExpected);
                            Assert.That(RateAmountExpected, Is.EqualTo(getRateAmount));
                        }
                        catch (Exception ex)
                        {
                            TestContext.Progress.WriteLine(ex);
                        }
                    }
                    else if (item == "SELECT")
                    {
                        TestContext.Progress.WriteLine("Testing If Statement " + item);
                        try
                        {
                            string getValueToCompare = medicalPage.gettxtDeseretSelect().Text;
                            // Compare Values for Plan Labels
                            Assert.That(getValueToCompare, Does.Contain(item).IgnoreCase);
                            TestContext.Progress.WriteLine(getValueToCompare + " contains " + item);

                            // Compare Value for Plan Rate
                            string getRateAmount = medicalPage.gettxtDeseretSelectRate().Text;
                            string RateAmountExpected = getDataParser().extractData("medicalUser." + testItem + ".SELECT");
                            TestContext.Progress.WriteLine("Testing if " + getRateAmount + " is equal to " + RateAmountExpected);
                            Assert.That(RateAmountExpected, Is.EqualTo(getRateAmount));


                        }
                        catch (Exception ex)
                        {
                            TestContext.Progress.WriteLine(ex);
                        }

                    }
                    else if (item == "VALUE")
                    {
                        TestContext.Progress.WriteLine("Testing If Statement " + item);
                        try
                        {
                            string getValueToCompare = medicalPage.gettxtDeseretValue().Text;
                            // Compare Values for Plan Labels
                            Assert.That(getValueToCompare, Does.Contain(item).IgnoreCase);
                            TestContext.Progress.WriteLine(getValueToCompare + " contains " + item);


                            // Compare Value for Plan Rate
                            string getRateAmount = medicalPage.gettxtDeseretValueRate().Text;
                            string RateAmountExpected = getDataParser().extractData("medicalUser." + testItem + ".VALUE");
                            TestContext.Progress.WriteLine("Testing if " + getRateAmount + " is equal to " + RateAmountExpected);
                            Assert.That(RateAmountExpected, Is.EqualTo(getRateAmount));
                        }
                        catch (Exception ex)
                        {
                            TestContext.Progress.WriteLine(ex);
                        }

                    }
                    else if (item == "PROTECT")
                    {
                        TestContext.Progress.WriteLine("Testing If Statement " + item);
                        try
                        {
                            string getValueToCompare = medicalPage.gettxtDeseretProtect().Text;
                            // Compare Values for Plan Labels
                            Assert.That(getValueToCompare, Does.Contain(item).IgnoreCase);
                            TestContext.Progress.WriteLine(getValueToCompare + " contains " + item);

                            // Compare Value for Plan Rate
                            string getRateAmount = medicalPage.gettxtDeseretProtectRate().Text;
                            string RateAmountExpected = getDataParser().extractData("medicalUser." + testItem + ".PROTECT");
                            TestContext.Progress.WriteLine("Testing if " + getRateAmount + " is equal to " + RateAmountExpected);
                            Assert.That(RateAmountExpected, Is.EqualTo(getRateAmount));
                        }
                        catch (Exception ex)
                        {
                            TestContext.Progress.WriteLine(ex);
                        }

                    }
                    else if (item == "CHOICE")
                    {
                        TestContext.Progress.WriteLine("Testing If Statement " + item);
                        try
                        {
                            string getValueToCompare = medicalPage.gettxtDeseretHawaiiChoice().Text;
                            // Compare Values for Plan Labels
                            Assert.That(getValueToCompare, Does.Contain(item).IgnoreCase);
                            TestContext.Progress.WriteLine(getValueToCompare + " contains " + item);

                            // Compare Value for Plan Rate
                            string getRateAmount = medicalPage.gettxtDeseretChoiceRate().Text;
                            string RateAmountExpected = getDataParser().extractData("medicalUser." + testItem + ".CHOICE");
                            TestContext.Progress.WriteLine("Testing if " + getRateAmount + " is equal to " + RateAmountExpected);
                            Assert.That(RateAmountExpected, Is.EqualTo(getRateAmount));
                        }
                        catch (Exception ex)
                        {
                            TestContext.Progress.WriteLine(ex);
                        }

                    }
                    else if (item == "KAISER")
                    {
                        TestContext.Progress.WriteLine("Testing If Statement " + item);
                        try
                        {
                            string getValueToCompare = medicalPage.gettxtDeseretHawaiiKaiser().Text;
                            // Compare Values for Plan Labels
                            Assert.That(getValueToCompare, Does.Contain(item).IgnoreCase);
                            TestContext.Progress.WriteLine(getValueToCompare + " contains " + item);


                            // Compare Value for Plan Rate
                            string getRateAmount = medicalPage.gettxtDeseretKaiserRate().Text;
                            string RateAmountExpected = getDataParser().extractData("medicalUser." + testItem + ".KAISER");
                            TestContext.Progress.WriteLine("Testing if " + getRateAmount + " is equal to " + RateAmountExpected);
                            Assert.That(RateAmountExpected, Is.EqualTo(getRateAmount));
                        }
                        catch (Exception ex)
                        {
                            TestContext.Progress.WriteLine(ex);
                        }

                    }
                    else if (item == "PERMANENTEE")
                    {
                        TestContext.Progress.WriteLine("Testing If Statement " + item);
                        try
                        {
                            string getValueToCompare = medicalPage.gettxtDeseretPermanentee().Text;
                            // Compare Values for Plan Labels
                            Assert.That(getValueToCompare, Does.Contain(item).IgnoreCase);
                            TestContext.Progress.WriteLine(getValueToCompare + " contains " + item);

                            // Compare Value for Plan Rate
                            string getRateAmount = medicalPage.gettxtDeseretPermanenteRate().Text;
                            string RateAmountExpected = getDataParser().extractData("medicalUser." + testItem + ".PERMANENTEE");
                            TestContext.Progress.WriteLine("Testing if " + getRateAmount + " is equal to " + RateAmountExpected);
                            Assert.That(RateAmountExpected, Is.EqualTo(getRateAmount));
                        }
                        catch (Exception ex)
                        {
                            TestContext.Progress.WriteLine(ex);
                        }

                    }

                }
            }

        }
    }
}