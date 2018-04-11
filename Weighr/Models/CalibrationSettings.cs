using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;



namespace Weighr.Models
{
    class CalibrationSettings
    {
        
        public bool StoreCalibrationSettings(decimal RawMinimumValue, decimal UserMinimumValue, decimal RawMaximumValue, decimal UserMaximumValue, decimal Gradient, decimal YIntercept, decimal Offset)
        {
            //update database table with ScaleID == 1
            return true;
        }

        public bool ZeroScale(int scale_id, decimal offset)
        {
            //update  offset to zero scale
            return true;
        }

        public string GetCalibrationSettings(int scale_id)
        {
            return "In real app we return a List of settings";
        }
    }
}
