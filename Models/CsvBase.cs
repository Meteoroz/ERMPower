using System;
namespace ERM_Models.Models
{
    public abstract class CsvBase
    {
        public int MeterPointCode { get; set; }
        public int SerialNumber { get; set; }
        public string PlantCode { get; set; }
        public DateTime DateTime { get; set; }
        public string DataType { get; set; }
        public string Status { get; set; }

    }
}
