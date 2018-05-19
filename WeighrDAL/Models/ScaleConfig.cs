using System;
using System.Collections.Generic;
using System.Text;

namespace WeighrDAL.Models
{
   public class ScaleConfig
    {
        public int ScaleConfigId { get; set; }
        public decimal Gradient { get; set; }
        public decimal YIntercept { get; set; }
        public decimal Resolution { get; set; }
        public decimal offset { get; set; }
    }
}
