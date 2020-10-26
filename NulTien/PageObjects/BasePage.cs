using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using TestAutomtionProject.Utils;
using SeleniumExtras.PageObjects;

/**
 * @author darko.mitric7@gmail.com
 *
 */

namespace TestAutomtionProject.PageObjects
{
    public class BasePage
    {

        public IWebDriver driver;

        protected NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        //Implicit wait
        public void Wait(int seconds)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(seconds);
        }

        // Explicit Waits
        public void WaitForElementToBeClickable(IWebElement element, int seconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
        }

        public static bool WaitTillElementisDisplayed(IWebDriver driver, By by, int timeoutInSeconds)
        {
            bool elementDisplayed = false;

            for (int i = 0; i < timeoutInSeconds; i++)
            {
                try
                {
                    if (timeoutInSeconds > 0)
                    {
                        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                        wait.Until(drv => drv.FindElement(by));
                    }
                    elementDisplayed = driver.FindElement(by).Displayed;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return elementDisplayed;

        }

        /**
         * 1. Wait methods (1. to slow down the test execution (implicit waits) 2.
         * to wait for the elements to appear (explicit waits) 2. Logs ???
         */

    }
}
