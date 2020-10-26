using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAutomtionProject.PageObjects;

namespace NulTien.PageObjects
{
    public class SamsungPhonesPage : BasePage
    {

        [FindsBy(How = How.XPath, Using = "//span[text()='Sortiraj']")]
        private IList<IWebElement> SortDropDownLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[@class='uiMenuItem']")]
        private IList<IWebElement> SortOptionsLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@class='searchButton secondarySearchButton']")]
        private IWebElement SearchButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='view-count']")]
        private IList<IWebElement> ViewCountLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[@class='adName']")]
        private IList<IWebElement> DescriptionFieldLocator { get; set; }

        public SamsungPhonesPage(IWebDriver driver) : base(driver)
        {
            logger.Debug("new SamsungPhonesPage()");
            this.driver = driver;
        }

        public void ClickOnDropDownElement(string dropdownName)
        {
            logger.Debug("ClickOnSortElement()");
            IList<IWebElement> SortDropDownLocator = driver.FindElements(By.XPath("//span[text()='" + dropdownName + "']"));
            try
            {
                SortDropDownLocator[1].Click();
            }
            catch (StaleElementReferenceException)
            {
                IList<IWebElement> elementList = driver.FindElements(By.XPath("//span[text()='" + dropdownName + "']"));
                WaitForElementToBeClickable(elementList[1], 5);
                elementList[1].Click();
            }
            
        }

        public void SelectSortOption(string sortOption)
        {
            logger.Debug("SelectSortOption()");
            IWebElement SortOptionsLocator = driver.FindElement(By.XPath("//div[text()='" + sortOption + "']"));
            WaitForElementToBeClickable(SortOptionsLocator, 5);
            SortOptionsLocator.Click();
        }

        public void ClickOnSearchButton()
        {
            logger.Debug("ClickOnSearchButton()");
            WaitForElementToBeClickable(SearchButton, 5);
            SearchButton.Click();
        }

        public int[] GetActualViews()
        {
            logger.Debug("GetViews()");
            int size = ViewCountLocator.Count;
            int[] actual = new int[size];

            for (int i = 0; i < size; i++)
            {
                actual[i] = int.Parse(ViewCountLocator[i].Text);
            }

            return actual;
        }

        public string CheckIfProductBelongsChoosenCategory(string text)
        {
            logger.Debug("CheckIfProductBelongsChoosenCategory()");
            int size = DescriptionFieldLocator.Count;
            string message = "";

            for (int i = 0; i < size; i++)
            { 
               if (!DescriptionFieldLocator[i].Text.ToLower().Contains(text))
               {
                    message = "Product doesn't belong choosen category!";
                    Debug.WriteLine(DescriptionFieldLocator[i].Text);
               }
                    
            }

            return message;
        }

    }
}
