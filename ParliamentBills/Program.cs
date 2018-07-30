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
        private static CurrentBillsBeforeParliamentPage CurrentBillsBeforeParliamentPageObjectModel { get; set; }

        private static BillDetailsPage BillDetailsPageObjectModel { get; set; }

        static void Main(string[] args)
        {
            var forceUpdate = true;
            var parliamentBillsContext = new ParliamentBillsContext();
            var driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            CurrentBillsBeforeParliamentPageObjectModel = new CurrentBillsBeforeParliamentPage(driver);
            BillDetailsPageObjectModel = new BillDetailsPage(driver);
            CurrentBillsBeforeParliamentPageObjectModel.NavigateToPage();

            var currentBills = CurrentBillsBeforeParliamentPageObjectModel.GetBills();

            var billInfo = new List<Bill>();

            foreach (var bill in currentBills)
            {
                var currentHouse = CurrentBillsBeforeParliamentPageObjectModel.GetCurrentHouse(bill);
                var billName = CurrentBillsBeforeParliamentPageObjectModel.GetBillName(bill);
                var billUrl = CurrentBillsBeforeParliamentPageObjectModel.GetBillUrl(bill);
                var billLastUpdated = CurrentBillsBeforeParliamentPageObjectModel.GetBillLastUpdatedDate(bill);

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

                var billRequiresUpdating = (matchedBill != null && bill.LastUpdated > matchedBill.LastUpdated) || matchedBill == null;

                if (billRequiresUpdating || forceUpdate)
                {
                    if (matchedBill != null) bill.Id = matchedBill.Id;

                    BillDetailsPageObjectModel.NavigateToPage(bill.Uri);

                    bill.OriginatedHouse = BillDetailsPageObjectModel.GetBillOriginatedHouse();

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
