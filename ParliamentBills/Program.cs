using System;
using System.Globalization;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ParliamentBillsCrawler.PageObjectModels;

namespace ParliamentBillsCrawler
{
    class BillsCrawler
    {


        public static CurrentBillsBeforeParliamentPage CurrentBillsBeforeParliamentPage { get; set; }

        static void Main(string[] args)
        {
            var driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            CurrentBillsBeforeParliamentPage = new CurrentBillsBeforeParliamentPage(driver);
            CurrentBillsBeforeParliamentPage.NavigateToPage();

            var currentBills = CurrentBillsBeforeParliamentPage.GetBills();

            foreach (var bill in currentBills)
            {
                var billName = CurrentBillsBeforeParliamentPage.GetBillName(bill);
                var billUrl = CurrentBillsBeforeParliamentPage.GetBillUrl(bill);
                var billLastUpdated = CurrentBillsBeforeParliamentPage.GetBillLastUpdatedDate(bill);

                Console.WriteLine(billName + " | " + billUrl + " | " + billLastUpdated.ToString(CultureInfo.CurrentCulture));
            }

            Console.WriteLine("Press return to exit");
            Console.ReadLine();
        }
    }
}
