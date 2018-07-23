using System;

namespace DAL.Models
{
    public class BillStageDetail
    {
        public long Id { get; set; }
        public long StageId { get; set; }
        public int BillStageId { get; set; }
        public DateTime DateStageOccurred { get; set; }
        public string LinkUri { get; set; }
    }
}
