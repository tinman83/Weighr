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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Weighr
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Calibration : Page
    {
        public decimal UserMinimumValue;
        public decimal LoadcellValueAtMinimum;
        public decimal LoadcellOffset, RawMinimumValue;
        public decimal UserMaximumValue, RawMaximumValue, Gradient, YIntercept;
        public Calibration()
        {
            this.InitializeComponent();
            tblCalibrationSteps.Text = "";
            tblCalibrationSteps.Text = "Step 1 - Unload Scale, Enter Minimum Value then Click CALIBRATE ZERO.";
        }

        private void MinimumDivisionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnSetMinimum_Click(object sender, RoutedEventArgs e)
        {
            //read Loadcell to get Raw Minimum Value
            UserMinimumValue = Convert.ToDecimal(tbxSetMinimum.Text); //Capture User Minimum
            LoadcellOffset = 0;//set offset to zero
            //store value in database
            tblCalibrationSteps.Text = "Step 2 - Load Scale, Enter Load Weight then Click CALIBRATE SPAN.";
        }

        private void btnSetMaximum_Click(object sender, RoutedEventArgs e)
        {
            //read Loadcell to get Raw Maximum Value
            UserMaximumValue = Convert.ToDecimal(tbxSetMaximum.Text); //Capture User Maximum
            //store value in database
            Gradient = (UserMaximumValue - UserMinimumValue) / (RawMaximumValue - RawMinimumValue);

            //y = mx +c  => c = y - mx
            YIntercept = UserMinimumValue - (Gradient * RawMinimumValue);

            CalibrationSettings settings = new CalibrationSettings();
            bool result = settings.StoreCalibrationSettings(RawMinimumValue, UserMaximumValue, RawMaximumValue, UserMaximumValue, Gradient, YIntercept, LoadcellOffset);

            tblCalibrationSteps.Text = "Calibration Successful";
        }
    }
}
