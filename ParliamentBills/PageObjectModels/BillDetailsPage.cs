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
            _driver.Navigate().GoToUrl(uri);
            Common.WaitUntilElementExists(_driver);
        }

        [FindsBy(How = How.XPath, Using = ".//div[@class='diagram']/h2")]
        public IWebElement BillOriginated { get; set; }

        internal string GetBillOriginatedHouse()
        {
            var innerSpan = BillOriginated.FindElement(By.XPath(".//span"));
            var houseName = innerSpan.Text.Substring(8, innerSpan.Text.Length - 9);

            return houseName;
        }
    }
}
