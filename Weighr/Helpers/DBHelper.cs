using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeighrDAL.Components;
using WeighrDAL.Models;

namespace Weighr.Helpers
{
   public class DBHelper
    {

        public void zeroScale()
        {

        }
      
        public void InitialiseTables()
        {
            /*** Initialise Calibration Settings***/
            ScaleConfigComponent ScaleConfigComp = new ScaleConfigComponent();

            var config = ScaleConfigComp.GetScaleConfig();

            ScaleConfig scaleCon = new ScaleConfig() { Gradient = -0.00000497M, Resolution = 200, YIntercept = -1.15M, offset = 0 };

            if (config == null)
            {
                ScaleConfigComp.AddScaleConfig(scaleCon);
            }
            else
            {
                scaleCon.ScaleConfigId = 1;
                ScaleConfigComp.UpdateScaleConfig(scaleCon);
            }

            /*** Initialise Scale Settings***/
            ScaleSettingComponent ScaleSettigComp = new ScaleSettingComponent();
            var setting = ScaleSettigComp.GetScaleSetting();

            ScaleSetting scale_setting = new ScaleSetting()
            {
                DisplayUnits = "Kgs",
                DecimalPointPrecision = 2,
                Density = 1,
                DisplayUnitsWeight = 1,
                MaximumCapacity = 100,
                MinimumDivision = 0.5M,
                PrintMode = false,
                ZeroRange = 2,
                UpperLimit=2,
                LowerLimit=2,
                Inflight=0.3M
            };

            if (setting == null)
            {
                ScaleSettigComp.AddScaleSetting(scale_setting);
            }
            else
            {
                scale_setting.ScaleSettingId = 1;
                ScaleSettigComp.UpdateScaleSetting(scale_setting);
            }

            /*** Initialise Product***/
            ProductComponent productComp = new ProductComponent();
            var p = productComp.GetProducts();

            if (p.Count() == 0)
            {

                WeighrDAL.Models.Product productA = new WeighrDAL.Models.Product()
                {
                    ProductCode = "PROA",
                    Name = "Product A - 5Kg",
                    Density = 1,
                    Inflight = 0.3M,
                    isCurrent = false,
                    LowerLimit = 1,
                    UpperLimit = 1,
                    TargetWeight = 5
                };

                WeighrDAL.Models.Product productB = new WeighrDAL.Models.Product()
                {
                    ProductCode = "PROB",
                    Name = "Product B - 20Kg",
                    Density = 1,
                    Inflight = 0.3M,
                    isCurrent = false,
                    LowerLimit = 2,
                    UpperLimit = 2,
                    TargetWeight = 20
                };


                WeighrDAL.Models.Product productC = new WeighrDAL.Models.Product()
                {
                    ProductCode = "PROC",
                    Name = "Product C - 10Kg",
                    Density = 0,
                    Inflight = 0.3M,
                    isCurrent = false,
                    LowerLimit = 2,
                    UpperLimit = 2,
                    TargetWeight = 10
                };

                productComp.AddProduct(productA);
                productComp.AddProduct(productB);
                productComp.AddProduct(productC);

            }

        }
    }
}
