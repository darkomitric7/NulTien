using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

/**
 * @author darko.mitric7@gmail.com
 *
 */

namespace TestAutomtionProject.Extensions
{
    public static class WebElementSetMethods
    {
        //Enter text into text box
        public static void EnterText(IWebElement element, string text, string elementName)
        {
            element.Clear();
            element.SendKeys(text);
            Console.WriteLine(text + " entered in the " + elementName + " field.");
        }

        //Check if element is displayed
        public static bool IsElementDisplayed(this IWebElement element, string elementName)
        {
            bool result;
            try
            {
                result = element.Displayed;
                Console.WriteLine(elementName + " is Displayed.");
            }
            catch (Exception)
            {
                result = false;
                Console.WriteLine(elementName + " is not Displayed.");
            }

            return result;
        }

        //Click on element
        public static void ClickOnElement(this IWebElement element, string elementName)
        {
            element.Click();
            Console.WriteLine("Clicked on " + elementName);
        }

        //Select part of element by text (example: select dropdown value by text)
        public static void SelectByText(this IWebElement element, string text, string elementName)
        {
            SelectElement SelectText = new SelectElement(element);
            SelectText.SelectByText(text);
            Console.WriteLine(text + " text selected on " + elementName);
        }

        //Select part of element by Index (example: select dropdown value by Intex)
        public static void SelectByIndex(this IWebElement element, int index, string elementName)
        {
            SelectElement SelectIndex = new SelectElement(element);
            SelectIndex.SelectByIndex(index);
            Console.WriteLine(index + " index selected on " + elementName);
        }

        //Select part of element by Value (example: select dropdown value by Value)
        public static void SelectByValue(this IWebElement element, string text, string elementName)
        {
            SelectElement SelectValue = new SelectElement(element);
            SelectValue.SelectByValue(text);
            Console.WriteLine(text + " value selected on " + elementName);
        }


    }
}
