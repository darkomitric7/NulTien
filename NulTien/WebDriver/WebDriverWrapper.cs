using OpenQA.Selenium;
using System;

/**
 * @author darko.mitric7@gmail.com
 *
 */

namespace TestAutomtionProject.WebDriver
{
    public class WebDriverWrapper
    {
        private IWebDriver driver;

        protected int implicitTimeout = 10;

        public IWebDriver GetDriver()
        {
            driver = WebDriverFactory.CreateDriver();
            //driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(implicitTimeout);

            return driver;
        }

        public static void LoadApplication(IWebDriver driver, string url)
        {
            driver.Url = url;
        }

    }
}
