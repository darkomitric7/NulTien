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

        public IWebDriver GetDriver()
        {
            driver = WebDriverFactory.CreateDriver();
            driver.Manage().Window.Maximize();

            return driver;
        }

        public static void LoadApplication(IWebDriver driver, string url)
        {
            driver.Url = url;
        }

    }
}
