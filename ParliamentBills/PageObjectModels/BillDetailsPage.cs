using OpenQA.Selenium;
using ParliamentBillsCrawler.Interfaces;
using ParliamentBillsCrawler.Utils;
using SeleniumExtras.PageObjects;

namespace ParliamentBillsCrawler.PageObjectModels
{
    public class BillDetailsPage : IParliamentWebPages
    {
        private readonly IWebDriver _driver;

        public string WebPage { get; set; }

        public BillDetailsPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        public void NavigateToPage(string uri)
        {
            _driver.Navigate().GoToUrl(WebPage);
            Common.WaitUntilElementExists(_driver);
        }
    }
}
