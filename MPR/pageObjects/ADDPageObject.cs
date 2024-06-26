﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using static System.Net.Mime.MediaTypeNames;

namespace MPR.pageObjects
{
    // -------------------------------------------------------------------------------
    /// <summary>
    /// This is the description of the method.
    /// </summary>
    /// <param name="firstParameter">This is the first parameter.</param>
    /// <returns>This is the description of the return value.</returns>
    // -------------------------------------------------------------------------------
    public class ADDPageObject
    {
        private IWebDriver driver;

        public ADDPageObject(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//div[contains(@onclick,'Accidental') and contains(.,'AD&D')]")]
        private IWebElement clkADD;
        public IWebElement getclkADD()
        { 
            return clkADD;
        }

        [FindsBy(How = How.XPath, Using = "//span[text()='24-Hour Accidental Death and Dismemberment']")]
        private IWebElement headingText;
        public IWebElement getheadingText()
        {

            return headingText;
        }


    }
    public class MyADDReflectionClass
    {
        public string MyMethod()
        {
            return DateTime.Now.ToString();
        }
    }
}
