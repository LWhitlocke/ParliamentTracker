using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public class Bill
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string CurrentHouse { get; set; }
        public string OriginatedHouse { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Uri { get; set; }

        public ICollection<BillStageDetail> BillStageDetails { get; set; }
        public ICollection<CrawlBillDetail> CrawlBillDetails { get; set; }
    }
}
