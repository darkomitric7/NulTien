
using NulTien.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAutomtionProject.WebDriver;

namespace NulTien.TestCases
{
    class SeleniumTestCase
    {
        private readonly string TEXT_TO_SEARCH = "Samsung";
        private readonly string expectedTitle = "KupujemProdajem | Praktično sve...";
        private readonly string sortOption = "Popularnije";

        private IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            driver = new WebDriverWrapper().GetDriver();
        }

        [Test]
        public void Test()
        {
            // Open Home page
            var homePage = new HomePage(driver).Open();

            // Close PopUp window
            homePage.ClosePopUp();
            Assert.That(homePage.GetPageTitle().Equals(expectedTitle));

            // 2. In search box type term “Samsung”
            homePage.TypeTextToSearch(TEXT_TO_SEARCH);

            // 3. From suggestion box choose category “Mobilni telefoni > Samsung”
            var samsungPhonesPage = homePage.ClickOnMyChoice();

            // 4. Select sorting of your choice
            samsungPhonesPage.ClickOnDropDownElement("Sortiraj");
            samsungPhonesPage.SelectSortOption(sortOption);

            // 5. Click on “Trazi”
            samsungPhonesPage.ClickOnSearchButton();

            // Check if products are sorted properly (checking first page is enough)
            Assert.That(samsungPhonesPage.GetActualViews(), Is.Ordered.Descending);

            // Check if products belong to category that was chosen
            Assert.AreNotEqual(samsungPhonesPage.CheckIfProductBelongsChoosenCategory("samsung"), "Product doesn't belong choosen category!");

        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }
    }
}
