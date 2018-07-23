using System;

namespace DAL.Models
{
    public class CrawlBillDetail
    {
        public long Id { get; set; }
        public long CrawlDetailsId { get; set; }
        public long BillId { get; set; }
        public DateTime Started { get; set; }
    }
}
