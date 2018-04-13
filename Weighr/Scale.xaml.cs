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
        private GpioPin clockPin;
        private GpioPin dataPin;
        private GpioPin ledPin;

        double scale_gradient, y_intercept;
        decimal minimum_division, maximum_capacity, resolution;

        IList<ScaleConfigComponent> scale_configurations = new List<ScaleConfigComponent>();
        IList<ScaleSettingComponent> scale_settings = new List<ScaleSettingComponent>();

        public Scale()
        {
            this.InitializeComponent();
            GetScaleConfigurations();
            GetScaleSettings();
            //Get scale configuration
            //Get scale settings

            Thread oThread = new Thread(new ThreadStart(weightReaderHandler));
            oThread.Start();


        }

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
            //ScaleSettingComponent ScaleSettingComp = new ScaleSettingComponent();

            //var setting = ScaleSettingComp.GetScaleSetting(1);

            //scale_gradient = setting.;
            //y_intercept = config.YIntercept;
            //minimum_division = config.MinimumDivision;
            //maximum_capacity = config.MaximumCapacity;
            //resolution = config.Resolution;
        }



        public void weightReaderHandler()
        {
            while (true)
            {
                Int32 result = ReadData();
                double calc_result = ((-0.00000497) * result) - 1.15;
                tblWeightDisplay.Text = calc_result.ToString();

                if (calc_result < 2.00)
                {
                    ledPin.Write(GpioPinValue.High);
                }
                else
                {
                    ledPin.Write(GpioPinValue.Low);
                }



            }
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
            //record initial scale weight
            //start necessary valves and reading continously
            //if weight reaches target - free fall stop
            //record on database
            //check if auto print is on and then print if it is

        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            //print record
        }

        private void btnZeroScale_Click(object sender, RoutedEventArgs e)
        {
            
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
    }
}
