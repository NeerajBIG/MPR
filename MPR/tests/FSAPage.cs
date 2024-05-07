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
        public void VerifyFSAPageLaunching()
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
        public void VerifyFSAErrorMessage()
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
        public void VerifyFSAMoreInformationLink()
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
                Assert.Fail("Web element not found Error: " + ex.ToString());
            }


        }


        // -------------------------------------------------------------------------------
        /// <summary>
        /// Verify "?" icon popup models opens,closes for both HealthCare, dependent, Do you want
        /// healthcare FSA benefit card icons.
        /// Covers Text Cases from TestCases Excel Document. Test Cases 297,299-301,303-305,307-308
        /// </summary>
        /// <param name="firstParameter">This is the first parameter.</param>
        // -------------------------------------------------------------------------------
        [Test]
        //[Ignore("Ignore test")]
        public void VerifyFSAPopUpModals()
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

                // Verify that all modals have correct title and close properly
                // click HealthCare Modal Icon
                FSAPage.gethealthCareFSAIconLink().Click();
                Thread.Sleep(2000);
                // Verify the title
                string expectedHealthCareModalTitle = "Medical and Dental FSA";
                string healthCareModalTitle = FSAPage.gethealthCareFSAModalTitle().Text;
                Assert.That(expectedHealthCareModalTitle, Is.EqualTo(healthCareModalTitle));
                // Close modal with cancel button
                FSAPage.getmodelCancelButton().Click();
                Thread.Sleep(2000);
                // Open again to test the "x" icon close link for modal
                FSAPage.gethealthCareFSAIconLink().Click();
                Thread.Sleep(2000);
                FSAPage.getmodelCloseIcon().Click();
                Thread.Sleep(2000);

                // click Dependent Modal Icon
                FSAPage.getdependentCareFSAIconLink().Click();
                Thread.Sleep(2000);
                // Verify the title
                string expectedDependentCareModalTitle = "Dependent Care FSA";
                string dependentCareModalTitle = FSAPage.getdependentCareFSAModalTitle().Text;
                Assert.That(expectedDependentCareModalTitle, Is.EqualTo(dependentCareModalTitle));
                // Close modal with cancel button
                FSAPage.getmodelCancelButton().Click();
                Thread.Sleep(2000);
                // Open again to test the "x" icon close link for modal
                FSAPage.getdependentCareFSAIconLink().Click();
                Thread.Sleep(2000);
                FSAPage.getmodelCloseIcon().Click();
                Thread.Sleep(2000);

                // click HealthCare FSA Benefit Card Modal Icon
                FSAPage.gethealthCareFSABenefitCardIconLink().Click();
                Thread.Sleep(2000);
                // Verify the title
                string expectedHealthCareFSABenefitCardModalTitle = "Healthcare FSA Benefit Card";
                string healthCareFSABenefitCardModalTitle = FSAPage.gethealthCareFSABenefitCardModalTitle().Text;
                Assert.That(expectedHealthCareFSABenefitCardModalTitle, Is.EqualTo(healthCareFSABenefitCardModalTitle));
                // Close modal with cancel button
                FSAPage.getmodelCancelButton().Click();
                Thread.Sleep(2000);
                // Open again to test the "x" icon close link for modal
                FSAPage.gethealthCareFSABenefitCardIconLink().Click();
                Thread.Sleep(2000);
                FSAPage.getmodelCloseIcon().Click();
                Thread.Sleep(2000);
            }
            catch (NoSuchElementException ex)
            {
                Assert.Fail("Web element not found Error: " + ex.ToString());
            }
        }

        // -------------------------------------------------------------------------------
        /// <summary>
        /// Check that inputing valid user input results in expected navigation or output for fields
        ///  Covers Text Cases from TestCases Excel Document. Test Cases 309,310,317-321,325 - 326
        /// </summary>
        /// <param name="firstParameter">This is the first parameter.</param>
        // -------------------------------------------------------------------------------
        [Test]
        //[Ignore("Ignore test")]
        public void VerifyValidUserInputInFSA()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            FSAPageObject FSAPage = new FSAPageObject(getDriver());
            AboutMePageObject aboutMePage = new AboutMePageObject(getDriver());
            LDBPageObject LDBPage = new LDBPageObject(getDriver());

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("medicalUser.username");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("medicalUser.password");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            menuPage.getbtnContinue().Click();

            FSAPage.getclkFSA().Click();
            Thread.Sleep(1000);

            // Test FSA second page and if inputs are valid
            // Clear boxes
            FSAPage.gethealthCareFSATextBox().Clear();
            FSAPage.getdependentCareFSATextBox().Clear();
            Thread.Sleep(1000);
            FSAPage.gethealthCareFSATextBox().SendKeys("1000");
            FSAPage.getdependentCareFSATextBox().SendKeys("1000");
            FSAPage.getHealthCareFSABenefitCardYesRadioButton().Click();
            Thread.Sleep(1000);
            // click next button to test if input valid
            aboutMePage.getnextBtn().Click();
            Thread.Sleep(2000);

            // verify that we are on FSA 2a page
            string secondFSAPageText = FSAPage.getsecondFSAPageVerification().Text;
            string secondFSAPageTextExpected = "Before proceeding, please review the policies below.";
            Assert.That(secondFSAPageText, Is.EqualTo(secondFSAPageTextExpected));

            // click Prev button -> takes user to vision step.
            aboutMePage.getprevBtn().Click();
            Thread.Sleep(1000);
            // click Next button to go back to FSA
            aboutMePage.getnextBtn().Click();
            Thread.Sleep(1000);

            // Clear boxes
            FSAPage.gethealthCareFSATextBox().Clear();
            FSAPage.getdependentCareFSATextBox().Clear();
            Thread.Sleep(1000);
            FSAPage.gethealthCareFSATextBox().SendKeys("0");
            FSAPage.getdependentCareFSATextBox().SendKeys("0");
            // Click No Radio Button
            FSAPage.getHealthCareFSABenefitCardNoRadioButton().Click();
            Thread.Sleep(1000);
            // click next button to test if input valid
            aboutMePage.getnextBtn().Click();
            Thread.Sleep(2000);

            // verify that we are on LDB page
            string LDBPageHeading = LDBPage.getheadingText().Text;
            string LDBPageHeadingExpected = "Life and Disability Benefits";
            Assert.That(LDBPageHeading, Is.EqualTo(LDBPageHeadingExpected));

            // click Prev button -> takes user to vision step.
            aboutMePage.getprevBtn().Click();
            Thread.Sleep(1000);

            // Verify Monthy Deduction columns
            //calculation = election amount / months remaining in the year after hire date
            string[] userHiredate = getDataParser().extractData("userHireDate").Split("/");
            double x = 12 - double.Parse(userHiredate[0]);
            string expectedOutput = "$" + Math.Round((500.00/x),2).ToString();
            TestContext.Progress.WriteLine(expectedOutput);

            // Clear boxes
            FSAPage.gethealthCareFSATextBox().Clear();
            FSAPage.getdependentCareFSATextBox().Clear();
            Thread.Sleep(1000);
            FSAPage.gethealthCareFSATextBox().SendKeys("500");
            FSAPage.getdependentCareFSATextBox().SendKeys("500");
            // Click No Radio Button
            FSAPage.getHealthCareFSABenefitCardNoRadioButton().Click();
            Thread.Sleep(1000);
            // click off text for calculation to trigger
            FSAPage.getheadingText().Click();

            // verify Monthly Deductions for HealthCare and Dependent Care
            string healthCareMonthlyDeduction = FSAPage.getHealthCareFSAMonthlyDecuction().Text;
            string dependentCareMonthlyDeduction = FSAPage.getDependentCareFSAMonthlyDecuction().Text;

            // Verify Page Monthly Deduction with expected
            Assert.That(healthCareMonthlyDeduction, Is.EqualTo(expectedOutput));
            Assert.That(dependentCareMonthlyDeduction, Is.EqualTo(expectedOutput));


            // Verify "Do you want a healthcare FSA benefit card to pay for qualifying medical, dental, and vision expenses?" Radio buttons
            // verify No Radio Button
            // Clear boxes
            FSAPage.gethealthCareFSATextBox().Clear();
            FSAPage.getdependentCareFSATextBox().Clear();
            Thread.Sleep(1000);
            FSAPage.gethealthCareFSATextBox().SendKeys("1000");
            FSAPage.getdependentCareFSATextBox().SendKeys("1000");

            // Click No Radio Button
            FSAPage.getHealthCareFSABenefitCardNoRadioButton().Click();
            Thread.Sleep(1000);
            // click next button to test if input valid
            aboutMePage.getnextBtn().Click();
            Thread.Sleep(2000);

            // verify that we are on LDB page
            LDBPageHeading = LDBPage.getheadingText().Text;
            Assert.That(LDBPageHeading, Is.EqualTo(LDBPageHeadingExpected));

            // click Prev button -> takes user to vision step.
            aboutMePage.getprevBtn().Click();
            Thread.Sleep(1000);

            // verify Yes Radio Button
            // Clear boxes
            FSAPage.gethealthCareFSATextBox().Clear();
            FSAPage.getdependentCareFSATextBox().Clear();
            Thread.Sleep(1000);
            FSAPage.gethealthCareFSATextBox().SendKeys("0");
            FSAPage.getdependentCareFSATextBox().SendKeys("1000");

            // Click No Radio Button
            FSAPage.getHealthCareFSABenefitCardYesRadioButton().Click();
            Thread.Sleep(1000);
            // click next button to test if input valid
            aboutMePage.getnextBtn().Click();
            Thread.Sleep(2000);

            // verify that we are on LDB page
            LDBPageHeading = LDBPage.getheadingText().Text;
            Assert.That(LDBPageHeading, Is.EqualTo(LDBPageHeadingExpected));

            // click Prev button -> takes user to vision step.
            aboutMePage.getprevBtn().Click();
            Thread.Sleep(1000);

            // Verify Second FSA Page Navigation
            // Testing Navigation when selecting "No" radio button input.
            // Clear boxes
            FSAPage.gethealthCareFSATextBox().Clear();
            FSAPage.getdependentCareFSATextBox().Clear();
            Thread.Sleep(1000);
            FSAPage.gethealthCareFSATextBox().SendKeys("1000");
            FSAPage.getdependentCareFSATextBox().SendKeys("1000");
            FSAPage.getHealthCareFSABenefitCardYesRadioButton().Click();
            Thread.Sleep(1000);
            // click next button to test if input valid
            aboutMePage.getnextBtn().Click();
            Thread.Sleep(2000);

            // verify that we are on FSA 2a page
            secondFSAPageText = FSAPage.getsecondFSAPageVerification().Text;
            Assert.That(secondFSAPageText, Is.EqualTo(secondFSAPageTextExpected));

            // Click No Radio button on second FSA page
            FSAPage.getSecondPageFSABenefitCardNoRadioButton().Click();

            // click Next button to go back to FSA
            aboutMePage.getnextBtn().Click();
            Thread.Sleep(1000);

            // verify we are on FSA page
            string FSAPageHeading = FSAPage.getheadingText().Text;
            string FSAPageHeadingExpected = "Flexible Spending Account (FSA)";
            Assert.That(FSAPageHeading, Is.EqualTo(FSAPageHeadingExpected));

            // Testing Navigation when selecting "Yes" radio button input.
            FSAPage.gethealthCareFSATextBox().Clear();
            FSAPage.getdependentCareFSATextBox().Clear();
            Thread.Sleep(1000);
            FSAPage.gethealthCareFSATextBox().SendKeys("1000");
            FSAPage.getdependentCareFSATextBox().SendKeys("1000");
            FSAPage.getHealthCareFSABenefitCardYesRadioButton().Click();
            Thread.Sleep(1000);
            // click next button to test if input valid
            aboutMePage.getnextBtn().Click();
            Thread.Sleep(2000);

            // verify that we are on FSA 2a page
            secondFSAPageText = FSAPage.getsecondFSAPageVerification().Text;
            Assert.That(secondFSAPageText, Is.EqualTo(secondFSAPageTextExpected));

            // Click No Radio button on second FSA page
            FSAPage.getSecondPageFSABenefitCardYesRadioButton().Click();

            // click next button to test if input valid
            aboutMePage.getnextBtn().Click();
            Thread.Sleep(2000);

            // verify that we are on LDB page
            LDBPageHeading = LDBPage.getheadingText().Text;
            Assert.That(LDBPageHeading, Is.EqualTo(LDBPageHeadingExpected));

        }

    }    
}