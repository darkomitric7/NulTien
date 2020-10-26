using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAutomtionProject.Extensions;
using TestAutomtionProject.PageObjects;
using NLog;

namespace NulTien.PageObjects
{
    public class HomePage : BasePage
    {

        private static readonly string LOGIN_URL = ConfigurationManager.AppSettings["URL"];

        [FindsBy(How = How.Id, Using = "searchKeywordsInput")]
        private IWebElement SearchBoxLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text()='Mobilni Telefoni']")]
        private IWebElement SuggestionBoxChoice { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='kpBoxCloseButton']")]
        private IWebElement PopUpCloseButtonLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[text()='Mobilni telefoni > Samsung']")]
        private IWebElement SuggestionBoxMyChoice { get; set; }

        public HomePage(IWebDriver driver) : base(driver)
        {
            logger.Debug("============ NEW TEST STARTED ================");
            logger.Debug("new HomePage()");
            this.driver = driver;
        }

        public HomePage Open()
        {
            logger.Debug("Home Page Open()");
            driver.Url = LOGIN_URL;
            return this;
        }

        public void TypeTextToSearch(string textToSearch)
        {
            logger.Debug("TypeTextToSearch()");
            Wait(5);
            SearchBoxLocator.Clear();
            SearchBoxLocator.SendKeys(textToSearch);
        }

        public string GetPageTitle()
        {
            logger.Debug("IsPageTitleAsExpected()");
            return driver.Title;
        }

        public void ClosePopUp()
        {
            logger.Debug("ClosePopUp()");
            WaitForElementToBeClickable(PopUpCloseButtonLocator, 5);
            PopUpCloseButtonLocator.Click();
        }

        public SamsungPhonesPage ClickOnMyChoice()
        {
            logger.Debug("ClickOnMyChoice()");
            WaitForElementToBeClickable(SuggestionBoxMyChoice, 10);
            SuggestionBoxMyChoice.Click();
            return new SamsungPhonesPage(driver);
        }
    }
}
