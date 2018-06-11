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
using Weighr.Helpers;
using System.Threading.Tasks;
using SDKTemplate;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Weighr
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage Current;

        public decimal product_weight;
        public decimal current_weight;
        public decimal LoadcellOffset;
        public static decimal _inflight_setpoint=0;

        /// <summary>
        /// Used to signal that the device is properly initialized and ready to use
        /// </summary>
        private bool available = false;

        public Batch _batch;
        DeviceInfo _deviceInfo = new DeviceInfo();

        public MainPage()
        {
            this.InitializeComponent();
            BackButton.Visibility = Visibility.Collapsed;
            
            MyFrame.Navigate(typeof(Scale));
                TitleTextBlock.Text = "Scale";
                Scale.IsSelected = true;
               

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            bool IsInitialised = GpioUtility.InitialiseGpio();

            DeviceInfoComponent deviceInfoComp = new DeviceInfoComponent();
            //deviceInfoComp.DeleteDeviceInfo();
            _deviceInfo = deviceInfoComp.GetDeviceInfo();
            if (_deviceInfo == null)
            {
                //string key = CreateDeviceFunc(DeviceInfoHelper.Instance.SerialNumber).Result;
                _deviceInfo = new DeviceInfo
                {
                    Model = DeviceInfoHelper.Instance.Model,
                    Manufacturer = DeviceInfoHelper.Instance.Manufacturer,
                    Name = DeviceInfoHelper.Instance.Id,
                    OSName = DeviceInfoHelper.OSName,
                    SerialNumber = DeviceInfoHelper.Instance.SerialNumber,
                    iotHubDeviceKey = DeviceInfoHelper.Instance.iotHubDeviceKey,
                    ClientId = DeviceInfoHelper.Instance.ClientId,
                    pushToCloud = DeviceInfoHelper.Instance.pushToCloud,
                    iotHubUri = DeviceInfoHelper.Instance.iotHubUri,
                    pushToWebApi = DeviceInfoHelper.Instance.pushToWebApi,
                    ServerUrl = DeviceInfoHelper.Instance.ServerUrl,
                    PlantId= DeviceInfoHelper.Instance.PlantId,
                    MachineName= DeviceInfoHelper.Instance.MachineName
                };

                deviceInfoComp.AddDeviceInfo(_deviceInfo);
            }
            
            BatchComponent batchComp = new BatchComponent();
            _batch = batchComp.GetCurrentBatch();
            //DBHelper dbhelper = new DBHelper();
            //dbhelper.InitialiseTables();
        }

        public async Task<string> CreateDeviceFunc(string deviceId)
        {
            await MyDevice.AddDeviceAsync(deviceId);
            string key = MyDevice.device_key;

            return key;
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
            else if (Diagnostics.IsSelected)
            {
                BackButton.Visibility = Visibility.Visible;
                MyFrame.Navigate(typeof(WeighrDiagnostics));
                TitleTextBlock.Text = "Diagnostics";
            }
            else if (Reports.IsSelected)
            {
                BackButton.Visibility = Visibility.Visible;
                MyFrame.Navigate(typeof(Reports));
                TitleTextBlock.Text = "Reports";
            }
            //else if (Shift.IsSelected)
            //{
            //    BackButton.Visibility = Visibility.Visible;
            //    MyFrame.Navigate(typeof(Shift));
            //    TitleTextBlock.Text = "Shift";
            //}

            else if (Calibration.IsSelected)
            {
                BackButton.Visibility = Visibility.Visible;
                MyFrame.Navigate(typeof(Calibration));
                TitleTextBlock.Text = "Calibration";
            }
        }

        public void NotifyUser(string strMessage, NotifyType type)
        {
            switch (type)
            {
                case NotifyType.StatusMessage:
                    StatusBorder.Background = new SolidColorBrush(Windows.UI.Colors.Green);
                    break;
                case NotifyType.ErrorMessage:
                    StatusBorder.Background = new SolidColorBrush(Windows.UI.Colors.Red);
                    break;
            }
            StatusBlock.Text = strMessage;

            //Collapse the StatusBlock if it has no text to conserve real estate.
           StatusBorder.Visibility = (StatusBlock.Text != String.Empty) ? Visibility.Visible : Visibility.Collapsed;
        }

    }

    public enum NotifyType
    {
        StatusMessage,
        ErrorMessage
    };

}
