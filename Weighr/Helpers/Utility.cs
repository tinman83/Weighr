using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Weighr.Helpers
{
   public static class Utility
    {
        public static double GetBaseUnitConversionFactor(string unitName)
        {

            switch (unitName)
            {
                case "Grams":
                    return 1000;
                case "Kgs":
                    return 1;
                case "Tonnes":
                    return 0.001;
                default:
                    return 1;
            }
        }
    }
}
