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
using System.Xml.Linq;
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
    public class VisionPageShould : Base
    {

        [Test]
        //[Ignore("Ignore test")]
        public void VerifyVisionPageLaunching()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            MedicalPageObject medicalPage = new MedicalPageObject(getDriver());
            DentalPageObject dentalPage = new DentalPageObject(getDriver());
            VisionPageObject visionPage = new VisionPageObject(getDriver());

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("medicalUser.username");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("medicalUser.password");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            menuPage.getbtnContinue().Click();

            visionPage.getclkVision().Click();
            Thread.Sleep(1000);

            IWebElement x = driver.FindElement(By.XPath("//*[@id='tdVISIONPlanTitle']"));
            TestContext.Progress.WriteLine(x.GetAttribute("innerHTML"));
            TestContext.Progress.WriteLine("------------------------------------------------");
            string test = x.GetAttribute("innerHTML");
            TestContext.Progress.WriteLine(test);
            TestContext.Progress.WriteLine("------------------------------------------------");
            string[] splitValues = test.Split("<br>");
            foreach (var word in splitValues)
            {
                TestContext.Progress.WriteLine("This is from the split array ------> " + word);
                if (word == "WITHOUT") {
                    TestContext.Progress.WriteLine("THIS MATCHED CORRECTLY");
                }
            }
            if (splitValues.Contains("WITH"))
            {
                TestContext.Progress.WriteLine("True");
            }
            else {
                TestContext.Progress.WriteLine("False");
            }
            TestContext.Progress.WriteLine("------------------------------------------------");



            string visionPageHeading = visionPage.getheadingText().Text;
            string visionPageHeadingExpected = "Vision (VSP)";
            Assert.That(visionPageHeading, Is.EqualTo(visionPageHeadingExpected));
        }

        [Test]
        //[Ignore("Ignore test")]
        public void VerifyVisionPagePlans()
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

            string[] testData = getDataParser().extractDataArray("medicalUser.VisionPlanChangeTestData");

            foreach (var testItem in testData)
            {

                TestContext.Progress.WriteLine("----------------- Doing Tests For " + testItem + "------------------------------");
                Thread.Sleep(1000);
                string execution = getDataParser().extractData("medicalUser." + testItem + ".execute");
                if (execution == "True")
                {
                    driver.SwitchTo().NewWindow(WindowType.Tab);
                    // on dependent page.
                    driver.Url = getDataParser().extractData("dependantStepURL");

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
                            //string relationship = getDataParser().extractData("dependantUser.additionalDependant1.relationship");
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
                    driver.Url = getDataParser().extractData("VisionStepURL");

                    loginPage.getbtnLogoutText().Click();
                    loginPage.getloginLink().Click();
                    loginPage.getusername().SendKeys(usernameValid);
                    loginPage.getpassword().SendKeys(passwordValid);
                    loginPage.getsubmit().Click();
                    Thread.Sleep(2000);
                    driver.Url = getDataParser().extractData("VisionStepURL");

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
                            VisionPageObject visionObject = new VisionPageObject(getDriver());
                            Type type = typeof(VisionPageObject);
                            MethodInfo method = type.GetMethod(methodName);
                            string methodXPath = (string)method.Invoke(visionObject, null);

                            string getValueToCompare = driver.FindElement(By.XPath(methodXPath)).Text;
                            Assert.That(getValueToCompare, Does.Contain(item).IgnoreCase);
                            //TestContext.Progress.WriteLine(getValueToCompare + " contains " + item);
                        }
                        if (itemIndex >= 2)
                        {
                            // Compare Label with expected label
                            TestContext.Progress.WriteLine(itemIndex.ToString() + " Testing for " + item);
                            string methodName = "get" + item + "CoveragexPath";
                            VisionPageObject visionObject = new VisionPageObject(getDriver());
                            Type type = typeof(VisionPageObject);
                            MethodInfo methodComparePlan = type.GetMethod(methodName);
                            string methodComparePlanXPath = (string)methodComparePlan.Invoke(visionObject, null);
                            Thread.Sleep(1000);

                            string getPlanToCompare = driver.FindElement(By.XPath(methodComparePlanXPath)).Text;
                            Assert.That(getPlanToCompare, Does.Contain(item).IgnoreCase);

                            // Compare Price to Expected price
                            // Test Json Value
                            string expectedRate = getDataParser().extractData("medicalUser." + testItem + "." + item);
                            // Get xpath and Rate from medical step page
                            string rateMethodName = "get" + item + "CoverageRatexPath";
                            MethodInfo methodCompareRate = type.GetMethod(rateMethodName);
                            string methodCompareRateXPath = (string)methodCompareRate.Invoke(visionObject, null);
                            string getRateToCompare = driver.FindElement(By.XPath(methodCompareRateXPath)).Text;

                            TestContext.Progress.WriteLine(getRateToCompare + "(Rate From Vision Step Page) should equal = " + expectedRate);
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

        [Test]
        //[Ignore("Ignore test")]
        public void VerifyVisionPageGridValues()
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
            string passwordValid = getDataParser().extractData("medicalUser.password");
            loginPage.getpassword().SendKeys(passwordValid);
            loginPage.getsubmit().Click();
            menuPage.getbtnContinue().Click();


            // Get current window handle
            string originalWindow = driver.CurrentWindowHandle;
            string[] testData = getDataParser().extractDataArray("planVisionBenefitsTestData.CaseList");
            TestContext.Progress.WriteLine("original window handle" + originalWindow);

            foreach (var testItem in testData)
            {

                TestContext.Progress.WriteLine("----------------- Doing Tests For " + testItem + "------------------------------");
                Thread.Sleep(1000);
                string execution = getDataParser().extractData("planVisionBenefitsTestData." + testItem + ".execute");
                if (execution == "True")
                {
                    driver.SwitchTo().NewWindow(WindowType.Tab);
                    driver.Url = getDataParser().extractData("medicalUser.PersonalInfoStepURL");

                    //string originalWindow = driver.CurrentWindowHandle;
                    string zipCode = getDataParser().extractData("planVisionBenefitsTestData." + testItem + ".zipcode");
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
                        visionPage.getclkVision().Click();
                    }
                    catch (NoSuchElementException)
                    {
                        driver.Url = getDataParser().extractData("VisionStepURL");
                    }
                    // get plans to compare with plans in new tab opened by link.
                    string[] planList = getDataParser().extractDataArray("planVisionBenefitsTestData." + testItem + ".plansToValidate");
                    int itemIndex = 1;
                    TestContext.Progress.WriteLine("##################################################");

                    foreach (string item in planList)
                    {
                        TestContext.Progress.WriteLine("Testing Values for " + item);
                        // Extract Json Data data
                        string ExpectedEyeExam = getDataParser().extractData("planVisionBenefitsTestData." + testItem + "." + item + ".eyeExam");
                        string ExpectedFrames = getDataParser().extractData("planVisionBenefitsTestData." + testItem + "." + item + ".frames");
                        string ExpectedSingleVisionLenses = getDataParser().extractData("planVisionBenefitsTestData." + testItem + "." + item + ".singleVisionLenses");
                        string ExpectedFramesContacts = getDataParser().extractData("planVisionBenefitsTestData." + testItem + "." + item + ".framesContacts");
                        string ExpectedFramesContactFittings = getDataParser().extractData("planVisionBenefitsTestData." + testItem + "." + item + ".framesContactFittings");

                        //---------------------------------------------------------------------------------------------------------------
                        // trying more systematic approach
                        // Find Index position of Matching Plan and build the xpath. reusing medical plan xpath cause its the same as dental.
                        string visionPlanMethodName = "getPlanLabelsxPath";
                        VisionPageObject visionObject = new VisionPageObject(getDriver());
                        Type typeVision = typeof(VisionPageObject);
                        MethodInfo visionPlanMethod = typeVision.GetMethod(visionPlanMethodName);
                        string visionPlanMethodXPath = (string)visionPlanMethod.Invoke(visionObject, null);
                        // find tr elements -> list of users in the grid
                        var listOfPlanOptionElements = driver.FindElements(By.XPath(visionPlanMethodXPath));
                        // select every plan once
                        int indexCount = 0;
                        int planIndex = 0;
                        foreach (var element in listOfPlanOptionElements)
                        {
                            indexCount++;
                            TestContext.Progress.WriteLine("Looking for index of this plan: " + item);
                            TestContext.Progress.WriteLine(" Text of element: " + element.Text);
                            Thread.Sleep(1000);
                            string[] splitValues = element.GetAttribute("innerHTML").Split("<br>");
                             if (splitValues.Contains(item))
                            {
                                    planIndex = indexCount;
                                    TestContext.Progress.WriteLine("Found Index of Item: " + planIndex.ToString());
                                    break;
                            }
                        }


                        TestContext.Progress.WriteLine(planIndex.ToString() + " This is index that I will use to extract Plan Grid Data");

                        // Hard coded row column positions. taken from Dental step page selection table.
                        string[] tableRows = ["8", "9", "10", "11", "12"];

                        // get xpath of dental grid
                        string VisionPlanGridMethodName = "getVisionGridValuesxPath";
                        MethodInfo visionGridPlanMethod = typeVision.GetMethod(VisionPlanGridMethodName);
                        string visionGridMethodXPath = (string)visionGridPlanMethod.Invoke(visionObject, null);
                        string[] splitXpath = visionGridMethodXPath.Split("+");
                        // declaring variables
                        string eyeExamValuesMethodXPath = "";
                        string framesValuesMethodXPath = "";
                        string singleVisionLensesValuesMethodXPath = "";
                        string framesContactsValuesMethodXPath = "";
                        string framesContactFittingsValuesMethodXPath = "";

                        // using grid xpath and tableRow and planIndex location. create xpaths to extract the table values.
                        foreach (var row in tableRows)
                        {
                            if (row == "8")
                            {
                                eyeExamValuesMethodXPath = splitXpath[0] + row + splitXpath[2] + planIndex + splitXpath[4];
                            }
                            else if (row == "9")
                            {
                                framesValuesMethodXPath = splitXpath[0] + row + splitXpath[2] + planIndex + splitXpath[4];
                            }
                            else if (row == "10")
                            {
                                singleVisionLensesValuesMethodXPath = splitXpath[0] + row + splitXpath[2] + planIndex + splitXpath[4];
                            }
                            else if (row == "11")
                            {
                                framesContactsValuesMethodXPath = splitXpath[0] + row + splitXpath[2] + planIndex + splitXpath[4];
                            }
                            else if (row == "12")
                            {
                                framesContactFittingsValuesMethodXPath = splitXpath[0] + row + splitXpath[2] + planIndex + splitXpath[4];
                            }

                        }

                        // Extract data from Vision step selection table
                        string eyeExamValues = driver.FindElement(By.XPath(eyeExamValuesMethodXPath)).Text;
                        string framesValues = driver.FindElement(By.XPath(framesValuesMethodXPath)).Text;
                        string singleVisionLensesValues = driver.FindElement(By.XPath(singleVisionLensesValuesMethodXPath)).Text;
                        string framesContactsValues = driver.FindElement(By.XPath(framesContactsValuesMethodXPath)).Text;
                        string framesContactFittingsValues = driver.FindElement(By.XPath(framesContactFittingsValuesMethodXPath)).Text;

                        TestContext.Progress.WriteLine("------- Value of extracted values below ------ ");
                        TestContext.Progress.WriteLine(eyeExamValues);
                        TestContext.Progress.WriteLine(framesValues);
                        TestContext.Progress.WriteLine(singleVisionLensesValues);
                        TestContext.Progress.WriteLine(framesContactsValues);
                        TestContext.Progress.WriteLine(framesContactFittingsValues);



                        // Compare Values for  Contracted and non-contract Values In Grid with expected values
                        Assert.That(eyeExamValues, Does.Contain(ExpectedEyeExam).IgnoreCase);
                        Assert.That(framesValues, Does.Contain(ExpectedFrames).IgnoreCase);
                        Assert.That(singleVisionLensesValues, Does.Contain(ExpectedSingleVisionLenses).IgnoreCase);
                        Assert.That(framesContactsValues, Does.Contain(ExpectedFramesContacts).IgnoreCase);
                        Assert.That(framesContactFittingsValues, Does.Contain(ExpectedFramesContactFittings).IgnoreCase);


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