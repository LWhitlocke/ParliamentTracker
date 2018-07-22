using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using ParliamentBillsCrawler.Interfaces;
using ParliamentBillsCrawler.Utils;
using SeleniumExtras.PageObjects;

namespace ParliamentBillsCrawler.PageObjectModels
{
    public class CurrentBillsBeforeParliamentPage : IParliamentWebPages
    {
        private readonly IWebDriver _driver;

        public string WebPage { get; set; }

        public CurrentBillsBeforeParliamentPage(IWebDriver driver)
        {
            _driver = driver;
            WebPage = "https://services.parliament.uk/bills/";
            
            PageFactory.InitElements(_driver, this);
        }

        public void NavigateToPage()
        {
            _driver.Navigate().GoToUrl(WebPage);
            Common.WaitUntilElementExists(_driver);
        }

        [FindsBy(How=How.XPath, Using= ".//table[@class='bill-list']")]
        public IWebElement BillsList { get; set; }

        public IList<IWebElement> GetBills()
        {
            return BillsList.FindElements(By.XPath(".//tbody/tr[not(@class='group-heading')]"));
        }

        internal string GetBillUrl(IWebElement billElement)
        {
            var children = billElement.FindElements(By.XPath(".//td"));
            var anchor = children[1].FindElement(By.XPath(".//a"));

            var url = anchor.GetAttribute("href");

            return url;
        }

        public string GetBillName(IWebElement billElement)
        {
            var children = billElement.FindElements(By.XPath(".//td"));

            return children[1].Text;
        }

        public DateTime GetBillLastUpdatedDate(IWebElement billElement)
        {
            var children = billElement.FindElements(By.XPath(".//td"));
            var dateString = children[2].Text;
            DateTime.TryParse(dateString, out var date);

            return date;
        }
    }
}
