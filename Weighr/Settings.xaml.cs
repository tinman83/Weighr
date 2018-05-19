using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WeighrDAL.Components;
using WeighrDAL.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
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

            ScaleSettingComponent scaleSettingComp = new ScaleSettingComponent();

            _scaleSetting = scaleSettingComp.GetScaleSettingDefault();

            DisplayUnitsComboBox.SelectedIndex = GetUnitIndex(_scaleSetting.DisplayUnits);
            DecimalPointPrecisionComboBox.SelectedValue = _scaleSetting.DecimalPointPrecision.ToString();
            ZeroRangeComboBox.SelectedValue = _scaleSetting.ZeroRange.ToString();

            string printMode;
            if (_scaleSetting.PrintMode == false) { printMode = "Manual"; } else { printMode = "Auto"; }
            PrintModeComboBox.SelectedValue = printMode;
            MinimumDivisionComboBox.SelectedValue = _scaleSetting.MinimumDivision.ToString();
            //MaximumCapacitySlider.Value = _scaleSetting.MaximumCapacity;
            //densitySlider.Value = _scaleSetting.Density;

            tblMaximumCapacity.Text = "Current Max Cap: " + _scaleSetting.MaximumCapacity.ToString();
            tblSliderDisplay.Text = "Current Density: " + _scaleSetting.Density.ToString();
            //UpperLimitComboBox.SelectedValue = _scaleSetting.UpperLimit.ToString();
            //LowerLimitComboBox.SelectedValue = _scaleSetting.LowerLimit.ToString();
            InflightComboBox.SelectedValue = _scaleSetting.Inflight.ToString();

        }

        private void btnSaveSettings_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateSettingsInput() == false)
            {
                return;
            }

            _scaleSetting.DisplayUnits = DisplayUnitsComboBox.SelectedValue.ToString();
            _scaleSetting.DecimalPointPrecision = int.Parse(DecimalPointPrecisionComboBox.SelectedValue.ToString());
            _scaleSetting.ZeroRange = decimal.Parse(ZeroRangeComboBox.SelectedValue.ToString());

            string mode = PrintModeComboBox.SelectedValue.ToString();
            if (mode == "Auto")
            {
                _scaleSetting.PrintMode =false;
            }
            else
            {
                _scaleSetting.PrintMode = false;
            }
            
            _scaleSetting.MinimumDivision = decimal.Parse(MinimumDivisionComboBox.SelectedValue.ToString());
            _scaleSetting.MaximumCapacity = Convert.ToDecimal(MaximumCapacitySlider.Value);
            _scaleSetting.Density = Convert.ToDecimal(densitySlider.Value);
           // _scaleSetting.UpperLimit = decimal.Parse(UpperLimitComboBox.SelectedValue.ToString());
           // _scaleSetting.LowerLimit = decimal.Parse(LowerLimitComboBox.SelectedValue.ToString());
            //_scaleSetting.Inflight = double.Parse(InflightComboBox.SelectedValue.ToString());

            ScaleSettingComponent scaleSettingComp = new ScaleSettingComponent();

            scaleSettingComp.UpdateScaleSetting(_scaleSetting);


        }

        private bool ValidateSettingsInput()
        {
            //double num;

            //if (MinimumDivisionTextBox.Text == "") { ErrorMessage.Text = "Please enter Minimum division value"; return false; }

            //if (!double.TryParse(MinimumDivisionTextBox.Text, out num))
            //{
            //    ErrorMessage.Text = "Please enter Numeric Minimum division value"; return false;
            //}

            //if (MaximumCapacityTextBox.Text == "") { ErrorMessage.Text = "Please enter  Maximum Capacity value"; return false; }
            //if (!double.TryParse(MaximumCapacityTextBox.Text, out num))
            //{
            //    ErrorMessage.Text = "Please enter Numeric Maximum Capacity value"; return false;
            //}

            if (InflightComboBox.SelectedValue == null) { ErrorMessage.Text = "Please select inflight"; return false; }
            
            return true;
        }

        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            decimal val = Convert.ToDecimal(e.NewValue);
            string msg = "Current Density: " + val.ToString("0.00");
            tblSliderDisplay.Text = msg;
        }

        private void MaximumCapacitySlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            decimal val = Convert.ToDecimal(e.NewValue);
            string msg = "Current Max Cap: " +val.ToString("0.00");
            tblMaximumCapacity.Text = msg;
        }
    }
}
