using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WeighrDAL;
using WeighrDAL.Components;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Gpio;
using WeighrDAL.Models;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Weighr
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public decimal product_weight;
        public decimal current_weight;
        public decimal LoadcellOffset;

        //private int clockPinNumber = 23;
        //private int dataPinNumber = 24;
        //private int ledPinNumber = 12;

        //private int runPinNumber = 25;
        //private int stopPinNumber = 8;
        //private int estopPinNumber = 7;
        //private int pressurePinNumber = 16;
        //private int normalFeedPinNumber = 20;
        //private int dribbleFeedPinNumber = 21;
        //private int underWeightPinNumber = 26;
        //private int overWeightPinNumber = 19;
        //private int normalWeightPinNumber = 13;



        //private GpioPin clockPin;
        //private GpioPin dataPin;
        //private GpioPin ledPin;

        //private GpioPin runPin;
        //private GpioPin stopPin;
        //private GpioPin estopPin;
        //private GpioPin pressurePin;
        //private GpioPin normalFeedPin;
        //private GpioPin dribbleFeedPin;
        //private GpioPin underWeightPin;
        //private GpioPin overWeightPin;
        //private GpioPin normalWeightPin;

        /// <summary>
        /// Used to signal that the device is properly initialized and ready to use
        /// </summary>
        private bool available = false;

        
        public MainPage()
        {
            this.InitializeComponent();
            BackButton.Visibility = Visibility.Collapsed;
            initialiseTables();
            //bool IsInitialised = InitialiseGpio();

            //if (IsInitialised)
            //{

            //
            MyFrame.Navigate(typeof(Scale));
                TitleTextBlock.Text = "Scale";
                Scale.IsSelected = true;
               
            //}

            
            //Initialise settings to defaults
        }

        private void HamgurgerButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(Scale));
            TitleTextBlock.Text = "Scale";
            Scale.IsSelected = true;
        }



        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Scale.IsSelected)
            {
                BackButton.Visibility = Visibility.Collapsed;
                MyFrame.Navigate(typeof(Scale));
                TitleTextBlock.Text = "Scale";
            }
            else if (Product.IsSelected)
            {
                BackButton.Visibility = Visibility.Visible;
                MyFrame.Navigate(typeof(Product));
                TitleTextBlock.Text = "Product";
            }
            else if (Settings.IsSelected)
            {
                BackButton.Visibility = Visibility.Visible;
                MyFrame.Navigate(typeof(Settings));
                TitleTextBlock.Text = "Settings";
            }
            else if (Shift.IsSelected)
            {
                BackButton.Visibility = Visibility.Visible;
                MyFrame.Navigate(typeof(Shift));
                TitleTextBlock.Text = "Shift";
            }
            else if (Reports.IsSelected)
            {
                BackButton.Visibility = Visibility.Visible;
                MyFrame.Navigate(typeof(Reports));
                TitleTextBlock.Text = "Reports";
            }
            else if (Calibration.IsSelected)
            {
                BackButton.Visibility = Visibility.Visible;
                MyFrame.Navigate(typeof(Calibration));
                TitleTextBlock.Text = "Calibration";
            }
        }

        private void initialiseTables()
        {
            /*** Initialise Calibration Settings***/
            ScaleConfigComponent ScaleConfigComp = new ScaleConfigComponent();

            var config = ScaleConfigComp.GetScaleConfig(1);

            ScaleConfig scaleCon = new ScaleConfig() { Gradient = -0.00000497, YIntercept = -1.15, MaximumCapacity = 100, MinimumDivision = 1, Resolution = 200 };

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
            var setting = ScaleSettigComp.GetScaleSetting(1);

            ScaleSetting scale_setting = new ScaleSetting() {
                DisplayUnits = "Kgs",
                DecimalPointPrecision = 2
            };
            
            if(setting == null)
            {
                ScaleSettigComp.AddScaleSetting(scale_setting);
            }
            else
            {
                scale_setting.ScaleSettingId = 1;
                ScaleSettigComp.UpdateScaleSetting(scale_setting);
            }



        }


        //private bool InitialiseGpio()
        //{
        //    GpioController gpio = GpioController.GetDefault();

        //    if (null == gpio)
        //    {
        //        available = false;
        //        return false;
        //    }
        //    /*
        //        * Initialize the clock pin and set to "Low"
        //        *
        //        * Instantiate the clock pin object
        //        * Write the GPIO pin value of low on the pin
        //        * Set the GPIO pin drive mode to output
        //        */
        //    clockPin = gpio.OpenPin(clockPinNumber, GpioSharingMode.Exclusive);
        //    clockPin.Write(GpioPinValue.Low);
        //    clockPin.SetDriveMode(GpioPinDriveMode.Output);

        //    ledPin = gpio.OpenPin(ledPinNumber, GpioSharingMode.Exclusive);
        //    ledPin.SetDriveMode(GpioPinDriveMode.Output);

        //    normalFeedPin = gpio.OpenPin(normalFeedPinNumber, GpioSharingMode.Exclusive);
        //    normalFeedPin.SetDriveMode(GpioPinDriveMode.Output);

        //    dribbleFeedPin = gpio.OpenPin(dribbleFeedPinNumber, GpioSharingMode.Exclusive);
        //    dribbleFeedPin.SetDriveMode(GpioPinDriveMode.Output);

        //    underWeightPin = gpio.OpenPin(underWeightPinNumber, GpioSharingMode.Exclusive);
        //    underWeightPin.SetDriveMode(GpioPinDriveMode.Output);

        //    overWeightPin = gpio.OpenPin(overWeightPinNumber, GpioSharingMode.Exclusive);
        //    overWeightPin.SetDriveMode(GpioPinDriveMode.Output);

        //    normalWeightPin = gpio.OpenPin(normalWeightPinNumber, GpioSharingMode.Exclusive);
        //    normalWeightPin.SetDriveMode(GpioPinDriveMode.Output);



        //    /*
        //        * Initialize the data pin and set to "Low"
        //        * 
        //        * Instantiate the data pin object
        //        * Set the GPIO pin drive mode to input for reading
        //        */
        //    dataPin = gpio.OpenPin(dataPinNumber, GpioSharingMode.Exclusive);
        //    dataPin.SetDriveMode(GpioPinDriveMode.Input);

        //    runPin = gpio.OpenPin(runPinNumber, GpioSharingMode.Exclusive);
        //    runPin.SetDriveMode(GpioPinDriveMode.Input);

        //    stopPin = gpio.OpenPin(stopPinNumber, GpioSharingMode.Exclusive);
        //    stopPin.SetDriveMode(GpioPinDriveMode.Input);

        //    estopPin = gpio.OpenPin(estopPinNumber, GpioSharingMode.Exclusive);
        //    estopPin.SetDriveMode(GpioPinDriveMode.Input);

        //    available = true;
        //    return true;
        //}


    }
}
