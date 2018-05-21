using System;
using System.Collections.Generic;
using System.Text;

namespace WeighrDAL.Models
{
   public class ScaleSetting
    {
        public int ScaleSettingId { get; set; }
        public string DisplayUnits { get; set; }
        public decimal DisplayUnitsWeight { get; set; }
        public int DecimalPointPrecision { get; set; }
        public string DecimalPointPositionName { get; set; }
        public decimal ZeroRange { get; set; }
        public bool PrintMode { get; set; } // 0=Manual , 1=Auto

        public decimal MinimumDivision { get; set; }
        public decimal MaximumCapacity { get; set; }

        public decimal Density { get; set; }

        public decimal UpperLimit { get; set; }
        public decimal LowerLimit { get; set; }

        public decimal Inflight { get; set; }
        public int InflightTiming { get; set; }

        public bool pushToCloud { get; set; }
        public string iotHubUri { get; set; }
        public string iotHubDeviceKey { get; set; }
        public bool pushToWebApi { get; set; }
        public string ServerUrl { get; set; }
    }
}
