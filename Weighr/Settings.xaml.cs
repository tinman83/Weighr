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
            DecimalPointPrecisionComboBox.SelectedValue = _scaleSetting.DecimalPointPrecision;
            ZeroRangeComboBox.SelectedValue = _scaleSetting.ZeroRange;

            string printMode;
            if (_scaleSetting.PrintMode == false) { printMode = "Manual"; } else { printMode = "Auto"; }
            PrintModeComboBox.SelectedValue = printMode;
            MinimumDivisionTextBox.Text = _scaleSetting.MinimumDivision.ToString();
            MinimumDivisionTextBox.Text = _scaleSetting.MaximumCapacity.ToString();
            densitySlider.Value = _scaleSetting.Density;

        }

        private void btnSaveSettings_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateSettingsInput() == false)
            {
                return;
            }

            _scaleSetting.DisplayUnits = DisplayUnitsComboBox.SelectedValue.ToString();
            _scaleSetting.DecimalPointPrecision = int.Parse(DecimalPointPrecisionComboBox.SelectedValue.ToString());
            _scaleSetting.ZeroRange = double.Parse(ZeroRangeComboBox.SelectedValue.ToString());
            _scaleSetting.PrintMode = bool.Parse(PrintModeComboBox.SelectedValue.ToString());

            _scaleSetting.MinimumDivision = double.Parse(MinimumDivisionTextBox.Text);
            _scaleSetting.MaximumCapacity = double.Parse(MaximumCapacityTextBox.Text);
            _scaleSetting.Density = densitySlider.Value;

            ScaleSettingComponent scaleSettingComp = new ScaleSettingComponent();

            scaleSettingComp.UpdateScaleSetting(_scaleSetting);


        }

        private bool ValidateSettingsInput()
        {
            double num;

            if (MinimumDivisionTextBox.Text == "") { ErrorMessage.Text = "Please enter Minimum division value"; return false; }
            
            if (!double.TryParse(MinimumDivisionTextBox.Text, out num))
            {
                ErrorMessage.Text = "Please enter Numeric Minimum division value"; return false;
            }

            if (MaximumCapacityTextBox.Text == "") { ErrorMessage.Text = "Please enter  Maximum Capacity value"; return false; }
            if (!double.TryParse(MaximumCapacityTextBox.Text, out num))
            {
                ErrorMessage.Text = "Please enter Numeric Maximum Capacity value"; return false;
            }

            return true;
        }

        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            decimal val = Convert.ToDecimal(e.NewValue);
            string msg = val.ToString();
            tblSliderDisplay.Text = msg;
        }
    }
}
