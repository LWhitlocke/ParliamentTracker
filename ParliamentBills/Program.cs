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
            driver.Manage().Window.Minimize();

            var crawlDetail = parliamentBillsContext.CrawlDetails.Add(new CrawlDetail() {Started = DateTime.UtcNow});
            parliamentBillsContext.SaveChanges();

            CurrentBillsBeforeParliamentPageObjectModel = new CurrentBillsBeforeParliamentPage(driver);
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

            BillDetailsPageObjectModel = new BillDetailsPage(driver);
            var existingBills = parliamentBillsContext.Bills.ToList();

            crawlDetail.CrawlBillDetails = new List<CrawlBillDetail>();

            foreach (var bill in billInfo)
            {
                var matchedBill = existingBills.FirstOrDefault(x => x.Title == bill.Title);
                var billRequiresUpdating = (matchedBill != null && bill.LastUpdated > matchedBill.LastUpdated) || matchedBill == null;

                if (billRequiresUpdating || forceUpdate)
                {
                    var temp = new CrawlBillDetail()
                    {
                        Started = DateTime.UtcNow,
                        CrawlDetailsId = crawlDetail.Id
                    };

                    try
                    {
                        BillDetailsPageObjectModel.NavigateToPage(bill.Uri);

                        bill.OriginatedHouse = BillDetailsPageObjectModel.GetBillOriginatedHouse();

                        try
                        {
                            var billStageId = BillDetailsPageObjectModel.GetBillStageId();

                            if (bill.BillStageDetails == null) bill.BillStageDetails = new List<BillStageDetail>();
                            bill.BillStageDetails.Add(new BillStageDetail()
                            {
                                BillStageId = billStageId
                            });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        if (matchedBill != null)
                        {
                            bill.Id = matchedBill.Id;
                            temp.BillId = matchedBill.Id;
                        }
                        else
                        {
                            temp.Bill = bill;
                        }
                    }
                    catch (Exception ex)
                    {
                        temp.ExceptionDetails = ex.ToString();
                    }

                    crawlDetail.CrawlBillDetails.Add(temp);
                }
            }

            crawlDetail.Completed = DateTime.UtcNow;
            parliamentBillsContext.CrawlDetails.AddOrUpdate(crawlDetail);

            parliamentBillsContext.SaveChanges();
            parliamentBillsContext.Dispose();
            driver.Close();
            driver.Quit();

            Console.WriteLine(crawlDetail.CrawlBillDetails.Count + " new/updated records");
            Console.ReadLine();
        }
    }
}
