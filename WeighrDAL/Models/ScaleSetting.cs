using System;
using System.Collections.Generic;
using System.Text;

namespace WeighrDAL.Models
{
   public class ScaleSetting
    {
        public int ScaleSettingId { get; set; }
        public string DisplayUnits { get; set; }
        public int DecimalPointPosition { get; set; }
        public string DecimalPointPositionName { get; set; }
        public decimal ZeroRange { get; set; }
        public bool PowerOnZero { get; set; } // 0=None, 1=Zero On Power
        public bool PrintMode { get; set; } // 0=Manual , 1=Auto
        public decimal ZeroTrackWidth { get; set; }
    }
}
