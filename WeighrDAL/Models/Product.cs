﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WeighrDAL.Models
{
   public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ProductId { get; set; }
        public Guid rowguid { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public decimal Density { get; set; }
        public decimal UpperLimit { get; set; }
        public decimal LowerLimit { get; set; }
        public decimal TargetWeight { get; set; }
        public decimal Inflight { get; set; }

        public decimal DribblePoint { get; set; }
        public decimal ExpectedFillTime { get; set; }
        public bool isCurrent { get; set; }

        public DateTime ModifiedDate { get; set; }

    }
}
