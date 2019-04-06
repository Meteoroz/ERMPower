using System;
namespace ERM_Models.Models
{
    public class TOU : CsvBase
    {
        public decimal Energy { get; set; }
        public decimal MaximumDemand { get; set; }
        public DateTime TimeofMaxDemand { get; set; }
        public string Period { get; set; }
        public bool DLSActive { get; set; }
        public int BillingResetCount { get; set; }
        public DateTime BillingResetDateTime { get; set; }
        public string Rate { get; set; }
    }
}
