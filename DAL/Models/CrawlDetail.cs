using System;

namespace DAL.Models
{
    public class CrawlDetail
    {
        public long Id { get; set; }
        public DateTime Started { get; set; }
        public DateTime? Completed { get; set; }
        
    }
}
