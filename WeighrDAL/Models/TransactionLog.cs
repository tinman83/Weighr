using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WeighrDAL.Models
{
   public class TransactionLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long TransactionLogId { get; set; }
        public Guid rowguid { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string OrderNumber { get; set; }

        [ForeignKey("Batches")]
        public string BatchCode { get; set; }

        [ForeignKey("Shift")]
        public int ShiftId { get; set; }
        public decimal ActualWeight { get; set; }
        public decimal TargetWeight { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal WeightDifference { get; set; }
        public int WeightStatus { get; set; }  // -1=UnderWeight , 0=Normal , 1=OverWeight
        public string SerialNumber { get; set; }
        public string DeviceId { get; set; }
        public string WeightType { get; set; } //NET OR GROSS

        public decimal ProductDensity { get; set; }

        public string Units { get; set; }

        public decimal ExpectedFillTime { get; set; }
        public decimal ActualFillTime { get; set; }
        public decimal PercDiffFillTime { get; set; }

        public string ClientId { get; set; }
        public string PlantId { get; set; }
        public string MachineName { get; set; }
        public string BaseUnitOfMeasure { get; set; }
        public bool Uploaded { get; set; }
        public DateTime DateUploaded { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool persistedServer { get; set; }

    }
}
