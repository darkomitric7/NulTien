using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;
using System.Configuration;

/**
 * @author darko.mitric7@gmail.com
 *
 */

namespace TestAutomtionProject.WebDriver
{
    public class WebDriverFactory
    {

        private static IWebDriver driver;

        public static IWebDriver CreateDriver()
        {
            String browserName = ConfigurationManager.AppSettings["BROWSER"];

            switch (browserName.ToLower())
            {
                case "firefox":
                    driver = new FirefoxDriver();
                    return driver;
                case "chrome":
                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AddArguments("--disable-notifications");
                    //chromeOptions.AddArguments("--headless");
                    chromeOptions.AddArguments("start-maximized");
                    //chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
                    driver = new ChromeDriver(chromeOptions);
                    return driver;
                case "ie":
                    driver = new InternetExplorerDriver();
                    return driver;
                default:
                    ChromeOptions chromeOptionsDefault = new ChromeOptions();
                    chromeOptionsDefault.AddArguments("--disable-notifications");
                    chromeOptionsDefault.AddArguments("start-maximized");
                    driver = new ChromeDriver(chromeOptionsDefault);
                    return driver;
            }
        }
    }
}
