using System.Collections.Generic;

namespace DAL.Models
{
    public class BillStage
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<BillStageDetail> BillStageDetails { get; set; }
    }
}
