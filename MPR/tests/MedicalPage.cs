using MPR.pageObjects;
using MPR.utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V121.Page;
using OpenQA.Selenium.Interactions;
using System;
using System.Reactive.Joins;
using System.Reflection;
using System.Reflection.Metadata;
using System.Security.Policy;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

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
        public void VerifyMedicalPageSelection()
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

            medicalPage.getclkMedical().Click();
            Thread.Sleep(2000);

            // Verify Error popup when no selection 
            aboutMePage.getnextBtn().Click();
            Thread.Sleep(3000);
            string expectedNoSelectionErrorMessage = "Make a medical plan choice";
            try
            {
                string noSelectionErrorMessage = medicalPage.getnoSelectionErrorMessage().Text;
                Assert.That(noSelectionErrorMessage, Does.Contain(expectedNoSelectionErrorMessage).IgnoreCase);

            }
            catch (NoSuchElementException)
            {
                TestContext.Progress.WriteLine("No Error Message found");
                Assert.Fail("No Error Message Found");
            }

            // Verify that only one option can be selected at a time.
            // get selection
            string medicalSelectionMethodName = "getPlanSelectionxPath";
            MedicalPageObject medicalObject = new MedicalPageObject(getDriver());
            Type typeMedical = typeof(MedicalPageObject);
            MethodInfo medicalSelectionMethod = typeMedical.GetMethod(medicalSelectionMethodName);
            string medicalMethodXPath = (string)medicalSelectionMethod.Invoke(medicalObject, null);
            // find tr elements -> list of users in the grid
            var listOfDependentsSelectionOptionElements = driver.FindElements(By.XPath(medicalMethodXPath));
            // select every plan once
            foreach (var element in listOfDependentsSelectionOptionElements) {
                Thread.Sleep(1000);
                element.Click();
            }
            Thread.Sleep(1000);
            // Verify that only one plan is still selected after selecting every plan once.
            string medicalSelectionIndicatorMethodName = "getPlanSelectionIndicatorxPath";
            MethodInfo medicalSelectionIndicatorMethod = typeMedical.GetMethod(medicalSelectionIndicatorMethodName);
            string medicalSelectionIndicatorMethodXPath = (string)medicalSelectionIndicatorMethod.Invoke(medicalObject, null);

            // find how many options have been selected
            var listOfSelectorIndicatorElements = driver.FindElements(By.XPath(medicalSelectionIndicatorMethodXPath));
            int count = listOfSelectorIndicatorElements.Count;
            if (count == 1)
            {
                TestContext.Progress.WriteLine("Only one plan selected");
            }
            else {
                Assert.Fail("Multiple plans are selected");
            }
            Thread.Sleep(1000);
            // verify that a new color has been selected. same method there should only be one class with the changed color
            string medicalSelectionIndicatorColorMethodName = "getPlanSelectionIndicatorColorxPath";
            MethodInfo medicalSelectionIndicatorColorMethod = typeMedical.GetMethod(medicalSelectionIndicatorColorMethodName);
            string medicalSelectionIndicatorColorMethodXPath = (string)medicalSelectionIndicatorColorMethod.Invoke(medicalObject, null);

            // find how many options have been selected
            var listOfSelectorIndicatorColorElements = driver.FindElements(By.XPath(medicalSelectionIndicatorColorMethodXPath));
            int countColorElements = listOfSelectorIndicatorColorElements.Count;
            if (countColorElements == 1)
            {
                TestContext.Progress.WriteLine("Only one plan has color change");
            }
            else
            {
                Assert.Fail("Multiple plans are selected and have change color");
            }
            Thread.Sleep(1000);

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
            TestContext.Progress.WriteLine(originalWindow);
            medicalPage.getcomparePlanlink().Click();

            foreach (string window in driver.WindowHandles)
            {
                TestContext.Progress.WriteLine(window);
                if (originalWindow != window)
                {
                    driver.SwitchTo().Window(window);
                    break;
                }
            }
            Thread.Sleep(3000);


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
                    // log in and log out to refresh information
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


        [Test]
        //[Ignore("Ignore test")]
        public void VerifyFullComparisonLinkPagePlanWithZipCodes()
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
            string[] testData = getDataParser().extractDataArray("fullPlanComparisonTestData.CaseList");
            TestContext.Progress.WriteLine("original window handle" +  originalWindow);

            foreach (var testItem in testData)
            {

                TestContext.Progress.WriteLine("----------------- Doing Tests For " + testItem + "------------------------------");
                Thread.Sleep(1000);
                string execution = getDataParser().extractData("fullPlanComparisonTestData." + testItem + ".execute");
                if (execution == "True")
                {
                    driver.SwitchTo().NewWindow(WindowType.Tab);
                    driver.Url = getDataParser().extractData("medicalUser.PersonalInfoStepURL");

                    //string originalWindow = driver.CurrentWindowHandle;
                    string zipCode = getDataParser().extractData("medicalUser." + testItem + ".zipcode");
                    TestContext.Progress.WriteLine("ZipCode: " + zipCode);
                    aboutMePage.getzipCode().Clear();
                    aboutMePage.getzipCode().SendKeys(zipCode);
                    // about me page -> click the next button. This is a link?
                    aboutMePage.getnextBtn().Click();
                    Thread.Sleep(3000);
                    // dependent page -> click the next button
                    aboutMePage.getnextBtn().Click();
                    Thread.Sleep(1000);
                    // medical page -> click link
                    string secondWindow = driver.CurrentWindowHandle;
                    medicalPage.getfullPlanComparisonlink().Click();

                    // switch to new tab that the link opened
                    foreach (string window in driver.WindowHandles)
                    {
                        if (originalWindow != window & secondWindow != window)
                        {
                            driver.SwitchTo().Window(window);
                            break;
                        }
                    }
                    // compare new tab zip code with zipcode entered on about me page.
                    string newLinkZipcode = medicalPage.getzipCode().GetAttribute("Value");
                    TestContext.Progress.WriteLine(newLinkZipcode + " Equal to " + zipCode);
                    Assert.That(newLinkZipcode, Does.Contain(zipCode).IgnoreCase);

                    // Check that the dropdown has medical selected.
                    string expectedPlanTypeSelection = "Medical";
                    string newLinkDropdownSelection = medicalPage.getplanTypeSelection().Text;
                    Assert.That(expectedPlanTypeSelection, Does.Contain(newLinkDropdownSelection).IgnoreCase);

                    // get plans to compare with plans in new tab opened by link.
                    string[] planList = getDataParser().extractDataArray("fullPlanComparisonTestData." + testItem + ".plansToValidate");
                    int itemIndex = 1;
                    TestContext.Progress.WriteLine("##################################################");

                    foreach (string item in planList)
                    {

                        // Compare Label with expected label
                        TestContext.Progress.WriteLine(itemIndex.ToString() + " Testing for " + item);
                        string methodName = "get" + item + "ComparisonLinkCoveragexPath";
                        MedicalPageObject medicalStepObject = new MedicalPageObject(getDriver());
                        Type type = typeof(MedicalPageObject);
                        MethodInfo methodComparePlan = type.GetMethod(methodName);
                        string methodComparePlanXPath = (string)methodComparePlan.Invoke(medicalStepObject, null);
                        Thread.Sleep(1000);

                        string getPlanToCompare = driver.FindElement(By.XPath(methodComparePlanXPath)).Text;

                        Assert.That(getPlanToCompare, Does.Contain(item).IgnoreCase);
                    }
                    // Close the new window, if that window no more required
                    driver.Close();

                    // Switch back to original browser (first window)
                    driver.SwitchTo().Window(secondWindow);

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


        [Test]
        //[Ignore("Ignore test")]
        public void VerifyMedicalPageSelectionNextButton()
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

            // log in
            string passwordValid = getDataParser().extractData("medicalUser.password");
            loginPage.getpassword().SendKeys(passwordValid);
            loginPage.getsubmit().Click();
            menuPage.getbtnContinue().Click();

            // Get current window handle
            string originalWindow = driver.CurrentWindowHandle;
            string[] testData = getDataParser().extractDataArray("nextButtonTestData.CaseList");
            TestContext.Progress.WriteLine("original window handle" + originalWindow);

            foreach (var testItem in testData)
            {

                TestContext.Progress.WriteLine("----------------- Doing Tests For " + testItem + "------------------------------");
                Thread.Sleep(1000);
                string execution = getDataParser().extractData("nextButtonTestData." + testItem + ".execute");
                if (execution == "True")
                {
                    driver.SwitchTo().NewWindow(WindowType.Tab);
                    driver.Url = getDataParser().extractData("medicalUser.PersonalInfoStepURL");
                    // Enter in Zipcode on about me page
                    string zipCode = getDataParser().extractData("nextButtonTestData." + testItem + ".zipcode");
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
                    Thread.Sleep(2000);
                    driver.Url = getDataParser().extractData("medicalUser.MedicalStepURL");
                    Thread.Sleep(2000);

                    // get plans to compare with plans in new tab opened by link.
                    string[] planList = getDataParser().extractDataArray("nextButtonTestData." + testItem + ".plansToValidate");
                    int itemIndex = 1;
                    TestContext.Progress.WriteLine("##################################################");

                    foreach (string item in planList)
                    {
                        // Find Index position of Matching Plan
                        string medicalPlanMethodName = "getPlanLabelsxPath";
                        MedicalPageObject medicalObject = new MedicalPageObject(getDriver());
                        Type typeMedical = typeof(MedicalPageObject);
                        MethodInfo medicalPlanMethod = typeMedical.GetMethod(medicalPlanMethodName);
                        string medicalPlanMethodXPath = (string)medicalPlanMethod.Invoke(medicalObject, null);
                        // find tr elements -> list of users in the grid
                        var listOfPlanOptionElements = driver.FindElements(By.XPath(medicalPlanMethodXPath));
                        // select every plan once
                        int indexCount = 0;
                        int planIndex = 0;
                        foreach (var element in listOfPlanOptionElements)
                        {
                            indexCount++;
                            TestContext.Progress.WriteLine("Looking for index of this plan: " + item);
                            TestContext.Progress.WriteLine(" Text of element: " +element.Text);
                            Thread.Sleep(1000);
                            if (element.Text.Contains(item)) { 
                                planIndex = indexCount;
                                TestContext.Progress.WriteLine("Found Index of Item: " + planIndex.ToString());
                                break;
                            }
                        }

                        TestContext.Progress.WriteLine(planIndex.ToString() + " This is index that I need to find radio button");
                        // Find Radio button to interact with based on matching index of plan and click
                        string medicalSelectionMethodName = "getPlanSelectionxPath";
                        MethodInfo medicalSelectionMethod = typeMedical.GetMethod(medicalSelectionMethodName);
                        string medicalMethodXPath = (string)medicalSelectionMethod.Invoke(medicalObject, null);
                        // find tr elements
                        var listOfRadioOptionElements = driver.FindElements(By.XPath(medicalMethodXPath));
                        // select radio button for plan based on index of plan label
                        indexCount = 0;
                        foreach (var element in listOfRadioOptionElements)
                        {
                            indexCount++;
                            Thread.Sleep(1000);
                            if (indexCount == planIndex) {
                                element.Click();
                                break;
                            }
                        }
                        Thread.Sleep(1000);
                        // medical page -> click the next button to dental
                        // Verify that the Dental Step has been reached
                        aboutMePage.getnextBtn().Click();
                        Thread.Sleep(1000);
                        string dentalPageHeading = dentalPage.getheadingText().Text;
                        string dentalPageHeadingExpected = "Dental";
                        Assert.That(dentalPageHeading, Is.EqualTo(dentalPageHeadingExpected));
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

        }

        [Test]
        //[Ignore("Ignore test")]
        public void VerifyMedicalPageGridValues()
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
            string[] testData = getDataParser().extractDataArray("planBenefitsTestData.CaseList");
            TestContext.Progress.WriteLine("original window handle" + originalWindow);

            foreach (var testItem in testData)
            {

                TestContext.Progress.WriteLine("----------------- Doing Tests For " + testItem + "------------------------------");
                Thread.Sleep(1000);
                string execution = getDataParser().extractData("planBenefitsTestData." + testItem + ".execute");
                if (execution == "True")
                {
                    driver.SwitchTo().NewWindow(WindowType.Tab);
                    driver.Url = getDataParser().extractData("medicalUser.PersonalInfoStepURL");

                    //string originalWindow = driver.CurrentWindowHandle;
                    string zipCode = getDataParser().extractData("planBenefitsTestData." + testItem + ".zipcode");
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
                    Thread.Sleep(2000);
                    driver.Url = getDataParser().extractData("medicalUser.MedicalStepURL");
                    Thread.Sleep(2000);


                    // get plans to compare with plans in new tab opened by link.
                    string[] planList = getDataParser().extractDataArray("planBenefitsTestData." + testItem + ".plansToValidate");
                    int itemIndex = 1;
                    TestContext.Progress.WriteLine("##################################################");

                    foreach (string item in planList)
                    {
                        TestContext.Progress.WriteLine("Testing Values for " + item);
                        // Extract Json Data contract data
                        string contractExpectedOfficeVisitPrimaryCare = getDataParser().extractData("planBenefitsTestData." + testItem + "." + item + ".contracted.officeVisitPrimaryCare");
                        string contractExpectedOfficeVisitSpecialist = getDataParser().extractData("planBenefitsTestData." + testItem + "." + item + ".contracted.officeVisitSpecialist");
                        string contractExpectedAnnualDeductiblePerPerson = getDataParser().extractData("planBenefitsTestData." + testItem + "." + item + ".contracted.annualDeductiblePerPerson");
                        string contractExpectedAnnualDeductiblePerFamily = getDataParser().extractData("planBenefitsTestData." + testItem + "." + item + ".contracted.annualDeductiblePerFamily");
                        string contractExpectedCoinsurance = getDataParser().extractData("planBenefitsTestData." + testItem + "." + item + ".contracted.coinsurance");
                        string contractExpectedOutOfPocketMaxPerPerson = getDataParser().extractData("planBenefitsTestData." + testItem + "." + item + ".contracted.outOfPocketMaxPerPerson");
                        string contractExpectedOutOfPocketMaxPerFamily = getDataParser().extractData("planBenefitsTestData." + testItem + "." + item + ".contracted.outOfPocketMaxPerFamily");

                        // Extract Json Data non-contract data
                        string nonContractExpectedOfficeVisitPrimaryCare = getDataParser().extractData("planBenefitsTestData." + testItem + "." + item + ".nonContracted.officeVisitPrimaryCare");
                        string nonContractExpectedOfficeVisitSpecialist = getDataParser().extractData("planBenefitsTestData." + testItem + "." + item + ".nonContracted.officeVisitSpecialist");
                        string nonContractExpectedAnnualDeductiblePerPerson = getDataParser().extractData("planBenefitsTestData." + testItem + "." + item + ".nonContracted.annualDeductiblePerPerson");
                        string nonContractExpectedAnnualDeductiblePerFamily = getDataParser().extractData("planBenefitsTestData." + testItem + "." + item + ".nonContracted.annualDeductiblePerFamily");
                        string nonContractExpectedCoinsurance = getDataParser().extractData("planBenefitsTestData." + testItem + "." + item + ".nonContracted.coinsurance");
                        string nonContractExpectedOutOfPocketMaxPerPerson = getDataParser().extractData("planBenefitsTestData." + testItem + "." + item + ".nonContracted.outOfPocketMaxPerPerson");
                        string nonContractExpectedOutOfPocketMaxPerFamily = getDataParser().extractData("planBenefitsTestData." + testItem + "." + item + ".nonContracted.outOfPocketMaxPerFamily");

                        //---------------------------------------------------------------------------------------------------------------
                        // trying more systematic approach
                        // Find Index position of Matching Plan and build the xpath
                        string medicalPlanMethodName = "getPlanLabelsxPath";
                        MedicalPageObject medicalObject = new MedicalPageObject(getDriver());
                        Type typeMedical = typeof(MedicalPageObject);
                        MethodInfo medicalPlanMethod = typeMedical.GetMethod(medicalPlanMethodName);
                        string medicalPlanMethodXPath = (string)medicalPlanMethod.Invoke(medicalObject, null);
                        // find tr elements -> list of users in the grid
                        var listOfPlanOptionElements = driver.FindElements(By.XPath(medicalPlanMethodXPath));
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
                                planIndex = indexCount;
                                TestContext.Progress.WriteLine("Found Index of Item: " + planIndex.ToString());
                                break;
                            }
                        }
                       

                        TestContext.Progress.WriteLine(planIndex.ToString() + " This is index that I will use to extract Plan Grid Data");

                        // Hard coded row column positions. taken from medical step page selection table.
                        string[] tableRows = ["8", "9","10","11"];
                        int contract = 1;
                        int nonContract = 2;
                        // contract xpath
                        string medicalPlanContractGridMethodName = "getMedicalGridValuesxPath";
                        MethodInfo medicalContractGridPlanMethod = typeMedical.GetMethod(medicalPlanContractGridMethodName);
                        string medicalPlanContractGridMethodXPath = (string)medicalContractGridPlanMethod.Invoke(medicalObject, null);
                        string[] splitXpath = medicalPlanContractGridMethodXPath.Split("+");
                        // declaring variables
                        string contractOfficeVisitValuesMethodXPath = "";
                        string nonContractOfficeVisitValuesMethodXPath = "";
                        string contractAnnualDeductibleValuesMethodXPath = "";
                        string nonContractAnnualDeductibleValuesMethodXPath = "";
                        string contractCoinsuranceValuesMethodXPath = "";
                        string nonContractCoinsuranceValuesMethodXPath = "";
                        string contractOutOfPocketMaxValuesMethodXPath = "";
                        string nonContractOutOfPocketMaxValuesMethodXPath = "";

                        foreach (var element in tableRows) {
                            if (element == "8") {
                                contractOfficeVisitValuesMethodXPath = splitXpath[0] + element + splitXpath[2] + planIndex + splitXpath[4]+ contract + splitXpath[6];
                                nonContractOfficeVisitValuesMethodXPath = splitXpath[0] + element + splitXpath[2] + planIndex + splitXpath[4] + nonContract + splitXpath[6];
                            }
                            else if (element == "9"){
                                contractAnnualDeductibleValuesMethodXPath = splitXpath[0] + element + splitXpath[2] + planIndex + splitXpath[4] + contract + splitXpath[6];
                                nonContractAnnualDeductibleValuesMethodXPath = splitXpath[0] + element + splitXpath[2] + planIndex + splitXpath[4] + nonContract + splitXpath[6];
                            }
                            else if (element == "10")
                            {
                                contractCoinsuranceValuesMethodXPath = splitXpath[0] + element + splitXpath[2] + planIndex + splitXpath[4] + contract + splitXpath[6];
                                nonContractCoinsuranceValuesMethodXPath = splitXpath[0] + element + splitXpath[2] + planIndex + splitXpath[4] + nonContract + splitXpath[6];
                            }
                            else if (element == "11")
                            {
                                contractOutOfPocketMaxValuesMethodXPath = splitXpath[0] + element + splitXpath[2] + planIndex + splitXpath[4] + contract + splitXpath[6];
                                nonContractOutOfPocketMaxValuesMethodXPath = splitXpath[0] + element + splitXpath[2] + planIndex + splitXpath[4] + nonContract + splitXpath[6];
                            }

                        }
                        

                        // Extract data from medical step selection table
                        // contract
                        string contractOfficeVisitValues = driver.FindElement(By.XPath(contractOfficeVisitValuesMethodXPath)).Text;
                        string contractAnnualDeductibleValues = driver.FindElement(By.XPath(contractAnnualDeductibleValuesMethodXPath)).Text;
                        string contractCoinsuranceValues = driver.FindElement(By.XPath(contractCoinsuranceValuesMethodXPath)).Text;
                        string contractOutOfPocketMaxValues = driver.FindElement(By.XPath(contractOutOfPocketMaxValuesMethodXPath)).Text;
                        // nonContract
                        string nonContractOfficeVisitValues = driver.FindElement(By.XPath(nonContractOfficeVisitValuesMethodXPath)).Text;
                        string nonContractAnnualDeductibleValues = driver.FindElement(By.XPath(nonContractAnnualDeductibleValuesMethodXPath)).Text;
                        string nonContractCoinsuranceValues = driver.FindElement(By.XPath(nonContractCoinsuranceValuesMethodXPath)).Text;
                        string nonContractOutOfPocketMaxValues = driver.FindElement(By.XPath(nonContractOutOfPocketMaxValuesMethodXPath)).Text;
                        //---------------------------------------------------------------------------------------------------------------------
                        TestContext.Progress.WriteLine("------- Value of extracted contract values below ------ ");
                        TestContext.Progress.WriteLine(contractOfficeVisitValues);
                        TestContext.Progress.WriteLine(contractAnnualDeductibleValues);
                        TestContext.Progress.WriteLine(contractCoinsuranceValues);
                        TestContext.Progress.WriteLine(contractOutOfPocketMaxValues);

                        TestContext.Progress.WriteLine("------- Value of extracted nonContract values below ------ ");
                        TestContext.Progress.WriteLine(nonContractOfficeVisitValues);
                        TestContext.Progress.WriteLine(nonContractAnnualDeductibleValues);
                        TestContext.Progress.WriteLine(nonContractCoinsuranceValues);
                        TestContext.Progress.WriteLine(nonContractOutOfPocketMaxValues);




                        // Extract Data from Medical Grid Table
                        /* Old code that I refactored
                        MedicalPageObject medicalStepObject = new MedicalPageObject(getDriver());
                        Type type = typeof(MedicalPageObject);

                        // Extract Contract Data Values from Grid for item (Plan)
                        // Office Visit Values
                        string contractOfficeVisitValuesMethodName = "get" + item + "ContractOfficeVisitValuesMedicalCoveragexPath";
                        MethodInfo contractOfficeVisitValuesMethod = type.GetMethod(contractOfficeVisitValuesMethodName);
                        string contractOfficeVisitValuesMethodXPath = (string)contractOfficeVisitValuesMethod.Invoke(medicalStepObject, null);
                        string contractOfficeVisitValues = driver.FindElement(By.XPath(contractOfficeVisitValuesMethodXPath)).Text;

                        // Annual Deductible
                        string contractAnnualDeductibleValuesMethodName = "get" + item + "ContractAnnualDeductibleValuesMedicalCoveragexPath";
                        MethodInfo contractAnnualDeductibleValuesMethod = type.GetMethod(contractAnnualDeductibleValuesMethodName);
                        string contractAnnualDeductibleValuesMethodXPath = (string)contractAnnualDeductibleValuesMethod.Invoke(medicalStepObject, null);
                        string contractAnnualDeductibleValues = driver.FindElement(By.XPath(contractAnnualDeductibleValuesMethodXPath)).Text;

                        // Coinsurance
                        string contractCoinsuranceValuesMethodName = "get" + item + "ContractCoinsuranceValuesMedicalCoveragexPath";
                        MethodInfo contractCoinsuranceValuesMethod = type.GetMethod(contractCoinsuranceValuesMethodName);
                        string contractCoinsuranceValuesMethodXPath = (string)contractCoinsuranceValuesMethod.Invoke(medicalStepObject, null);
                        string contractCoinsuranceValues = driver.FindElement(By.XPath(contractCoinsuranceValuesMethodXPath)).Text;

                        // OutOfPocketMax
                        string contractOutOfPocketMaxValuesMethodName = "get" + item + "ContractOutOfPocketMaxValuesMedicalCoveragexPath";
                        MethodInfo contractOutOfPocketMaxValuesMethod = type.GetMethod(contractOutOfPocketMaxValuesMethodName);
                        string contractOutOfPocketMaxValuesMethodXPath = (string)contractOutOfPocketMaxValuesMethod.Invoke(medicalStepObject, null);
                        string contractOutOfPocketMaxValues = driver.FindElement(By.XPath(contractOutOfPocketMaxValuesMethodXPath)).Text;

                        // Extract Non-Contract Data Values from Grid for item (Plan)
                        // Office Visit Values
                        string nonContractOfficeVisitValuesMethodName = "get" + item + "NonContractOfficeVisitValuesMedicalCoveragexPath";
                        MethodInfo nonContractOfficeVisitValuesMethod = type.GetMethod(nonContractOfficeVisitValuesMethodName);
                        string nonContractOfficeVisitValuesMethodXPath = (string)nonContractOfficeVisitValuesMethod.Invoke(medicalStepObject, null);
                        string nonContractOfficeVisitValues = driver.FindElement(By.XPath(nonContractOfficeVisitValuesMethodXPath)).Text;

                        // Annual Deductible
                        string nonContractAnnualDeductibleValuesMethodName = "get" + item + "NonContractAnnualDeductibleValuesMedicalCoveragexPath";
                        MethodInfo nonContractAnnualDeductibleValuesMethod = type.GetMethod(nonContractAnnualDeductibleValuesMethodName);
                        string nonContractAnnualDeductibleValuesMethodXPath = (string)nonContractAnnualDeductibleValuesMethod.Invoke(medicalStepObject, null);
                        string nonContractAnnualDeductibleValues = driver.FindElement(By.XPath(nonContractAnnualDeductibleValuesMethodXPath)).Text;

                        // Coinsurance
                        string nonContractCoinsuranceValuesMethodName = "get" + item + "NonContractCoinsuranceValuesMedicalCoveragexPath";
                        MethodInfo nonContractCoinsuranceValuesMethod = type.GetMethod(nonContractCoinsuranceValuesMethodName);
                        string nonContractCoinsuranceValuesMethodXPath = (string)nonContractCoinsuranceValuesMethod.Invoke(medicalStepObject, null);
                        string nonContractCoinsuranceValues = driver.FindElement(By.XPath(nonContractCoinsuranceValuesMethodXPath)).Text;

                        // OutOfPocketMax
                        string nonContractOutOfPocketMaxValuesMethodName = "get" + item + "NonContractOutOfPocketMaxValuesMedicalCoveragexPath";
                        MethodInfo nonContractOutOfPocketMaxValuesMethod = type.GetMethod(nonContractOutOfPocketMaxValuesMethodName);
                        string nonContractOutOfPocketMaxValuesMethodXPath = (string)nonContractOutOfPocketMaxValuesMethod.Invoke(medicalStepObject, null);
                        string nonContractOutOfPocketMaxValues = driver.FindElement(By.XPath(nonContractOutOfPocketMaxValuesMethodXPath)).Text;
                        */


                        // Compare Values for  Contracted and non-contract Values In Grid with expected values
                        // Contracted
                        Assert.That(contractOfficeVisitValues, Does.Contain(contractExpectedOfficeVisitPrimaryCare).IgnoreCase);
                        Assert.That(contractOfficeVisitValues, Does.Contain(contractExpectedOfficeVisitSpecialist).IgnoreCase);
                        Assert.That(contractAnnualDeductibleValues, Does.Contain(contractExpectedAnnualDeductiblePerPerson).IgnoreCase);
                        Assert.That(contractAnnualDeductibleValues, Does.Contain(contractExpectedAnnualDeductiblePerFamily).IgnoreCase);
                        Assert.That(contractCoinsuranceValues, Does.Contain(contractExpectedCoinsurance).IgnoreCase);
                        Assert.That(contractOutOfPocketMaxValues, Does.Contain(contractExpectedOutOfPocketMaxPerPerson).IgnoreCase);
                        Assert.That(contractOutOfPocketMaxValues, Does.Contain(contractExpectedOutOfPocketMaxPerFamily).IgnoreCase);
                        // non-contracted
                        Assert.That(nonContractOfficeVisitValues, Does.Contain(nonContractExpectedOfficeVisitPrimaryCare).IgnoreCase);
                        Assert.That(nonContractOfficeVisitValues, Does.Contain(nonContractExpectedOfficeVisitSpecialist).IgnoreCase);
                        Assert.That(nonContractAnnualDeductibleValues, Does.Contain(nonContractExpectedAnnualDeductiblePerPerson).IgnoreCase);
                        Assert.That(nonContractAnnualDeductibleValues, Does.Contain(nonContractExpectedAnnualDeductiblePerFamily).IgnoreCase);
                        Assert.That(nonContractCoinsuranceValues, Does.Contain(nonContractExpectedCoinsurance).IgnoreCase);
                        Assert.That(nonContractOutOfPocketMaxValues, Does.Contain(nonContractExpectedOutOfPocketMaxPerPerson).IgnoreCase);
                        Assert.That(nonContractOutOfPocketMaxValues, Does.Contain(nonContractExpectedOutOfPocketMaxPerFamily).IgnoreCase);


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