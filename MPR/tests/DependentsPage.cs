using MPR.pageObjects;
using MPR.utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;
using System.Reflection;
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
    public class DependentsPageShould : Base
    {

        [Test]
        //[Ignore("Ignore test")]
        public void VerifyDependentsPageLaunching()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            DependentsPageObject dependentsPage = new DependentsPageObject(getDriver());

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("medicalUser.username");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("medicalUser.password");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            menuPage.getbtnContinue().Click();

            dependentsPage.getclkDependents().Click();

            string medicalPageHeading = dependentsPage.getheadingText().Text;
            string medicalPageHeadingExpected = "Add Dependents";
            Assert.That(medicalPageHeading, Is.EqualTo(medicalPageHeadingExpected));
        }

        public IWebDriver GetDriver()
        {
            return driver;
        }

        [Test]
        //[Ignore("Ignore test")]
        public void deleteUsersOtherDependents()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            DependentsPageObject dependentsPage = new DependentsPageObject(getDriver());

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("medicalUser.username");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("medicalUser.password");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            menuPage.getbtnContinue().Click();

            dependentsPage.getclkDependents().Click();

            string methodName = "getdependentsxPath";
            DependentsPageObject c = new DependentsPageObject(getDriver());
            Type type = typeof(DependentsPageObject);
            MethodInfo method = type.GetMethod(methodName);
            string methodXPath = (string)method.Invoke(c, null);

            // find tr elements -> list of users in the grid
            var originalListOfDependents = driver.FindElements(By.XPath(methodXPath));
            // count users
            int count = originalListOfDependents.Count;
            TestContext.Progress.WriteLine("Number of Dependents: " + count);
            while (count > 1)
            {
                try
                {
                    TestContext.Progress.WriteLine("Delete users");
                    dependentsPage.getdeleteBtn().Click();
                    Thread.Sleep(2000);
                    dependentsPage.getconfirmDeletionBtn().Click();
                    Thread.Sleep(2000);
                    var currentListOfDependents = driver.FindElements(By.XPath(methodXPath));
                    count = currentListOfDependents.Count;
                    Thread.Sleep(2000);
                }
                catch (NoSuchElementException) {
                    TestContext.Progress.WriteLine("Could not find element");
                    break;
                }
            }
        }

        [Test]
        //[Ignore("Ignore test")]
        public void createUsersOtherDependents()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            DependentsPageObject dependentsPage = new DependentsPageObject(getDriver());

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("medicalUser.username");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("medicalUser.password");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            menuPage.getbtnContinue().Click();

            dependentsPage.getclkDependents().Click();

            string methodName = "getdependentsxPath";
            DependentsPageObject c = new DependentsPageObject(getDriver());
            Type type = typeof(DependentsPageObject);
            MethodInfo method = type.GetMethod(methodName);
            string methodXPath = (string)method.Invoke(c, null);

            // find tr elements -> list of users in the grid
            var originalListOfDependents = driver.FindElements(By.XPath(methodXPath));
            // count users
            int count = originalListOfDependents.Count;
            TestContext.Progress.WriteLine("Number of dependents in grid: " + count);
            // get and count addition dependents users
            string[] additionalDependents = getDataParser().extractDataArray("dependantUser.dependentUserData");


            if (count == 1)
            {
                TestContext.Progress.WriteLine("Create Users");
                foreach (string additionalDependent in additionalDependents)
                {
                    TestContext.Progress.WriteLine("Adding " + additionalDependent);
                    // extract data from Json
                    string fN = getDataParser().extractData("dependantUser." + additionalDependent + ".firstName");
                    string lN = getDataParser().extractData("dependantUser." + additionalDependent + ".lastName");
                    string sSN = getDataParser().extractData("dependantUser." + additionalDependent + ".SSN");
                    string bD = getDataParser().extractData("dependantUser." + additionalDependent + ".birthDate");
                    string R = getDataParser().extractData("dependantUser." + additionalDependent + ".relationship");
                    string G = getDataParser().extractData("dependantUser." + additionalDependent + ".Gender");

                    try
                    {
                        // click add dependent button
                        dependentsPage.getaddDependentBtn().Click();
                        Thread.Sleep(2000);

                        // Send extracted data to fields
                        dependentsPage.getfirstNameField().SendKeys(fN);
                        dependentsPage.getlastNameField().SendKeys(lN);
                        dependentsPage.getSSNField().SendKeys(sSN);
                        dependentsPage.getbirthDateField().SendKeys(bD);
                        if (G == "Male")
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
                        Thread.Sleep(2000);

                        // continue entering information
                        dependentsPage.getradioBtnMarriedNo().Click();
                        dependentsPage.getradioBtnEnrolledYes().Click();
                        dependentsPage.getradioBtnOtherCoverageNo().Click();

                        // second continue button click
                        dependentsPage.getcontinuePopUpBtn().Click();
                        Thread.Sleep(2000);

                    }
                    catch (NoSuchElementException ex)
                    {
                        TestContext.Progress.WriteLine("Could not find element");
                        TestContext.Progress.WriteLine(ex);
                        break;
                    }
                        

                    }




                }
                

            }

        }




}