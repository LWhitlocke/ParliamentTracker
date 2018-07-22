using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ParliamentBillsCrawler.Utils
{
    public class Common
    {
        public static IWebElement WaitUntilElementExists(IWebDriver driver, int timeout = 10)
        {
            try
            {
                Thread.Sleep(1000);
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(ExpectedConditions.ElementExists(By.TagName("html")));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Html Element was not found in current context page.");
                throw;
            }
        }
    }
}
