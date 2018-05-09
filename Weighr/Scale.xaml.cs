using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Weighr.Models;
using Windows.Devices.Gpio;
using WeighrDAL.Components;
using WeighrDAL.Models;
using System.Threading;
using Windows.UI.Core;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Weighr
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Scale : Page
    {
        public decimal product_weight;
        public decimal current_weight;
        public decimal LoadcellOffset;

        private int clockPinNumber = 23;
        private int dataPinNumber = 24;
        private int ledPinNumber = 12;

        private int runPinNumber = 25;
        private int stopPinNumber = 8;
        private int estopPinNumber = 7;
        private int pressurePinNumber = 12;
        private int normalFeedPinNumber = 16;
        private int dribbleFeedPinNumber = 20;
        private int underWeightPinNumber = 21;
        private int overWeightPinNumber = 26;
        private int normalWeightPinNumber = 19;

        private GpioPin clockPin;
        private GpioPin dataPin;
        private GpioPin ledPin;

        private GpioPin runPin;
        private GpioPin stopPin;
        private GpioPin estopPin;
        private GpioPin pressurePin;
        private GpioPin normalFeedPin;
        private GpioPin dribbleFeedPin;
        private GpioPin underWeightPin;
        private GpioPin overWeightPin;
        private GpioPin normalWeightPin;

        double scale_gradient, y_intercept,calc_result, normal_feed_cutoff_percentage = 0.8, weight_display,threshold=20;
        decimal minimum_division, maximum_capacity, resolution,current_target_weight, current_upper_limit, current_lower_limit;
        decimal current_product_density;
        string display_units, current_product_code, curent_product_name;
        string current_status;
        int decimal_position,divider, weight, current_product_id;

        private Boolean checkWeightProcess = false;
        private Boolean runprocess = true, normal_cutoff_reached = false, final_setpoint_reached = false;

        IList<ScaleConfigComponent> scale_configurations = new List<ScaleConfigComponent>();
        IList<ScaleSettingComponent> scale_settings = new List<ScaleSettingComponent>();

        private DispatcherTimer timer;

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            //threshold = Convert.ToDouble(tbxTest.Text);
        }

        //private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        //{
        //    decimal val = Convert.ToDecimal(e.NewValue);
        //    string msg = val.ToString();
        //    tblSliderDisplay.Text = msg;
        //}

        private bool available = false;
        public Scale()
        {
            this.InitializeComponent();
            GetScaleConfigurations();
            GetScaleSettings();
            bool IsInitialised = InitialiseGpio();
            //GetCurrentProductDetails();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1); // Interval of the timer
            timer.Tick += timer_Tick;
            timer.Start();


            //Thread oThread = new Thread(new ThreadStart(weightReaderHandler));
            //oThread.Start();


        }

        private void timer_Tick(object sender, object e)
        {
            //read scale and update ui
            Int32 result = ReadData();
            calc_result = ((scale_gradient) * result) + y_intercept;
            //weight = Convert.ToInt32(calc_result * 100);
            //weight_display = weight / 100;
            //tblWeightDisplay.Text = weight_display.ToString();
            tblWeightDisplay.Text = calc_result.ToString("0.##"); // returns "0"  when decimalVar == 0

            if(calc_result >= threshold)
            {
                ledPin.Write(GpioPinValue.High);
            }
            else
            {
                ledPin.Write(GpioPinValue.Low);
            }
        }

        //public void ReadandUpdateUI()
        //{
           
        //        //read scale and update ui
        //        Int32 result = ReadData();
        //        calc_result = ((scale_gradient) * result) + y_intercept;
        //        weight = Convert.ToInt32(calc_result * 100);
        //        weight_display = weight / 100;
        //        tblWeightDisplay.Text = weight_display.ToString();

        //        if (runprocess == true)
        //        {
        //        normalFeedPin.Write(GpioPinValue.High);  //open normal feed valve
        //        dribbleFeedPin.Write(GpioPinValue.High);  //open dribble feed valve
        //        runprocess = false;
        //        checkWeightProcess = true;
        //        }

        //        if (checkWeightProcess == true)
        //        {
        //           if (normal_cutoff_reached == false)
        //           {
        //            //display filling status
        //            if (weight_display >= (current_target_weight * Convert.ToDecimal(normal_feed_cutoff_percentage)))  //normal feed cutoff reached?
        //              {
        //                normalFeedPin.Write(GpioPinValue.Low);  //close normal feed valve
        //                normal_cutoff_reached = true;
        //              }
        //           }
        //           else if(final_setpoint_reached == false)
        //           {
        //            //display filling status
        //            if(weight_display >= current_target_weight)
        //            {
        //                dribbleFeedPin.Write(GpioPinValue.Low);  //close dribble feed valve
        //                final_setpoint_reached = true;
        //            }

        //           }

        //          else
        //          {
        //            //display filling status
        //            checkWeightProcess = false;
                    
        //            if (weight_display > ((current_target_weight * current_upper_limit) + current_target_weight))   //if overpacked
        //            {
        //                overWeightPin.Write(GpioPinValue.High);  //switch on overweight light
        //                underWeightPin.Write(GpioPinValue.Low);
        //                normalWeightPin.Write(GpioPinValue.Low);
        //            }

        //            else if (weight_display < (current_target_weight - (current_target_weight * current_lower_limit) ))   //if underpacke
        //            {
        //                underWeightPin.Write(GpioPinValue.High);  //switch on underweight light
        //                overWeightPin.Write(GpioPinValue.Low);
        //                normalWeightPin.Write(GpioPinValue.Low);
        //            }

        //            else
        //            {
        //                normalWeightPin.Write(GpioPinValue.High);  //switch on normal weight light
        //                overWeightPin.Write(GpioPinValue.Low);
        //                underWeightPin.Write(GpioPinValue.Low);
        //            }
        //            TransactionLogComponent TransactionLogComp = new TransactionLogComponent();
        //            TransactionLog trans_log = new TransactionLog() { ProductId = current_product_id, ProductCode = current_product_code, ActualWeight = weight_display, TransactionDate = DateTime.Now, WeightDifference = weight_display-current_target_weight };
        //            TransactionLogComp.AddTransactionLog(trans_log);
        //            //

        //        }

        //        rctFillingGraphic.Height = Convert.ToDouble((weight_display / current_target_weight) * 200);



        //        }         

         
        //}

        public void GetScaleConfigurations()
        {
            ScaleConfigComponent ScaleConfigComp = new ScaleConfigComponent();

            var config = ScaleConfigComp.GetScaleConfig(1);

            scale_gradient = config.Gradient;
            y_intercept = config.YIntercept;
            minimum_division = config.MinimumDivision;
            maximum_capacity = config.MaximumCapacity;
            resolution = config.Resolution;     
        }

        public void GetScaleSettings()
        {
            ScaleSettingComponent ScaleSettingComp = new ScaleSettingComponent();

            var setting = ScaleSettingComp.GetScaleSetting(1);

            display_units = setting.DisplayUnits;
            decimal_position = setting.DecimalPointPosition;

            if(decimal_position == 0)
            {
                divider = 1;
            }
            else if(decimal_position == 1)
            {
                divider = 10;

            }
            else if(decimal_position == 2)
            {
                divider = 100;
            }

            else
            {
                divider = 1000;
            }
            //y_intercept = config.YIntercept;
            //minimum_division = config.MinimumDivision;
            //maximum_capacity = config.MaximumCapacity;
            //resolution = config.Resolution;
        }

        public void weightReaderHandler()
        {
            while (true)
            {
                if (runprocess == false) { break; }
                try
                {
                    Int32 result = ReadData();
                    double calc_result = ((-0.00000497) * result) - 1.15;

                    if (checkWeightProcess == true) {
                        UpdateWeightUI((calc_result).ToString());
                    }
                    
                    if (calc_result < 2.00)
                    {
                        ledPin.Write(GpioPinValue.High);
                    }
                    else
                    {
                        ledPin.Write(GpioPinValue.Low);
                    }

                }
                catch (Exception ex)
                {

                    throw ex;
                }
                

            }
        }
        private async void UpdateWeightUI(string weightResult)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                tblWeightDisplay.Text = weightResult;
            });
        }

        private int ReadData()
        {
            uint value = 0;
            byte[] data = new byte[4];

            // Wait for chip to become ready
            for (; GpioPinValue.Low != dataPin.Read();) ;

            // Clock in data
            data[1] = ShiftInByte();
            data[2] = ShiftInByte();
            data[3] = ShiftInByte();

            // Clock in gain of 128 for next reading
            clockPin.Write(GpioPinValue.High);
            clockPin.Write(GpioPinValue.Low);

            // Replicate the most significant bit to pad out a 32-bit signed integer
            if (0x80 == (data[1] & 0x80))
            {
                data[0] = 0xFF;
            }
            else
            {
                data[0] = 0x00;
            }

            // Construct a 32-bit signed integer
            value = (uint)((data[0] << 24) | (data[1] << 16) | (data[2] << 8) | data[3]);

            // Datasheet indicates the value is returned as a two's complement value
            // https://cdn.sparkfun.com/datasheets/Sensors/ForceFlex/hx711_english.pdf

            // flip all the bits
            value = ~value;

            // ... and add 1
            return (int)(++value);
        }

        private byte ShiftInByte()
        {
            byte value = 0x00;

            // Convert "GpioPinValue.High" and "GpioPinValue.Low" to 1 and 0, respectively.
            // NOTE: Loop is unrolled for performance
            clockPin.Write(GpioPinValue.High);
            value |= (byte)((byte)(dataPin.Read()) << 7);
            clockPin.Write(GpioPinValue.Low);
            clockPin.Write(GpioPinValue.High);
            value |= (byte)((byte)(dataPin.Read()) << 6);
            clockPin.Write(GpioPinValue.Low);
            clockPin.Write(GpioPinValue.High);
            value |= (byte)((byte)(dataPin.Read()) << 5);
            clockPin.Write(GpioPinValue.Low);
            clockPin.Write(GpioPinValue.High);
            value |= (byte)((byte)(dataPin.Read()) << 4);
            clockPin.Write(GpioPinValue.Low);
            clockPin.Write(GpioPinValue.High);
            value |= (byte)((byte)(dataPin.Read()) << 3);
            clockPin.Write(GpioPinValue.Low);
            clockPin.Write(GpioPinValue.High);
            value |= (byte)((byte)(dataPin.Read()) << 2);
            clockPin.Write(GpioPinValue.Low);
            clockPin.Write(GpioPinValue.High);
            value |= (byte)((byte)(dataPin.Read()) << 1);
            clockPin.Write(GpioPinValue.Low);
            clockPin.Write(GpioPinValue.High);
            value |= (byte)dataPin.Read();
            clockPin.Write(GpioPinValue.Low);

            return value;
        }



        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            btnEnter.IsEnabled = false;
           
            /** scale zeroing **/
            //LoadcellOffset = weight_display + LoadcellOffset;
            ScaleConfigComponent ScaleConfigComp = new ScaleConfigComponent();
            var new_settings = ScaleConfigComp.ZeroScale(LoadcellOffset);
            //  LoadcellOffset = new_settings.Offset;

            normal_cutoff_reached = false;
            final_setpoint_reached = false;
            runprocess = true;
          


        }

        public void GetCurrentProductDetails()
        {
            ProductComponent ProductComp = new ProductComponent();
        
            var product = ProductComp.GetCurrentProduct();

            current_product_id = product.ProductId;
            current_product_code = product.ProductCode;
            current_product_density = product.Density;
            curent_product_name = product.Name;
            current_target_weight = product.TargetWeight;
            current_upper_limit = product.UpperLimit;
            current_lower_limit = product.LowerLimit;



    }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            //print record
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            //checkWeightProcess = false;
        }

        private void btnZeroScale_Click(object sender, RoutedEventArgs e)
        {
            //threshold = Convert.ToDouble(tbxTest.Text);
        }

        private void btnTareScale_Click(object sender, RoutedEventArgs e)
        {
            Calibration calibrate = new Calibration();
            //bool result = calibrate.TareScale();
        }

        private void btnNetGross_Click(object sender, RoutedEventArgs e)
        {
            Calibration calibrate = new Calibration();
            //bool result = calibrate.NetGrossScale();
        }

        private bool InitialiseGpio()
        {
            GpioController gpio = GpioController.GetDefault();

            if (null == gpio)
            {
                available = false;
                return false;
            }
            /*
                * Initialize the clock pin and set to "Low"
                *
                * Instantiate the clock pin object
                * Write the GPIO pin value of low on the pin
                * Set the GPIO pin drive mode to output
                */
            clockPin = gpio.OpenPin(clockPinNumber, GpioSharingMode.Exclusive);
            clockPin.Write(GpioPinValue.Low);
            clockPin.SetDriveMode(GpioPinDriveMode.Output);

            ledPin = gpio.OpenPin(ledPinNumber, GpioSharingMode.Exclusive);
            ledPin.SetDriveMode(GpioPinDriveMode.Output);

            normalFeedPin = gpio.OpenPin(normalFeedPinNumber, GpioSharingMode.Exclusive);
            normalFeedPin.SetDriveMode(GpioPinDriveMode.Output);

            dribbleFeedPin = gpio.OpenPin(dribbleFeedPinNumber, GpioSharingMode.Exclusive);
            dribbleFeedPin.SetDriveMode(GpioPinDriveMode.Output);

            underWeightPin = gpio.OpenPin(underWeightPinNumber, GpioSharingMode.Exclusive);
            underWeightPin.SetDriveMode(GpioPinDriveMode.Output);

            overWeightPin = gpio.OpenPin(overWeightPinNumber, GpioSharingMode.Exclusive);
            overWeightPin.SetDriveMode(GpioPinDriveMode.Output);

            normalWeightPin = gpio.OpenPin(normalWeightPinNumber, GpioSharingMode.Exclusive);
            normalWeightPin.SetDriveMode(GpioPinDriveMode.Output);



            /*
                * Initialize the data pin and set to "Low"
                * 
                * Instantiate the data pin object
                * Set the GPIO pin drive mode to input for reading
                */
            dataPin = gpio.OpenPin(dataPinNumber, GpioSharingMode.Exclusive);
            dataPin.SetDriveMode(GpioPinDriveMode.Input);

            runPin = gpio.OpenPin(runPinNumber, GpioSharingMode.Exclusive);
            runPin.SetDriveMode(GpioPinDriveMode.Input);

            stopPin = gpio.OpenPin(stopPinNumber, GpioSharingMode.Exclusive);
            stopPin.SetDriveMode(GpioPinDriveMode.Input);

            estopPin = gpio.OpenPin(estopPinNumber, GpioSharingMode.Exclusive);
            estopPin.SetDriveMode(GpioPinDriveMode.Input);

            available = true;
            return true;
        }
    }
}
