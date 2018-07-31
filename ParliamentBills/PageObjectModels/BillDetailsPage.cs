using System;
using OpenQA.Selenium;
using ParliamentBillsCommon.Utils;
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
            var houseName = innerSpan.Text.Substring(8, innerSpan.Text.Length - 8).Trim();

            return houseName;
        }

        [FindsBy(How = How.XPath, Using = ".//div[@class='next-event']/ul/li/img")]
        public IWebElement NextEventImageElement { get; set; }

        internal int GetBillStageId()
        {
            var elementVisible = true;
            try
            {
                elementVisible = NextEventImageElement.Displayed;
            }
            catch (Exception ex)
            {
                elementVisible = false;
            }

            if (!elementVisible) return (int)Enums.Stages.PendingRoyalAssent;

            var imageTitle = NextEventImageElement.GetAttribute("title");
            var stage = string.Empty;

            if (imageTitle.ToLower().Contains("lords"))
            {
                stage = "Lords";
            }
            else if (imageTitle.ToLower().Contains("commons"))
            {
                stage = "Commons";
            }

            var stageDetails = imageTitle.Substring(0, imageTitle.IndexOf(":", StringComparison.Ordinal)).Trim();
            var comparisonString = stageDetails.ToLower().Replace(" ", string.Empty);

            switch (comparisonString)
            {
                case "1streading":
                {
                    stage = stage + "FirstReading";
                    break;
                }
                case "2ndreading":
                {
                    stage = stage + "SecondReading";
                        break;
                }
                case "3rdreading":
                {
                    stage = stage + "ThirdReading";
                        break;
                }
                case "reportstage":
                {
                    stage = stage + "ReportStage";
                        break;
                }
                case "committeestage":
                {
                    stage = stage + "CommitteeStage";
                    break;
                }
                case "considerationofamendments":
                {
                    stage = stage + "ConsiderationOfAmendments";
                    break;
                }
                default:
                {
                    throw new Exception("Unknown stage detail: " + stageDetails);
                }
            }

            if (Enum.TryParse(stage, true, out Enums.Stages confirmedStage) == false)
            {
                throw new Exception("Error parsing stage text:" + stage);
            }

            return (int)confirmedStage;
        }
    }
}
