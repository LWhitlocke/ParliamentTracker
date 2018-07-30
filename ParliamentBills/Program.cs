using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using DAL;
using DAL.Models;
using OpenQA.Selenium.Chrome;
using ParliamentBillsCrawler.PageObjectModels;

namespace ParliamentBillsCrawler
{
    class BillsCrawler
    {
        public static CurrentBillsBeforeParliamentPage CurrentBillsBeforeParliamentPage { get; set; }

        static void Main(string[] args)
        {
            var parliamentBillsContext = new ParliamentBillsContext();
            var driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            CurrentBillsBeforeParliamentPage = new CurrentBillsBeforeParliamentPage(driver);
            CurrentBillsBeforeParliamentPage.NavigateToPage();

            var currentBills = CurrentBillsBeforeParliamentPage.GetBills();

            var billInfo = new List<Bill>();

            foreach (var bill in currentBills)
            {
                var currentHouse = CurrentBillsBeforeParliamentPage.GetCurrentHouse(bill);
                var billName = CurrentBillsBeforeParliamentPage.GetBillName(bill);
                var billUrl = CurrentBillsBeforeParliamentPage.GetBillUrl(bill);
                var billLastUpdated = CurrentBillsBeforeParliamentPage.GetBillLastUpdatedDate(bill);

                var temp = new Bill()
                {
                    CurrentHouse = currentHouse,
                    LastUpdated = billLastUpdated,
                    Title = billName,
                    Uri = billUrl
                };
                billInfo.Add(temp);
            }

            var existingBills = parliamentBillsContext.Bills.ToList();
            var updatedBills = new List<Bill>();

            foreach (var bill in billInfo)
            {
                var matchedBill = existingBills.FirstOrDefault(x => x.Title == bill.Title);
                if ((matchedBill != null && bill.LastUpdated > matchedBill.LastUpdated) || matchedBill == null)
                {
                    if (matchedBill != null) bill.Id = matchedBill.Id;

                    //Go get the other details from the bill details page
                    updatedBills.Add(bill);
                }
            }

            foreach (var updatedBill in updatedBills)
            {
                parliamentBillsContext.Bills.AddOrUpdate(updatedBill);
            }

            parliamentBillsContext.SaveChanges();
            parliamentBillsContext.Dispose();
            driver.Close();
            driver.Quit();

            Console.WriteLine(updatedBills.Count + " new/updated records");
            Console.ReadLine();
        }
    }
}
