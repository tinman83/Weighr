using System;
using System.Collections.Generic;
using System.Text;

namespace WeighrDAL.Models
{
   public class ScaleSetting
    {
        public int ScaleSettingId { get; set; }
        public string DisplayUnits { get; set; }
        public double DisplayUnitsWeight { get; set; }
        public int DecimalPointPrecision { get; set; }
        public string DecimalPointPositionName { get; set; }
        public double ZeroRange { get; set; }
        public bool PrintMode { get; set; } // 0=Manual , 1=Auto

        public double MinimumDivision { get; set; }
        public double MaximumCapacity { get; set; }

        public double Density { get; set; }

        public decimal UpperLimit { get; set; }
        public decimal LowerLimit { get; set; }

        public double Inflight { get; set; }
    }
}
