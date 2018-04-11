using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weighr.Models
{
    class Calibration
    {
        //
       
        public bool ZeroScale()
        {
            return true;
        }

        public bool TareScale()
        {
            return true;
        }

        public bool StoreCalibrationSettings(decimal RawMinimumValue, decimal UserMinimumValue, decimal RawMaximuValue, decimal UserMaximumValue, decimal Gradient, decimal YIntercept, decimal Offset)
        {
            //update database table with ScaleID == 1
            return true;
        }

        public bool CalibrateScale()
        {
            return true;
        }

        public bool NetGrossScale()
        {
            return true;
        }
    }
}
