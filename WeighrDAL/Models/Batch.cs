using System;
using System.Collections.Generic;
using System.Text;

namespace WeighrDAL.Models
{
   public class Batch
    {
        public int BatchId { get; set; }
        public string BatchCode { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool isCurrent { get; set; }

    }
}
