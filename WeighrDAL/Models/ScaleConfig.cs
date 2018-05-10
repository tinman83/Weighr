﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WeighrDAL.Models
{
   public class ScaleConfig
    {
        public int ScaleConfigId { get; set; }
        public Double Gradient { get; set; }
        public Double YIntercept { get; set; }
        public decimal Resolution { get; set; }
        public double offset { get; set; }
    }
}
