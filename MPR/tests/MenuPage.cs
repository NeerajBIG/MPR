using MPR.pageObjects;
using MPR.utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

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

            loginPage.getloginLink().Click();

            string usernameValid = getDataParser().extractData("newUser.username");
            loginPage.getusername().SendKeys(usernameValid);

            string passwordValid = getDataParser().extractData("newUser.password");
            loginPage.getpassword().SendKeys(passwordValid);

            loginPage.getsubmit().Click();

            menuPage.getbtnContinue().Click();                      
            Thread.Sleep(2000);

            menuPage.getPremiumDetailsLink().Click();
            Thread.Sleep(3000);

            int iRowsCount = menuPage.getPremiumDetailsPopoverContents().Count;
            for (int iRows = 1; iRows <= iRowsCount; iRows++) 
            {
                string bb = menuPage.getpremiumDetailsPopoverxPath();
                string bb2 = menuPage.getpremiumDetailsPopoverxPath();
                string premiumType = driver.FindElement(By.XPath(bb+"["+ iRows +"]/td[1]")).Text;
                string premiumValue = driver.FindElement(By.XPath(bb + "[" + iRows + "]/td[2]")).Text;
                TestContext.Progress.WriteLine(premiumType +" -- "+ premiumValue);
            }
            string aa = menuPage.getpremiumDetailsPopoverFooter1xPath().Text;
            string ab = menuPage.getpremiumDetailsPopoverFooter2xPath().Text;
            TestContext.Progress.WriteLine(aa + " -- " + ab);
        }
    }
}