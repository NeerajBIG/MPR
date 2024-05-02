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
    public class LDBShould : Base
    {

        // -------------------------------------------------------------------------------
        /// <summary>
        /// Check Menu button to navigate to FSA works.
        /// </summary>
        /// <param name="firstParameter">This is the first parameter.</param>
        // -------------------------------------------------------------------------------
        [Test]
        //[Ignore("Ignore test")]
        public void VerifyLDBPageLaunching()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            FSAPageObject FSAPage = new FSAPageObject(getDriver());
            LDBPageObject LDBPage = new LDBPageObject(getDriver());

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("medicalUser.username");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("medicalUser.password");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            menuPage.getbtnContinue().Click();

            LDBPage.getclkLDB().Click();
            Thread.Sleep(1000);

            string LDBPageHeading = LDBPage.getheadingText().Text;
            string LDBPageHeadingExpected = "Life and Disability Benefits";
            Assert.That(LDBPageHeading, Is.EqualTo(LDBPageHeadingExpected));
        }

        // -------------------------------------------------------------------------------
        /// <summary>
        /// Having issues grabbing element of PDF. still verifys the link works by switching windows.
        /// Check if all the HyperLinks "Where can I find more information" work
        /// Covers Text Cases from TestCases Excel Document. Test Cases 330,334
        /// </summary>
        /// <param name="firstParameter">This is the first parameter.</param>
        // -------------------------------------------------------------------------------
        [Test]
        //[Ignore("Ignore test")]
        public void VerifyLDBMoreInformationLink()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            FSAPageObject FSAPage = new FSAPageObject(getDriver());
            LDBPageObject LDBPage = new LDBPageObject(getDriver());

            try
            {
                loginPage.getloginLink().Click();

                string usernameValid = getDataParser().extractData("medicalUser.username");
                loginPage.getusername().SendKeys(usernameValid);

                string passwordValid = getDataParser().extractData("medicalUser.password");
                loginPage.getpassword().SendKeys(passwordValid);

                loginPage.getsubmit().Click();
                Thread.Sleep(2000);
                menuPage.getbtnContinue().Click();
                Thread.Sleep(2000);
                LDBPage.getclkLDB().Click();
                Thread.Sleep(2000);

                // get current window
                string originalWindow = driver.CurrentWindowHandle;
                
                // Click Hyper Link for Group Term Life
                LDBPage.getmoreGTFInformationLink().Click();
                Thread.Sleep(2000);
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
                Thread.Sleep(2000);

                // verify that PDF opened. If it can't find element then it didn't work.
                FSAPage.getpdfViewer();

                // Close window
                driver.Close();
                
                // Switch back to original browser (first window)
                driver.SwitchTo().Window(originalWindow);

                // Click Hyper Link for Disability Plan (DP)
                LDBPage.getmoreDPInformationLink().Click();
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

                // Close window
                driver.Close();

                // Switch back to original browser (first window)
                driver.SwitchTo().Window(originalWindow);
                // Click Hyper Link Occupational Accidental Death & Dismemberment (OAD&D)
                LDBPage.getmoreOADDInformationLink().Click();
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

                // Close window
                driver.Close();

                // Switch back to original browser (first window)
                driver.SwitchTo().Window(originalWindow);
                
            }
            catch (NoSuchElementException ex)
            {
                Assert.Fail("Web element not found Error: " + ex.ToString());
            }


        }


        // -------------------------------------------------------------------------------
        /// <summary>
        /// Verify the monthly benefit calculation in disability plan text.
        /// Verify that the next button works.
        /// Covers Text Cases from TestCases Excel Document. Test Cases 337, 339
        /// </summary>
        /// <param name="firstParameter">This is the first parameter.</param>
        // -------------------------------------------------------------------------------
        [Test]
        //[Ignore("Ignore test")]
        public void VerifyLDBPageValues()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            FSAPageObject FSAPage = new FSAPageObject(getDriver());
            LDBPageObject LDBPage = new LDBPageObject(getDriver());
            AboutMePageObject aboutMePage = new AboutMePageObject(getDriver());
            SGTLPageObject SGTLPage = new SGTLPageObject(getDriver());

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("medicalUser.username");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("medicalUser.password");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();
            Thread.Sleep(1000);
            menuPage.getbtnContinue().Click();
            Thread.Sleep(1000);
            LDBPage.getclkLDB().Click();
            Thread.Sleep(1000);

            // do calculation for monthly benefit
            string userSalary = getDataParser().extractData("userSalary");
            Double x = Double.Parse(userSalary)/12 * 0.66666667;
            string expectedOutput = Math.Round(x).ToString("C0");
            TestContext.Progress.WriteLine("Expected Output: " + expectedOutput);

            string[] splitValues = LDBPage.getDisabilityPlanText().GetAttribute("innerHTML").Replace("</b>","<b>").Split("<b>", StringSplitOptions.RemoveEmptyEntries);
            string extractedMonthlyBenefits = splitValues[1];
            Console.WriteLine("Extracted Value: "+extractedMonthlyBenefits);

            // verify the values
            Assert.That(expectedOutput, Is.EqualTo(extractedMonthlyBenefits));

            // Click Next button
            aboutMePage.getnextBtn().Click();
            Thread.Sleep(2000);

            // verify the next step is reached
            string LDBSecondPageHeading = LDBPage.getSecondPageHeadingText().Text;
            string LDBSecondPageHeadingExpected = "Beneficiaries for Group Term Life";
            Assert.That(LDBSecondPageHeading, Is.EqualTo(LDBSecondPageHeadingExpected));



        }







    }    
}