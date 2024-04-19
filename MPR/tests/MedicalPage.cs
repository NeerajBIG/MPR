using MPR.pageObjects;
using MPR.utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V121.Page;
using OpenQA.Selenium.Interactions;
using System;
using System.Reflection;
using System.Reflection.Metadata;
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
            string zipCode = getDataParser().extractData("medicalUser.zipcodeValidCase_1");
            TestContext.Progress.WriteLine("To get json string from array " + zipCode);
            aboutMePage.getzipCode().SendKeys(zipCode);
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
        public void VerifyMedicalPagePlanWithZipCodes()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            AboutMePageObject aboutMePage = new AboutMePageObject(getDriver());
            MedicalPageObject medicalPage = new MedicalPageObject(getDriver());
            DependentsPageObject dependentsPage = new DependentsPageObject(getDriver());
            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("medicalUser.username");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("medicalUser.password");
            loginPage.getpassword().SendKeys(passwordValid);
            loginPage.getsubmit().Click();
            menuPage.getbtnContinue().Click();

            // Need to verify step -> which step we are on.
            // start the loop -> complete steps one by one. up to the step we are testing. 
            // get class name


            // Get current window handle
            string originalWindow = driver.CurrentWindowHandle;
            //medicalPage.getcomparePlanlink().Click();            

            string[] testData = getDataParser().extractDataArray("medicalUser.PlanChangeWithZipCodeChangeTestData");

            foreach (var testItem in testData)
            {

                TestContext.Progress.WriteLine("----------------- Doing Tests For " + testItem + "------------------------------");
                Thread.Sleep(1000);
                string execution = getDataParser().extractData("medicalUser." + testItem + ".execute");
                if (execution == "True")
                {
                    driver.SwitchTo().NewWindow(WindowType.Tab);
                    driver.Url = getDataParser().extractData("medicalUser.PersonalInfoStepURL");

                    //string originalWindow = driver.CurrentWindowHandle;
                    string zipCode = getDataParser().extractData("medicalUser." + testItem + ".zipcode");
                    TestContext.Progress.WriteLine("ZipCode: " + zipCode);
                    aboutMePage.getzipCode().Clear();
                    aboutMePage.getzipCode().SendKeys(zipCode);
                    //Thread.Sleep(1000);
                    aboutMePage.getnextBtn().Click();

                    // on dependent page.
                    // ---------------------- Need to count dependents. decide whether to delete, add more, or do nothing if dependents are equal.
                    string dependentMethodName = "getdependentsxPath";
                    DependentsPageObject dependentObject = new DependentsPageObject(getDriver());
                    Type typeDependent = typeof(DependentsPageObject);
                    MethodInfo dependentMethod = typeDependent.GetMethod(dependentMethodName);
                    string dependentMethodXPath = (string)dependentMethod.Invoke(dependentObject, null);

                    // find tr elements -> list of users in the grid
                    var originalListOfDependents = driver.FindElements(By.XPath(dependentMethodXPath));
                    // count users
                    int count = originalListOfDependents.Count;
                    TestContext.Progress.WriteLine("Number of dependents in grid: " + count);
                    string peopleOnPlan = getDataParser().extractData("medicalUser." + testItem + ".PeopleOnPlan");
                    if (Int32.Parse(peopleOnPlan) == count)
                    {
                        TestContext.Progress.WriteLine("People on plan equal dependents count");

                    }
                    else if (Int32.Parse(peopleOnPlan) < count)
                    {
                        //delete users until it matchs people on plan count
                        while (count > Int32.Parse(peopleOnPlan))
                        {
                            try
                            {
                                TestContext.Progress.WriteLine("Delete user");
                                dependentsPage.getdeleteBtn().Click();
                                Thread.Sleep(2000);
                                dependentsPage.getconfirmDeletionBtn().Click();
                                Thread.Sleep(2000);
                                var currentListOfDependents = driver.FindElements(By.XPath(dependentMethodXPath));
                                count = currentListOfDependents.Count;
                                Thread.Sleep(2000);
                            }
                            catch (NoSuchElementException)
                            {
                                TestContext.Progress.WriteLine("Could not find element");
                                break;
                            }
                        }
                    }
                    else if (Int32.Parse(peopleOnPlan) > count)
                    {
                        //Need to add users until peopleOnPlan = count
                        while (Int32.Parse(peopleOnPlan) > count)
                        {

                            Random rnd = new Random();
                            int num = rnd.Next();
                            TestContext.Progress.WriteLine("Adding additionalDependent with same data and random number attached " + num.ToString());
                            // extract data from Json
                            string firstName = getDataParser().extractData("dependentUser.additionalDependant1.firstName") + num.ToString();
                            string lastName = getDataParser().extractData("dependentUser.additionalDependant1.lastName");
                            string ssn = num.ToString().Remove(9);
                            string birthDate = getDataParser().extractData("dependentUser.additionalDependant1.birthDate");
                            string gender = getDataParser().extractData("dependentUser.additionalDependant1.Gender");

                            try
                            {
                                // click add dependent button
                                dependentsPage.getaddDependentBtn().Click();
                                Thread.Sleep(2000);

                                // Send extracted data to fields
                                dependentsPage.getfirstNameField().SendKeys(firstName);
                                dependentsPage.getlastNameField().SendKeys(lastName);
                                dependentsPage.getSSNField().SendKeys(ssn);
                                dependentsPage.getbirthDateField().SendKeys(birthDate);

                                if (gender == "Male")
                                {
                                    dependentsPage.getradioBtnMale().Click();
                                }
                                else
                                {
                                    dependentsPage.getradioBtnFemale().Click();
                                }
                                dependentsPage.getrelationshipDropdown().Click();
                                dependentsPage.getrelationshipDropdown().SendKeys(Keys.ArrowDown);
                                dependentsPage.getrelationshipDropdown().SendKeys(Keys.ArrowDown);
                                dependentsPage.getrelationshipDropdown().SendKeys(Keys.Enter);

                                //continue btn
                                dependentsPage.getcontinuePopUpBtn().Click();
                                Thread.Sleep(3000);

                                // continue entering information
                                dependentsPage.getradioBtnMarriedNo().Click();
                                dependentsPage.getradioBtnEnrolledYes().Click();
                                dependentsPage.getradioBtnOtherCoverageNo().Click();

                                // second continue button click
                                dependentsPage.getcontinuePopUpBtn().Click();
                                Thread.Sleep(2000);
                                var currentListOfDependents = driver.FindElements(By.XPath(dependentMethodXPath));
                                count = currentListOfDependents.Count;

                            }
                            catch (NoSuchElementException ex)
                            {
                                TestContext.Progress.WriteLine("Could not find element");
                                TestContext.Progress.WriteLine(ex);
                                break;
                            }

                        }

                    }
                    Thread.Sleep(3000);
                    // Open New Tab.Go To Medical Step
                    driver.SwitchTo().NewWindow(WindowType.Tab);
                    driver.Url = getDataParser().extractData("medicalUser.MedicalStepURL");

                    loginPage.getbtnLogoutText().Click();
                    loginPage.getloginLink().Click();
                    loginPage.getusername().SendKeys(usernameValid);
                    loginPage.getpassword().SendKeys(passwordValid);
                    loginPage.getsubmit().Click();
                    Thread.Sleep(2000);
                    driver.Url = getDataParser().extractData("medicalUser.MedicalStepURL");

                    Thread.Sleep(2000);
                    string[] planList = getDataParser().extractDataArray("medicalUser." + testItem + ".plansToValidate");
                    int itemIndex = 1;
                    TestContext.Progress.WriteLine("##################################################");

                    foreach (string item in planList)
                    {
                        if (itemIndex == 1)
                        {
                            TestContext.Progress.WriteLine(itemIndex.ToString() + " Testing for " + item);
                            string methodName = "get" + item + "medicalCoveragexPath";
                            MedicalPageObject medicalStepObject = new MedicalPageObject(getDriver());
                            Type type = typeof(MedicalPageObject);
                            MethodInfo method = type.GetMethod(methodName);
                            string methodXPath = (string)method.Invoke(medicalStepObject, null);

                            string getValueToCompare = driver.FindElement(By.XPath(methodXPath)).Text;
                            Assert.That(getValueToCompare, Does.Contain(item).IgnoreCase);
                        }
                        if (itemIndex >= 2)
                        {
                            // Compare Label with expected label
                            TestContext.Progress.WriteLine(itemIndex.ToString() + " Testing for " + item);
                            string methodName = "get" + item + "medicalCoveragexPath";
                            MedicalPageObject medicalStepObject = new MedicalPageObject(getDriver());
                            Type type = typeof(MedicalPageObject);
                            MethodInfo methodComparePlan = type.GetMethod(methodName);
                            string methodComparePlanXPath = (string)methodComparePlan.Invoke(medicalStepObject, null);
                            Thread.Sleep(1000);

                            string getPlanToCompare = driver.FindElement(By.XPath(methodComparePlanXPath)).Text;

                            Assert.That(getPlanToCompare, Does.Contain(item).IgnoreCase);

                            // Compare Price to Expected price
                            // Test Json Value
                            string expectedRate = getDataParser().extractData("medicalUser." + testItem + "." + item);
                            // Get xpath and Rate from medical step page
                            string rateMethodName = "get" + item + "medicalCoverageRatexPath";
                            MethodInfo methodCompareRate = type.GetMethod(rateMethodName);
                            string methodCompareRateXPath = (string)methodCompareRate.Invoke(medicalStepObject, null);
                            string getRateToCompare = driver.FindElement(By.XPath(methodCompareRateXPath)).Text;

                            TestContext.Progress.WriteLine(getRateToCompare + "(Rate From Medical Step Page) should equal = " + expectedRate);
                            // compare expected rate to JSON Data rate.
                            Assert.That(getRateToCompare, Is.EqualTo(expectedRate));



                        }
                        itemIndex++;
                    }
                    foreach (string window in driver.WindowHandles)
                    {
                        if (window != originalWindow)
                        {
                            driver.SwitchTo().Window(window);
                            Thread.Sleep(1000);
                            if (driver.WindowHandles.Count != 2)
                            {
                                driver.Close();
                            }
                            else if (driver.WindowHandles.Count == 2)
                            {
                                driver.Close();
                                driver.SwitchTo().Window(originalWindow);
                            }
                            Thread.Sleep(1000);
                        }
                        else
                        {
                        }
                    }
                    driver.Navigate().Refresh();
                    TestContext.Progress.WriteLine(" ");
                }
                else {
                    TestContext.Progress.WriteLine("----------------- Skipping test for" + testItem + "------------------------------");
                }
            }

        }
    }    
}