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
    public class DentalPageShould : Base
    {

        // -------------------------------------------------------------------------------
        /// <summary>
        /// Verify that the Menu button to navigate to Dental works.
        /// </summary>
        // -------------------------------------------------------------------------------
        [Test]
        //[Ignore("Ignore test")]
        public void VerifyDentalPageLaunching()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            MedicalPageObject medicalPage = new MedicalPageObject(getDriver());
            DentalPageObject dentalPage = new DentalPageObject(getDriver());

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("dentalUser.username");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("dentalUser.password");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();
            menuPage.getbtnContinue().Click();
            dentalPage.getclkDental().Click();

            string dentalPageHeading = dentalPage.getheadingText().Text;
            string dentalPageHeadingExpected = "Dental";
            Assert.That(dentalPageHeading, Is.EqualTo(dentalPageHeadingExpected));
        }


        // -------------------------------------------------------------------------------
        /// <summary>
        /// Verify that only one plan can be selected at a time. Check for text and color 
        /// change highlights. 
        /// Verify Error message when no plan is selected
        /// Covers Text Cases from TestCases Excel Document. Test Cases 266 - 267
        /// </summary>
        // -------------------------------------------------------------------------------
        [Test]
        //[Ignore("Ignore test")]
        public void VerifyDentalPageSelection()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            MedicalPageObject medicalPage = new MedicalPageObject(getDriver());
            AboutMePageObject aboutMePage = new AboutMePageObject(getDriver());
            DentalPageObject dentalPage = new DentalPageObject(getDriver());

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("medicalUser.username");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("medicalUser.password");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            menuPage.getbtnContinue().Click();

            dentalPage.getclkDental().Click();
            Thread.Sleep(2000);


            DentalPageObject dentalObject = new DentalPageObject(getDriver());
            Type typeDental = typeof(DentalPageObject);
            string dentalSelectionIndicatorMethodName = "getPlanSelectionIndicatorxPath";
            MethodInfo dentalSelectionIndicatorMethod = typeDental.GetMethod(dentalSelectionIndicatorMethodName);
            string dentalSelectionIndicatorMethodXPath = (string)dentalSelectionIndicatorMethod.Invoke(dentalObject, null);
            // test that the next button throws a error when no plan selected
            try
            {
                // find how many options have been selected
                var listOfSelectedElements = driver.FindElements(By.XPath(dentalSelectionIndicatorMethodXPath));
                int countOfSelected = 0;
                foreach ( var selectedElement in listOfSelectedElements )
                {
                    if( selectedElement.Text.Contains("CURRENT SELECTION") ) 
                    {

                        countOfSelected++;
                    }
                }
                // No plans are selected. Can test if next button error works.
                if (countOfSelected == 0)
                {
                    // Verify Error popup when no selection 
                    aboutMePage.getnextBtn().Click();
                    Thread.Sleep(3000);
                    string expectedNoSelectionErrorMessage = "Make a dental plan choice";
                    try
                    {
                        string noSelectionErrorMessage = dentalPage.getnoSelectionErrorMessage().Text;
                        Assert.That(noSelectionErrorMessage, Does.Contain(expectedNoSelectionErrorMessage).IgnoreCase);

                    }
                    catch (NoSuchElementException)
                    {
                        TestContext.Progress.WriteLine("No Error Message found");
                        Assert.Fail("No Error Message Found");
                    }

                }
                else 
                {
                    TestContext.Progress.WriteLine("Skipping test: Error message pops up when no option is selected and user hits next button. due to not being able to unselect an option ");
                }

            }
            catch (NoSuchElementException)
            {
                Assert.Fail("Web element not found. Not on correct page. Navigation to page failed.");
            }

            // Verify that only one option can be selected at a time.
            try
            {
                // get selection
                string dentalSelectionMethodName = "getPlanSelectionxPath";
                MethodInfo dentalSelectionMethod = typeDental.GetMethod(dentalSelectionMethodName);
                string dentalMethodXPath = (string)dentalSelectionMethod.Invoke(dentalObject, null);
                // find tr elements -> list of users in the grid
                var listOfDependentsSelectionOptionElements = driver.FindElements(By.XPath(dentalMethodXPath));
                // select every plan once
                foreach (var element in listOfDependentsSelectionOptionElements)
                {
                    Thread.Sleep(1000);
                    element.Click();
                }
                Thread.Sleep(1000);

                // Verify that only one plan is still selected after selecting every plan once.
                // find how many options have been selected
                var listOfSelectedElements = driver.FindElements(By.XPath(dentalSelectionIndicatorMethodXPath));
                int countOfSelected = 0;
                foreach (var selectedElement in listOfSelectedElements)
                {
                    if (selectedElement.Text.Contains("CURRENT SELECTION"))
                    {
                        countOfSelected++;
                    }
                }
                // No plans are selected. Can test if next button error works.
                if (countOfSelected == 1)
                {
                    TestContext.Progress.WriteLine("Only one plan selected. Based on Label -> Current selection <- elements ");
                }
                else
                {
                    TestContext.Progress.WriteLine("More  selected. Based on Label -> Current selection <- elements ");
                    Assert.Fail("More than One plan Selected. Count of Selected plans == " + countOfSelected);
                }

            }
            catch (NoSuchElementException)
            {
                Assert.Fail("Web element not found. Not on correct page. Navigation to page failed.");
            }

            try
            {
                // verify that a new color has been selected. same method there should only be one element with different class indicating selection
                string dentalSelectionIndicatorColorMethodName = "getPlanSelectionIndicatorColorxPath";
                MethodInfo dentalSelectionIndicatorColorMethod = typeDental.GetMethod(dentalSelectionIndicatorColorMethodName);
                string dentalSelectionIndicatorColorMethodXPath = (string)dentalSelectionIndicatorColorMethod.Invoke(dentalObject, null);

                // find how many options have been selected
                var listOfSelectorIndicatorColorElements = driver.FindElements(By.XPath(dentalSelectionIndicatorColorMethodXPath));
                int countOfColorChangedElements = 0;
                foreach (var selectedElement in listOfSelectorIndicatorColorElements)
                {
                    if (selectedElement.GetAttribute("Class").Contains("raisedRow selectedPlanColor1"))
                    {
                        countOfColorChangedElements++;
                    }
                }

                if (countOfColorChangedElements == 1)
                {
                    TestContext.Progress.WriteLine("Only one plan has color change");
                }
                else
                {
                    Assert.Fail("Multiple plans are selected and have change color");
                }
                Thread.Sleep(1000);
            }
            catch (NoSuchElementException)
            {
                Assert.Fail("Web element not found. Not on correct page. Navigation to page failed.");
            }

        }


        // -------------------------------------------------------------------------------
        /// <summary>
        /// Verify that when zipcode changes to states that have different plan options it changes
        /// selectable plans on the Dental page.
        /// Verify that when zipcode changes to states that have different plan options it changes
        /// The premium to the correct values.
        /// Covers Text Cases from TestCases Excel Document. Test Cases 230 - 265
        /// </summary>
        // -------------------------------------------------------------------------------
        [Test]
        //[Ignore("Ignore test")]
        public void VerifyDentalPagePlanWithZipCodes()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            AboutMePageObject aboutMePage = new AboutMePageObject(getDriver());
            MedicalPageObject medicalPage = new MedicalPageObject(getDriver());
            DependentsPageObject dependentsPage = new DependentsPageObject(getDriver());


            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("dentalUser.username");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("dentalUser.password");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            menuPage.getbtnContinue().Click();

            // Get current window handle
            string originalWindow = driver.CurrentWindowHandle;            

            string[] testData = getDataParser().extractDataArray("medicalUser.DentalPlanChangeWithZipCodeChangeTestData");

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
                    driver.Url = getDataParser().extractData("dentalUser.DentalStepURL");

                    loginPage.getbtnLogoutText().Click();
                    loginPage.getloginLink().Click();
                    loginPage.getusername().SendKeys(usernameValid);
                    loginPage.getpassword().SendKeys(passwordValid);
                    loginPage.getsubmit().Click();
                    Thread.Sleep(2000);
                    driver.Url = getDataParser().extractData("dentalUser.DentalStepURL");

                    Thread.Sleep(2000);
                    string[] planList = getDataParser().extractDataArray("medicalUser." + testItem + ".plansToValidate");
                    int itemIndex = 1;
                    TestContext.Progress.WriteLine("##################################################");

                    foreach (string item in planList)
                    {
                        if (itemIndex == 1)
                        {
                            TestContext.Progress.WriteLine(itemIndex.ToString() + " Testing for " + item);
                            string methodName = "get" + item + "CoveragexPath";
                            DentalPageObject dentalObject = new DentalPageObject(getDriver());
                            Type type = typeof(DentalPageObject);
                            MethodInfo methodComparePlan = type.GetMethod(methodName);
                            string methodXPath = (string)methodComparePlan.Invoke(dentalObject, null);

                            string getValueToCompare = driver.FindElement(By.XPath(methodXPath)).Text;
                            Assert.That(getValueToCompare, Does.Contain(item).IgnoreCase);
                            //TestContext.Progress.WriteLine(getValueToCompare + " contains " + item);
                        }
                        if (itemIndex >= 2)
                        {
                            // Compare Label with expected label
                            TestContext.Progress.WriteLine(itemIndex.ToString() + " Testing for " + item);
                            string methodName = "get" + item + "CoveragexPath";
                            DentalPageObject dentalObject = new DentalPageObject(getDriver());
                            Type type = typeof(DentalPageObject);
                            MethodInfo methodComparePlan = type.GetMethod(methodName);
                            string methodComparePlanXPath = (string)methodComparePlan.Invoke(dentalObject, null);
                            Thread.Sleep(1000);

                            string getPlanToCompare = driver.FindElement(By.XPath(methodComparePlanXPath)).Text;

                            Assert.That(getPlanToCompare, Does.Contain(item).IgnoreCase);

                            // Compare Price to Expected price
                            // Test Json Value
                            string expectedRate = getDataParser().extractData("medicalUser." + testItem + "." + item);
                            // Get xpath and Rate from medical step page
                            string rateMethodName = "get" + item + "CoverageRatexPath";
                            MethodInfo methodCompareRate = type.GetMethod(rateMethodName);
                            string methodCompareRateXPath = (string)methodCompareRate.Invoke(dentalObject, null);
                            string getRateToCompare = driver.FindElement(By.XPath(methodCompareRateXPath)).Text;

                            TestContext.Progress.WriteLine(getRateToCompare + "(Rate From Dental Step Page) should equal = " + expectedRate);
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
                else
                {
                    TestContext.Progress.WriteLine("----------------- Skipping test for" + testItem + "------------------------------");
                }
            }

        }


        // -------------------------------------------------------------------------------
        /// <summary>
        /// Verify that when a plan is selected and the next button is clicked that it takes the
        /// user to the Vision step.
        /// Covers Text Cases from TestCases Excel Document. Test Cases 268-270
        /// </summary>
        // -------------------------------------------------------------------------------
        [Test]
        //[Ignore("Ignore test")]
        public void VerifyDentalPageSelectionNextButton()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            AboutMePageObject aboutMePage = new AboutMePageObject(getDriver());
            MedicalPageObject medicalPage = new MedicalPageObject(getDriver());
            DependentsPageObject dependentsPage = new DependentsPageObject(getDriver());
            DentalPageObject dentalPage = new DentalPageObject(getDriver());
            VisionPageObject visionPage = new VisionPageObject(getDriver());
            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("medicalUser.username");
            loginPage.getusername().SendKeys(usernameValid);
            // log in
            string passwordValid = getDataParser().extractData("medicalUser.password");
            loginPage.getpassword().SendKeys(passwordValid);
            loginPage.getsubmit().Click();
            menuPage.getbtnContinue().Click();
            dentalPage.getclkDental().Click();

            // Get current window handle
            string originalWindow = driver.CurrentWindowHandle;
            string[] testData = getDataParser().extractDataArray("nextButtonDentalTestData.CaseList");
            TestContext.Progress.WriteLine("original window handle" + originalWindow);

            foreach (var testItem in testData)
            {

                TestContext.Progress.WriteLine("----------------- Doing Tests For " + testItem + "------------------------------");
                Thread.Sleep(1000);
                string execution = getDataParser().extractData("nextButtonDentalTestData." + testItem + ".execute");
                if (execution == "True")
                {
                    // get plans to compare with plans in new tab opened by link.
                    string[] planList = getDataParser().extractDataArray("nextButtonDentalTestData." + testItem + ".plansToValidate");
                    int itemIndex = 0;
                    TestContext.Progress.WriteLine("##################################################");
                    foreach (string item in planList)
                    {

                        // Loop Through and find index of Plan
                        string methodName = "getPlanLabelsxPath";
                        DentalPageObject dentalObject = new DentalPageObject(getDriver());
                        Type type = typeof(DentalPageObject);
                        MethodInfo methodComparePlan = type.GetMethod(methodName);
                        string methodXPath = (string)methodComparePlan.Invoke(dentalObject, null);
                        var listOfItems = driver.FindElements(By.XPath(methodXPath));
                        int indexCount = 0;
                        int planIndex = 0;
                        foreach (var element in listOfItems)
                        {
                            indexCount++;
                            TestContext.Progress.WriteLine("Looking for index of this plan: " + item);
                            TestContext.Progress.WriteLine(" Text of element: " + element.Text);
                            Thread.Sleep(1000);
                            if (element.Text.Contains(item))
                            {
                                planIndex = indexCount +1;
                                TestContext.Progress.WriteLine("Found Index of Item: " + planIndex.ToString());
                                break;
                            }
                        }
                        TestContext.Progress.WriteLine(planIndex.ToString() + " This is index that I need to find radio button");
                        Thread.Sleep(1000);

                        // Find Radio button to interact with based on matching index of plan and click
                        string dentalSelectionMethodName = "getPlanSelectionxPath";
                        MethodInfo dentalSelectionMethod = type.GetMethod(dentalSelectionMethodName);
                        string dentalMethodXPath = (string)dentalSelectionMethod.Invoke(dentalObject, null);
                        // find tr elements
                        var listOfRadioOptionElements = driver.FindElements(By.XPath(dentalMethodXPath));
                        // select radio button for plan based on index of plan label
                        indexCount = 0;
                        foreach (var element in listOfRadioOptionElements)
                        {
                            indexCount++;
                            Thread.Sleep(1000);
                            if (indexCount == planIndex)
                            {
                                element.Click();
                                break;
                            }
                        }
                        Thread.Sleep(1000);
                        // medical page -> click the next button to dental
                        // Verify that the Vision Step has been reached
                        aboutMePage.getnextBtn().Click();
                        Thread.Sleep(1000);
                        string visionPageHeading = visionPage.getheadingText().Text;
                        string visionPageHeadingExpected = "Vision (VSP)";
                        Assert.That(visionPageHeading, Is.EqualTo(visionPageHeadingExpected));
                        // Go back to medical step for next selection
                        aboutMePage.getprevBtn().Click();


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
                else
                {
                    TestContext.Progress.WriteLine("----------------- Skipping test for " + testItem + "------------------------------");
                }
            }
            // do WAIVE selection Label
            // need to refactor this later
            // Find Radio button to interact with based on matching index of plan and click
            DentalPageObject dentalObject2 = new DentalPageObject(getDriver());
            Type type2 = typeof(DentalPageObject);
            string SelectionMethodName = "getPlanSelectionxPath";
            MethodInfo SelectionMethod = type2.GetMethod(SelectionMethodName);
            string dentalMethodXPath2 = (string)SelectionMethod.Invoke(dentalObject2, null);
            // find tr elements
            var listOfRadioOptionElements2 = driver.FindElements(By.XPath(dentalMethodXPath2));
            listOfRadioOptionElements2[0].Click();
            Thread.Sleep(1000);
            // medical page -> click the next button to dental
            // Verify that the Vision Step has been reached
            aboutMePage.getnextBtn().Click();
            Thread.Sleep(1000);
            string visionPageHeading2 = visionPage.getheadingText().Text;
            string visionPageHeadingExpected2 = "Vision (VSP)";
            Assert.That(visionPageHeading2, Is.EqualTo(visionPageHeadingExpected2));
            // Go back to medical step for next selection
            aboutMePage.getprevBtn().Click();

        }

        // -------------------------------------------------------------------------------
        /// <summary>
        /// Verify that the plan benefit values in the selection grid are correct for the given zipcodes
        /// Covers Text Cases from TestCases Excel Document. Test Cases 517 - 544
        /// </summary>
        // -------------------------------------------------------------------------------
        [Test]
        //[Ignore("Ignore test")]
        public void VerifyDentalPageGridValues()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            AboutMePageObject aboutMePage = new AboutMePageObject(getDriver());
            MedicalPageObject medicalPage = new MedicalPageObject(getDriver());
            DependentsPageObject dependentsPage = new DependentsPageObject(getDriver());
            DentalPageObject dentalPage = new DentalPageObject(getDriver());
            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("medicalUser.username");
            loginPage.getusername().SendKeys(usernameValid);
            string passwordValid = getDataParser().extractData("medicalUser.password");
            loginPage.getpassword().SendKeys(passwordValid);
            loginPage.getsubmit().Click();
            menuPage.getbtnContinue().Click();


            // Get current window handle
            string originalWindow = driver.CurrentWindowHandle;
            string[] testData = getDataParser().extractDataArray("planDentalBenefitsTestData.CaseList");
            TestContext.Progress.WriteLine("original window handle" + originalWindow);

            foreach (var testItem in testData)
            {

                TestContext.Progress.WriteLine("----------------- Doing Tests For " + testItem + "------------------------------");
                Thread.Sleep(1000);
                string execution = getDataParser().extractData("planDentalBenefitsTestData." + testItem + ".execute");
                if (execution == "True")
                {
                    driver.SwitchTo().NewWindow(WindowType.Tab);
                    driver.Url = getDataParser().extractData("medicalUser.PersonalInfoStepURL");

                    //string originalWindow = driver.CurrentWindowHandle;
                    string zipCode = getDataParser().extractData("planDentalBenefitsTestData." + testItem + ".zipcode");
                    TestContext.Progress.WriteLine("ZipCode: " + zipCode);
                    aboutMePage.getzipCode().Clear();
                    aboutMePage.getzipCode().SendKeys(zipCode);
                    // about me page -> click the next button. This is a link?
                    aboutMePage.getnextBtn().Click();
                    Thread.Sleep(3000);
                    // dependent page -> click the next button
                    aboutMePage.getnextBtn().Click();
                    Thread.Sleep(1000);
                    // log in and log out to refresh information
                    loginPage.getbtnLogoutText().Click();
                    loginPage.getloginLink().Click();
                    loginPage.getusername().SendKeys(usernameValid);
                    loginPage.getpassword().SendKeys(passwordValid);
                    loginPage.getsubmit().Click();
                    Thread.Sleep(1000);
                    menuPage.getbtnContinue().Click();
                    Thread.Sleep(1000);
                    // go to dental page
                    try
                    {
                        dentalPage.getclkDental().Click();
                    }
                    catch (NoSuchElementException)
                    {
                        driver.Url = getDataParser().extractData("dentalUser.DentalStepURL");
                    }
                    // get plans to compare with plans in new tab opened by link.
                    string[] planList = getDataParser().extractDataArray("planDentalBenefitsTestData." + testItem + ".plansToValidate");
                    int itemIndex = 1;
                    TestContext.Progress.WriteLine("##################################################");

                    foreach (string item in planList)
                    {
                        TestContext.Progress.WriteLine("Testing Values for " + item);
                        // Extract Json Data data
                        string ExpectedAnnualMax = getDataParser().extractData("planDentalBenefitsTestData." + testItem + "." + item + ".annualMax");
                        string ExpectedHospitalizationAndAnesthesia = getDataParser().extractData("planDentalBenefitsTestData." + testItem + "." + item + ".hospitalizationAndAnesthesia");
                        string ExpectedOralSurgery = getDataParser().extractData("planDentalBenefitsTestData." + testItem + "." + item + ".oralSurgery");
                        string ExpectedOrthodontics = getDataParser().extractData("planDentalBenefitsTestData." + testItem + "." + item + ".orthodontics");
                        string ExpectedPreventiveCarePart1 = getDataParser().extractData("planDentalBenefitsTestData." + testItem + "." + item + ".preventiveCarePart1");
                        string ExpectedPreventiveCarePart2 = getDataParser().extractData("planDentalBenefitsTestData." + testItem + "." + item + ".preventiveCarePart2");
                        string ExpectedRestorativeAndProsthodonticCarePart1 = getDataParser().extractData("planDentalBenefitsTestData." + testItem + "." + item + ".restorativeAndProsthodonticCarePart1");
                        string ExpectedRestorativeAndProsthodonticCarePart2 = getDataParser().extractData("planDentalBenefitsTestData." + testItem + "." + item + ".restorativeAndProsthodonticCarePart2");
                        string ExpectedSealants = getDataParser().extractData("planDentalBenefitsTestData." + testItem + "." + item + ".sealants");

                        //---------------------------------------------------------------------------------------------------------------
                        // trying more systematic approach
                        // Find Index position of Matching Plan and build the xpath. reusing medical plan xpath cause its the same as dental.
                        string dentalPlanMethodName = "getPlanLabelsxPath";
                        DentalPageObject dentalObject = new DentalPageObject(getDriver());
                        Type typeDental = typeof(DentalPageObject);
                        MethodInfo dentalPlanMethod = typeDental.GetMethod(dentalPlanMethodName);
                        string dentalPlanMethodXPath = (string)dentalPlanMethod.Invoke(dentalObject, null);
                        // find tr elements -> list of users in the grid
                        var listOfPlanOptionElements = driver.FindElements(By.XPath(dentalPlanMethodXPath));
                        // select every plan once
                        int indexCount = 0;
                        int planIndex = 0;
                        foreach (var element in listOfPlanOptionElements)
                        {
                            indexCount++;
                            TestContext.Progress.WriteLine("Looking for index of this plan: " + item);
                            TestContext.Progress.WriteLine(" Text of element: " + element.Text);
                            Thread.Sleep(1000);
                            if (element.Text.Contains(item))
                            {
                                // Needed to add one to index. xpath is not picking up the waive plan element.
                                planIndex = indexCount + 1;
                                TestContext.Progress.WriteLine("Found Index of Item: " + planIndex.ToString());
                                break;
                            }
                        }


                        TestContext.Progress.WriteLine(planIndex.ToString() + " This is index that I will use to extract Plan Grid Data");

                        // Hard coded row column positions. taken from Dental step page selection table.
                        string[] tableRows = ["7", "8", "9", "10", "11", "12", "13"];

                        // get xpath of dental grid
                        string DentalPlanGridMethodName = "getDentalGridValuesxPath";
                        MethodInfo dentalGridPlanMethod = typeDental.GetMethod(DentalPlanGridMethodName);
                        string dentalGridMethodXPath = (string)dentalGridPlanMethod.Invoke(dentalObject, null);
                        string[] splitXpath = dentalGridMethodXPath.Split("+");
                        // declaring variables
                        string annualMaxValuesMethodXPath = "";
                        string hospitalizationAndAnesthesiaValuesMethodXPath = "";
                        string oralSurgeryValuesMethodXPath = "";
                        string orthodonticsValuesMethodXPath = "";
                        string preventiveCareValuesMethodXPath = "";
                        string restorativeAndProsthodonticCareValuesMethodXPath = "";
                        string sealantsValuesMethodXPath = "";
                        // using grid xpath and tableRow and planIndex location. create xpaths to extract the table values.
                        foreach (var row in tableRows)
                        {
                            if (row == "7")
                            {
                                annualMaxValuesMethodXPath = splitXpath[0] + row + splitXpath[2] + planIndex + splitXpath[4];
                            }
                            else if (row == "8")
                            {
                                hospitalizationAndAnesthesiaValuesMethodXPath = splitXpath[0] + row + splitXpath[2] + planIndex + splitXpath[4];
                            }
                            else if (row == "9")
                            {
                                oralSurgeryValuesMethodXPath = splitXpath[0] + row + splitXpath[2] + planIndex + splitXpath[4];
                            }
                            else if (row == "10")
                            {
                                orthodonticsValuesMethodXPath = splitXpath[0] + row + splitXpath[2] + planIndex + splitXpath[4];
                            }
                            else if (row == "11")
                            {
                                preventiveCareValuesMethodXPath = splitXpath[0] + row + splitXpath[2] + planIndex + splitXpath[4];
                            }
                            else if (row == "12")
                            {
                                restorativeAndProsthodonticCareValuesMethodXPath = splitXpath[0] + row + splitXpath[2] + planIndex + splitXpath[4];
                            }
                            else if (row == "13")
                            {
                                sealantsValuesMethodXPath = splitXpath[0] + row + splitXpath[2] + planIndex + splitXpath[4];
                            }

                        }

                        // Extract data from Dental step selection table
                        string annualMaxVisitValues = driver.FindElement(By.XPath(annualMaxValuesMethodXPath)).Text;
                        string hospitalizationAndAnesthesiaValues = driver.FindElement(By.XPath(hospitalizationAndAnesthesiaValuesMethodXPath)).Text;
                        string oralSurgeryValues = driver.FindElement(By.XPath(oralSurgeryValuesMethodXPath)).Text;
                        string orthodonticsValues = driver.FindElement(By.XPath(orthodonticsValuesMethodXPath)).Text;
                        string preventiveCareValues = driver.FindElement(By.XPath(preventiveCareValuesMethodXPath)).Text;
                        string restorativeAndProsthodonticCareValues = driver.FindElement(By.XPath(restorativeAndProsthodonticCareValuesMethodXPath)).Text;
                        string sealantsValues = driver.FindElement(By.XPath(sealantsValuesMethodXPath)).Text;

                        TestContext.Progress.WriteLine("------- Value of extracted values below ------ ");
                        TestContext.Progress.WriteLine(annualMaxVisitValues);
                        TestContext.Progress.WriteLine(hospitalizationAndAnesthesiaValues);
                        TestContext.Progress.WriteLine(oralSurgeryValues);
                        TestContext.Progress.WriteLine(orthodonticsValues);
                        TestContext.Progress.WriteLine(preventiveCareValues);
                        TestContext.Progress.WriteLine(restorativeAndProsthodonticCareValues);
                        TestContext.Progress.WriteLine(sealantsValues);



                        // Compare Values for  Contracted and non-contract Values In Grid with expected values
                        Assert.That(annualMaxVisitValues, Does.Contain(ExpectedAnnualMax).IgnoreCase);
                        Assert.That(hospitalizationAndAnesthesiaValues, Does.Contain(ExpectedHospitalizationAndAnesthesia).IgnoreCase);
                        Assert.That(oralSurgeryValues, Does.Contain(ExpectedOralSurgery).IgnoreCase);
                        Assert.That(orthodonticsValues, Does.Contain(ExpectedOrthodontics).IgnoreCase);
                        Assert.That(preventiveCareValues, Does.Contain(ExpectedPreventiveCarePart1).IgnoreCase);
                        Assert.That(preventiveCareValues, Does.Contain(ExpectedPreventiveCarePart2).IgnoreCase);
                        Assert.That(restorativeAndProsthodonticCareValues, Does.Contain(ExpectedRestorativeAndProsthodonticCarePart1).IgnoreCase);
                        Assert.That(restorativeAndProsthodonticCareValues, Does.Contain(ExpectedRestorativeAndProsthodonticCarePart2).IgnoreCase);
                        Assert.That(sealantsValues, Does.Contain(ExpectedSealants).IgnoreCase);


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
                else
                {
                    TestContext.Progress.WriteLine("----------------- Skipping test for" + testItem + "------------------------------");
                }
            }

        }
    }    
}