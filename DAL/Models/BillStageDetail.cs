using System;

namespace DAL.Models
{
    public class BillStageDetail
    {
        public long Id { get; set; }
        public long BillId { get; set; }
        public int BillStageId { get; set; }
        public DateTime? DateStageOccurred { get; set; }

        public Bill Bill { get; set; }
        public BillStage BillStage { get; set; }
    }
}
