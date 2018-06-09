using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WeighrDAL.Components;
using WeighrDAL.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Weighr
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Settings : Page
    {
        ScaleSetting _scaleSetting = new ScaleSetting();
        DeviceInfo _deviceInfo = new DeviceInfo();

        private enum Units
        {
            Grams,
            Kgs,
            Tonnes
        }
        private int GetUnitIndex(string unit)
        {
            switch (unit)
            {
                case "Grams":
                    return 0;
                case "Kgs":
                    return 1;
                case "Tonnes":
                    return 2;
                default:
                    return 1;
            }
        }
        public Settings()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            DeviceInfoComponent deviceInfoComp = new DeviceInfoComponent();
            _deviceInfo = deviceInfoComp.GetDeviceInfo();

            if (_deviceInfo != null)
            {
                PlantNameTextBox.Text = _deviceInfo.PlantId;
                MachineNameTextBox.Text = _deviceInfo.MachineName;
            }

            ScaleSettingComponent scaleSettingComp = new ScaleSettingComponent();

            _scaleSetting = scaleSettingComp.GetScaleSettingDefault();

            if (_scaleSetting != null)
            {
                DisplayUnitsComboBox.SelectedIndex = GetUnitIndex(_scaleSetting.DisplayUnits);

                PrecisionTextBox.Text = _scaleSetting.MinimumDivision.ToString();
                InflightTextBox.Text = _scaleSetting.Inflight.ToString();
                InflightTimingTextBox.Text = _scaleSetting.InflightTiming.ToString();
                MaximumCapacityTextBox.Text = _scaleSetting.MaximumCapacity.ToString();


            }
            else
            {
                _scaleSetting = new ScaleSetting();
            }

           

        }

        private void btnSaveSettings_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateSettingsInput() == false)
            {
                return;
            }

            _scaleSetting.DisplayUnits = DisplayUnitsComboBox.SelectedValue.ToString();
            _scaleSetting.DecimalPointPrecision = 0; //int.Parse(DecimalPointPrecisionComboBox.SelectedValue.ToString());
            _scaleSetting.ZeroRange = 0; // decimal.Parse(ZeroRangeComboBox.SelectedValue.ToString());

            
            _scaleSetting.MinimumDivision = decimal.Parse(PrecisionTextBox.Text);
            _scaleSetting.MaximumCapacity = Convert.ToDecimal(MaximumCapacityTextBox.Text);
            _scaleSetting.Density = 1;
            _scaleSetting.Inflight = decimal.Parse(InflightTextBox.Text);
            _scaleSetting.InflightTiming = int.Parse(InflightTimingTextBox.Text);

            ScaleSettingComponent scaleSettingComp = new ScaleSettingComponent();

            MainPage._inflight_setpoint = _scaleSetting.Inflight; 

            scaleSettingComp.UpdateScaleSetting(_scaleSetting);

            string msg = "Scale Settings successfully saved";
            saveSuccessfullmessage(msg);


        }

        private bool ValidateSettingsInput()
        {

            double num;
            int numInt;

            if (PrecisionTextBox.Text == "") { errorMessageShow("Please enter precision"); return false; }
            if (!int.TryParse(PrecisionTextBox.Text, out numInt))
            {
                errorMessageShow("Please enter integer value for precision"); return false;
            }

            if (InflightTextBox.Text == "") { errorMessageShow("Please enter inflight"); return false; }
            if (!double.TryParse(InflightTextBox.Text, out num))
            {
                errorMessageShow("Please enter numeric value for inflight"); return false;
            }

            if (InflightTimingTextBox.Text == "") { errorMessageShow("Please enter inflight timing"); return false; }
            if (!int.TryParse(InflightTimingTextBox.Text, out numInt))
            {
                errorMessageShow("Please enter integer value for inflight timing"); return false;
            }

            if (MaximumCapacityTextBox.Text == "") { errorMessageShow("Please enter the maximum capacity"); return false; }
            if (!double.TryParse(MaximumCapacityTextBox.Text, out num))
            {
                errorMessageShow("Please enter numeric value for the maximum capacity"); return false;
            }

            return true;
        }

        private bool ValidateDeviceInfoInput()
        {

            if (PlantNameTextBox.Text == "") { errorMessageShow("Please enter plant name"); return false; }
           
            if (MachineNameTextBox.Text == "") { errorMessageShow("Please enter machine name"); return false; }
            
            return true;
        }


        private void btnSaveDeviceInfo_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateDeviceInfoInput() == false)
            {
                return;
            }

           
            DeviceInfoComponent deviceInfoComp = new DeviceInfoComponent();

            var deviceInfo = deviceInfoComp.GetDeviceInfo();

            deviceInfo.PlantId = PlantNameTextBox.Text;
            deviceInfo.MachineName = MachineNameTextBox.Text;

            deviceInfoComp.UpdateDeviceInfo(deviceInfo);

            string msg = "Device information saved";
            saveSuccessfullmessage(msg);
        }

        private async void saveSuccessfullmessage(string message)
        {
            var dialog = new MessageDialog(message,"Information");

            dialog.Commands.Clear();
            dialog.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
            var res = await dialog.ShowAsync();

        }

        private async void errorMessageShow(string message)
        {
            var dialog = new MessageDialog(message,"Validation Error");

            dialog.Commands.Clear();
            dialog.Commands.Add(new UICommand { Label = "Ok", Id = 0 });

            var res = await dialog.ShowAsync();

        }

    }
}
