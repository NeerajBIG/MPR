using MPR.pageObjects;
using MPR.utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Reflection;
using System.Xml.Linq;

namespace MPR.tests
{
    // -------------------------------------------------------------------------------
    /// <summary>
    /// This is the description of the method.
    /// </summary>
    /// <param name="firstParameter">This is the first parameter.</param>
    /// <returns>This is the description of the return value.</returns>
    // -------------------------------------------------------------------------------
    public class MenuPageShould : Base
    {
        [Test]
        public void VerifyMenuPageSteps()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("usernameValid");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("passwordValid");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            menuPage.getbtnContinue().Click();


            //int menuStepsCount = driver.FindElements(By.XPath("//span[text()='Introduction']/parent::div/parent::div/parent::div/parent::div/div[@onclick]")).Count;
            //TestContext.Progress.WriteLine(menuStepsCount);

            //int menuStepsCountExpected = 14;
            //Assert.That(menuStepsCount, Is.EqualTo(menuStepsCountExpected));
        }
        [Test]
        public void VerifyIntroductionText()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("usernameValid");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("passwordValid");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            menuPage.getbtnContinue().Click();

            string txtIntroduction = menuPage.getintroductionText().Text;

            string txtIntroductionExpected = "Introduction";
            Assert.That(txtIntroduction, Is.EqualTo(txtIntroductionExpected));
        }


        [Test]
        public void VerifyMenuPremiumDetails()
        {
            LoginPageObject loginPage = new LoginPageObject(getDriver());
            MenuPageObject menuPage = new MenuPageObject(getDriver());
            AboutMePageObject aboutMePage = new AboutMePageObject(getDriver());
            MedicalPageObject medicalPage = new MedicalPageObject(getDriver());
            DependentsPageObject dependentsPage = new DependentsPageObject(getDriver());
            FSAPageObject FSAPage = new FSAPageObject(getDriver());

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("medicalUser.username");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("medicalUser.password");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            menuPage.getbtnContinue().Click();                      
            Thread.Sleep(5000);

            // Get completed Steps
            IList<IWebElement> listOfCompletedStepsWebElements = menuPage.getCompletedStepsByBGColor();
            List<string> listOfCompletedSteps = new List<string>();
            foreach (IWebElement element in listOfCompletedStepsWebElements) 
            {
                listOfCompletedSteps.Add(element.Text);
                TestContext.Progress.WriteLine(element.Text);
            }
            if (listOfCompletedSteps.Contains("Introduction") & listOfCompletedSteps.Contains("About Me") & listOfCompletedSteps.Contains("Manage Dependents"))
            {
                TestContext.Progress.WriteLine("Steps Required To See Premium Details Complete. Continue with test");
            }
            else 
            {
                Assert.Fail("Missing one of the following steps required for premium link to be visible. Introduction,About Me,Manage Dependents");
            }
            // Go to Manage Department. Click next to go to Medical Step
            //menuPage.getclkManageDependents().Click();
            driver.Url = "https://demo2.dmba.com/DMBA_Enrollment/IE/Dependents";
            Thread.Sleep(2000);
            aboutMePage.getnextBtn().Click();
            Thread.Sleep(2000);

            string medicalPlanSelection = "SELECT";
            // Find Index position of Matching Plan
            string medicalPlanMethodName = "getPlanLabelsxPath";
            MedicalPageObject medicalObject = new MedicalPageObject(getDriver());
            Type typeMedical = typeof(MedicalPageObject);
            MethodInfo medicalPlanMethod = typeMedical.GetMethod(medicalPlanMethodName);
            string medicalPlanMethodXPath = (string)medicalPlanMethod.Invoke(medicalObject, null);
            // find tr elements -> list of users in the grid
            var listOfPlanOptionElements = driver.FindElements(By.XPath(medicalPlanMethodXPath));
            // Looking for index of plan
            int indexCount = 0;
            int planIndex = 0;
            foreach (var element in listOfPlanOptionElements)
            {
                indexCount++;
                TestContext.Progress.WriteLine("Looking for index of this plan: " + medicalPlanSelection);
                TestContext.Progress.WriteLine(" Text of element: " + element.Text);
                Thread.Sleep(1000);
                if (element.Text.Contains(medicalPlanSelection))
                {
                    planIndex = indexCount;
                    TestContext.Progress.WriteLine("Found Index of Item: " + planIndex.ToString());
                    break;
                }
            }
            TestContext.Progress.WriteLine(planIndex.ToString() + " This is index that I need to find radio button");

            // Get xpath and Rate from medical step page
            string rateMethodName = "get" + medicalPlanSelection + "medicalCoverageRatexPath";
            MethodInfo methodCompareRate = typeMedical.GetMethod(rateMethodName);
            string methodCompareRateXPath = (string)methodCompareRate.Invoke(medicalObject, null);
            string getMedicalPlanRateToCompare = driver.FindElement(By.XPath(methodCompareRateXPath)).Text;
            TestContext.Progress.WriteLine("Extracted rate: " + getMedicalPlanRateToCompare);


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
                if (indexCount == planIndex)
                {
                    element.Click();
                    break;
                }
            }
            Thread.Sleep(2000);
            aboutMePage.getnextBtn().Click();
            Thread.Sleep(3000);

            // -----------------------------------------  dental plan --------------------------------
            string dentalPlanSelection = "PLUS";
            // Loop Through and find index of Plan
            string methodName = "getPlanLabelsxPath";
            DentalPageObject dentalObject = new DentalPageObject(getDriver());
            Type dentalType = typeof(DentalPageObject);
            MethodInfo methodComparePlan = dentalType.GetMethod(methodName);
            string methodXPath = (string)methodComparePlan.Invoke(dentalObject, null);
            var listOfItems = driver.FindElements(By.XPath(methodXPath));
            indexCount = 0;
            planIndex = 0;
            foreach (var element in listOfItems)
            {
                indexCount++;
                TestContext.Progress.WriteLine("Looking for index of this plan: " + dentalPlanSelection);
                TestContext.Progress.WriteLine(" Text of element: " + element.Text);
                Thread.Sleep(1000);
                if (element.Text.Contains(dentalPlanSelection))
                {
                    planIndex = indexCount + 1;
                    TestContext.Progress.WriteLine("Found Index of Item: " + planIndex.ToString());
                    break;
                }
            }
            TestContext.Progress.WriteLine(planIndex.ToString() + " This is index that I need to find radio button");
            Thread.Sleep(1000);

            //  ----------------------------- Get xpath and Rate from dental step page -------------------------
            string rateDentalMethodName = "get" + dentalPlanSelection + "CoverageRatexPath";
            MethodInfo methodDentalCompareRate = dentalType.GetMethod(rateDentalMethodName);
            string methodDentalCompareRateXPath = (string)methodDentalCompareRate.Invoke(dentalObject, null);
            string getDentalRateToCompare = driver.FindElement(By.XPath(methodDentalCompareRateXPath)).Text;

            // Find Radio button to interact with based on matching index of plan and click
            string dentalSelectionMethodName = "getPlanSelectionxPath";
            MethodInfo dentalSelectionMethod = dentalType.GetMethod(dentalSelectionMethodName);
            string dentalMethodXPath = (string)dentalSelectionMethod.Invoke(dentalObject, null);
            // find tr elements
            listOfRadioOptionElements = driver.FindElements(By.XPath(dentalMethodXPath));
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
            // dental page -> click the next button to get to vision page
            aboutMePage.getnextBtn().Click();
            Thread.Sleep(1000);
            //  ----------------------------------------- Vision Step ------------------------------------------- 
            // Find Index position of Matching Plan and build the xpath. reusing medical plan xpath cause its the same as dental.
            string visionPlanSelection = "WITHOUT";
            string visionPlanMethodName = "getPlanLabelsxPath";
            VisionPageObject visionObject = new VisionPageObject(getDriver());
            Type typeVision = typeof(VisionPageObject);
            MethodInfo visionPlanMethod = typeVision.GetMethod(visionPlanMethodName);
            string visionPlanMethodXPath = (string)visionPlanMethod.Invoke(visionObject, null);
            // find tr elements -> list of users in the grid
            listOfPlanOptionElements = driver.FindElements(By.XPath(visionPlanMethodXPath));
            // Find Index of plan
            indexCount = 0;
            planIndex = 0;
            foreach (var element in listOfPlanOptionElements)
            {
                indexCount++;
                TestContext.Progress.WriteLine("Looking for index of this plan: " + visionPlanSelection);
                TestContext.Progress.WriteLine(" Text of element: " + element.Text);
                Thread.Sleep(1000);
                string[] splitValues = element.GetAttribute("innerHTML").Split("<br>");
                if (splitValues.Contains(visionPlanSelection))
                {
                    planIndex = indexCount;
                    TestContext.Progress.WriteLine("Found Index of Item: " + planIndex.ToString());
                    break;
                }
            }
            TestContext.Progress.WriteLine(planIndex.ToString() + " This is index that I will use to extract Plan Grid Data");

            // ---------------------------- Get Vision Plan Rate ---------------------------------------------
            string rateVisionMethodName = "get" + visionPlanSelection + "CoverageRatexPath";
            MethodInfo methodVisionCompareRate = typeVision.GetMethod(rateVisionMethodName);
            string methodVisionCompareRateXPath = (string)methodVisionCompareRate.Invoke(visionObject, null);
            string getVisionRateToCompare = driver.FindElement(By.XPath(methodVisionCompareRateXPath)).Text;

            // Find Radio button to interact with based on matching index of plan and click
            string visionSelectionMethodName = "getPlanSelectionxPath";
            MethodInfo visionSelectionMethod = typeVision.GetMethod(visionSelectionMethodName);
            string visionMethodXPath = (string)visionSelectionMethod.Invoke(visionObject, null);
            // find tr elements
            listOfRadioOptionElements = driver.FindElements(By.XPath(visionMethodXPath));
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
            // Click Next Button
            aboutMePage.getnextBtn().Click();
            Thread.Sleep(1000);
            // ---------------------------------------- FSA Step ----------------------------------------------
            // Verify Monthy Deduction columns
            //calculation = election amount / months remaining in the year after hire date
            string[] userHiredate = getDataParser().extractData("userHireDate").Split("/");
            double x = 12 - double.Parse(userHiredate[0]);
            string expectedFSAOutput = "$" + Math.Round((500.00 / x) * 2, 2).ToString();
            TestContext.Progress.WriteLine(expectedFSAOutput);

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
            Thread.Sleep(2000);
            // Click Next Button
            aboutMePage.getnextBtn().Click();
            Thread.Sleep(2000);
            menuPage.getmenuBtn().Click();
            Thread.Sleep(2000);
            // ----------------------------------- SGTL Selection --------------------------------------------
            // ----------------------------------- 24-Hour AD&D Selection ------------------------------------

            menuPage.getPremiumDetailsLink().Click();
            Thread.Sleep(3000);
            var dollarValues = new List<string>();
            var planLabels = new List<string>();
            int iRowsCount = menuPage.getPremiumDetailsPopoverContents().Count;
            // check if select plans are correct
            bool containsMedicalPlanName = false;
            bool containsDentalPlanName = false;
            bool containsVisionPlanName = false;

            for (int iRows = 1; iRows <= iRowsCount; iRows++) 
            {
                string bb = menuPage.getpremiumDetailsPopoverxPath();
                string bb2 = menuPage.getpremiumDetailsPopoverxPath();
                string premiumType = driver.FindElement(By.XPath(bb+"["+ iRows +"]/td[1]")).Text;
                string premiumValue = driver.FindElement(By.XPath(bb + "[" + iRows + "]/td[2]")).Text;
                planLabels.Add(premiumType);
                dollarValues.Add(premiumValue);
                TestContext.Progress.WriteLine(premiumType +" -- "+ premiumValue);
                if (premiumType.Contains(medicalPlanSelection, StringComparison.OrdinalIgnoreCase)) {
                    containsMedicalPlanName = true;
                }
                else if (premiumType.Contains(dentalPlanSelection, StringComparison.OrdinalIgnoreCase))
                {
                    containsDentalPlanName = true;
                }
                else if (premiumType.Contains(visionPlanSelection, StringComparison.OrdinalIgnoreCase))
                {
                    containsVisionPlanName = true;
                }
            }
            string aa = menuPage.getpremiumDetailsPopoverFooter1xPath().Text;
            string ab = menuPage.getpremiumDetailsPopoverFooter2xPath().Text;
            TestContext.Progress.WriteLine(aa + " -- " + ab);
            TestContext.Progress.WriteLine("Values contained " + getMedicalPlanRateToCompare + " :" + dollarValues.Contains(getMedicalPlanRateToCompare));
            TestContext.Progress.WriteLine("Values contained " + getDentalRateToCompare + " :" + dollarValues.Contains(getDentalRateToCompare));
            TestContext.Progress.WriteLine("Values contained " + getVisionRateToCompare + " :" + dollarValues.Contains(getVisionRateToCompare));
            TestContext.Progress.WriteLine("Values contained " + expectedFSAOutput + " :" + dollarValues.Contains(expectedFSAOutput));

            // Medical comparison
            
            Assert.That(containsMedicalPlanName);
            Assert.That(dollarValues, Does.Contain(getMedicalPlanRateToCompare).IgnoreCase);
            // Dental comparison
            Assert.That(containsDentalPlanName);
            Assert.That(dollarValues, Does.Contain(getDentalRateToCompare).IgnoreCase);
            // Vision comparison
            Assert.That(containsVisionPlanName);
            Assert.That(dollarValues, Does.Contain(getVisionRateToCompare).IgnoreCase);
            // FSA comparison
            Assert.That(dollarValues, Does.Contain(expectedFSAOutput).IgnoreCase);

        }
    }
}