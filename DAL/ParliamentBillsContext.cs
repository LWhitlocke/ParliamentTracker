using System.Data.Entity;
using DAL.Models;

namespace DAL
{
    public class ParliamentBillsContext : DbContext
    {
        public ParliamentBillsContext() : base()
        {

        }

        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillStage> BillStages { get; set; }
        public DbSet<BillStageDetail> BillStageDetails { get; set; }
        public DbSet<CrawlBillDetail> CrawlBillDetails { get; set; }
        public DbSet<CrawlDetail> CrawlDetails { get; set; }
    }
}
