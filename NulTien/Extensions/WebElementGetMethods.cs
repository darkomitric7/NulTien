using OpenQA.Selenium;

namespace TestAutomtionProject.Extensions
{
    class WebElementsGetMethods
    {
        public static string GetText(IWebElement element)
        {
            return element.Text;
        }

    }
}
