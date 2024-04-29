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
    public class FSAShould : Base
    {

        // -------------------------------------------------------------------------------
        /// <summary>
        /// Check Menu button to navigate to FSA works.
        /// </summary>
        /// <param name="firstParameter">This is the first parameter.</param>
        // -------------------------------------------------------------------------------
        [Test]
        //[Ignore("Ignore test")]
        public void VerifyVisionPageLaunching()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            FSAPageObject FSAPage = new FSAPageObject(getDriver());

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("medicalUser.username");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("medicalUser.password");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            menuPage.getbtnContinue().Click();

            FSAPage.getclkFSA().Click();
            Thread.Sleep(1000);

            string FSAPageHeading = FSAPage.getheadingText().Text;
            string FSAPageHeadingExpected = "Flexible Spending Account (FSA)";
            Assert.That(FSAPageHeading, Is.EqualTo(FSAPageHeadingExpected));
        }



        // -------------------------------------------------------------------------------
        /// <summary>
        /// Check if error message pops up when user enters no values and clicks next button.
        /// Covers Text Cases from TestCases Excel Document. Test Cases 311-316
        /// </summary>
        /// <param name="firstParameter">This is the first parameter.</param>
        // -------------------------------------------------------------------------------
        [Test]
        //[Ignore("Ignore test")]
        public void VerifyFSAErrorMessageLaunching()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            AboutMePageObject aboutMePage = new AboutMePageObject(getDriver());
            FSAPageObject FSAPage = new FSAPageObject(getDriver());

            try
            {
                loginPage.getloginLink().Click();

                string usernameValid = getDataParser().extractData("medicalUser.username");
                loginPage.getusername().SendKeys(usernameValid);

                string passwordValid = getDataParser().extractData("medicalUser.password");
                loginPage.getpassword().SendKeys(passwordValid);

                loginPage.getsubmit().Click();

                menuPage.getbtnContinue().Click();

                FSAPage.getclkFSA().Click();
                Thread.Sleep(1000);

                // Clear boxes to be able to trigger alert
                FSAPage.gethealthCareFSATextBox().Clear();
                FSAPage.getdependentCareFSATextBox().Clear();

                // click next button to trigger alert
                aboutMePage.getnextBtn().Click();
                Thread.Sleep(3000);

                // Verify that error messages are displayed when nothing is entered. TC 313,314
                IList<IWebElement> ListOfErrors = FSAPage.getenterValuesErrorAlert();
                bool isHealthCareFSAErrorPresent = false;
                bool isDependentCareFSAErrorPresent = false;
                string expectedHeathCareFSAError = "Please enter an amount for your healthcare FSA election. Enter 0 for no election.";
                string expectedDependentCareFSAError = "Please enter an amount for your dependent care FSA election. Enter 0 for no election.";
                foreach (var error in ListOfErrors)
                {
                    if (error.Text.Contains(expectedHeathCareFSAError))
                    {
                        isHealthCareFSAErrorPresent = true;
                    }
                    else if (error.Text.Contains(expectedDependentCareFSAError))
                    {
                        isDependentCareFSAErrorPresent = true;
                    }

                }
                // Check if the error messages are present
                Assert.That(isHealthCareFSAErrorPresent, Is.True);
                Assert.That(isDependentCareFSAErrorPresent, Is.True);
                Thread.Sleep(2000);

                // Clear boxes to be able to trigger alert
                FSAPage.gethealthCareFSATextBox().Clear();
                FSAPage.getdependentCareFSATextBox().Clear();
                Thread.Sleep(1000);
                FSAPage.gethealthCareFSATextBox().SendKeys("abc");
                FSAPage.getdependentCareFSATextBox().SendKeys("abc");
                Thread.Sleep(1000);
                // click next button to trigger alert
                aboutMePage.getnextBtn().Click();
                Thread.Sleep(3000);

                // Reset variables. Verify that error messages are displayed when invalid data is entered. TC 315-316
                ListOfErrors = FSAPage.getenterValuesErrorAlert();
                isHealthCareFSAErrorPresent = false;
                isDependentCareFSAErrorPresent = false;
                foreach (var error in ListOfErrors)
                {
                    if (error.Text.Contains(expectedHeathCareFSAError))
                    {
                        isHealthCareFSAErrorPresent = true;
                    }
                    else if (error.Text.Contains(expectedDependentCareFSAError))
                    {
                        isDependentCareFSAErrorPresent = true;
                    }

                }
                // Check if the error messages are present
                Assert.That(isHealthCareFSAErrorPresent, Is.True);
                Assert.That(isDependentCareFSAErrorPresent, Is.True);


                // Clear boxes to be able to trigger alert
                FSAPage.gethealthCareFSATextBox().Clear();
                FSAPage.getdependentCareFSATextBox().Clear();
                Thread.Sleep(1000);
                FSAPage.gethealthCareFSATextBox().SendKeys("10000");
                FSAPage.getdependentCareFSATextBox().SendKeys("10000");
                Thread.Sleep(1000);
                // click next button to trigger alert
                aboutMePage.getnextBtn().Click();
                Thread.Sleep(3000);

                // Reset variables. Verify that error messages are displayed when value exceeding limit is entered. TC 311-312.
                ListOfErrors = FSAPage.getenterValuesErrorAlert();
                expectedHeathCareFSAError = "The amount entered for your healthcare FSA election exceeds the yearly maximum.";
                expectedDependentCareFSAError = "The amount entered for your dependent care FSA election exceeds the yearly maximum.";
                isHealthCareFSAErrorPresent = false;
                isDependentCareFSAErrorPresent = false;
                foreach (var error in ListOfErrors)
                {
                    if (error.Text.Contains(expectedHeathCareFSAError))
                    {
                        isHealthCareFSAErrorPresent = true;
                    }
                    else if (error.Text.Contains(expectedDependentCareFSAError))
                    {
                        isDependentCareFSAErrorPresent = true;
                    }

                }
                // Check if the error messages are present
                Assert.That(isHealthCareFSAErrorPresent, Is.True);
                Assert.That(isDependentCareFSAErrorPresent, Is.True);
            }
            catch (NoSuchElementException ex)
            {
                Assert.Fail("Web element not found Error: " + ex);
            }
        }


        // -------------------------------------------------------------------------------
        /// <summary>
        /// Check if HyperLink "Where can I find more information" is working
        /// Covers Text Cases from TestCases Excel Document. Test Cases 293
        /// </summary>
        /// <param name="firstParameter">This is the first parameter.</param>
        // -------------------------------------------------------------------------------
        [Test]
        //[Ignore("Ignore test")]
        public void VerifyMoreInformationLink()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            FSAPageObject FSAPage = new FSAPageObject(getDriver());

            try
            {
                loginPage.getloginLink().Click();

                string usernameValid = getDataParser().extractData("medicalUser.username");
                loginPage.getusername().SendKeys(usernameValid);

                string passwordValid = getDataParser().extractData("medicalUser.password");
                loginPage.getpassword().SendKeys(passwordValid);

                loginPage.getsubmit().Click();

                menuPage.getbtnContinue().Click();

                FSAPage.getclkFSA().Click();
                Thread.Sleep(1000);
                // get current window
                string originalWindow = driver.CurrentWindowHandle;

                // Click Read more button to see link
                FSAPage.getReadMoreBtn().Click();
                Thread.Sleep(1000);
                // Click Hyper Link
                FSAPage.getmoreInformationLink().Click();

                // switch to driver to new tab
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

                // verify that PDF opened. If it can't find element then it didn't work.
                FSAPage.getpdfViewer();
            }
            catch (NoSuchElementException ex)
            {
                Assert.Fail("Web element not found Error: " + ex);
            }


        }



    }    
}