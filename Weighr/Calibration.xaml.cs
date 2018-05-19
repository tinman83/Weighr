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
using Weighr.Helpers;
using WeighrDAL.Models;
using WeighrDAL.Components;
using System.Globalization;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Weighr
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Calibration : Page
    {
        public decimal _UserMinimumValue;
        public decimal _LoadcellValueAtMinimum;
        public decimal _LoadcellOffset;
        public decimal _RawMinimumValue;
        public decimal _UserMaximumValue, _RawMaximumValue;
        public decimal _Gradient, _YIntercept;

        public decimal _MinimumValue, _MaximumValue;
        Weighr.Models.Calibration _calibrate = new Weighr.Models.Calibration();

        public Calibration()
        {
            this.InitializeComponent();
            //tblCalibrationSteps.Text = "";
            //tblCalibrationSteps.Text = "Step 1 - Unload Scale, Enter Minimum Value then Click CALIBRATE ZERO.";
            
        }

        private void SetMinimumButton_Click(object sender, RoutedEventArgs e)
        {
            if (SetMinimumTextBox.Text == "") { return; }
            //read Loadcell to get Raw Minimum Value
            //if (SetMinimumTextBox.Text == "0")
            //{
            //    _UserMinimumValue = 0.00;
            //}
            //else
            //{

            //}
           


            _UserMinimumValue = Convert.ToDecimal(SetMinimumTextBox.Text); //Capture User Minimum
            _RawMinimumValue = GpioUtility.ReadData();
            _LoadcellOffset = 0;//set offset to zero
        }

        private void SetMaximumButton_Click(object sender, RoutedEventArgs e)
        {
            if (SetMaximumTextBox.Text == "") { return; }
            //read Loadcell to get Raw Maximum Value

            _UserMaximumValue = Convert.ToDecimal(SetMaximumTextBox.Text); //Capture User Maximum
            //_MaximumValue = Convert.ToDecimal(SetMaximumTextBox.Text); //Capture User Minimum
            _RawMaximumValue = GpioUtility.ReadData();

        }

        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {

            _Gradient = (_UserMaximumValue - _UserMinimumValue) / (_RawMaximumValue - _RawMinimumValue);


            //y = mx +c  => c = y - mx
            _YIntercept = _UserMinimumValue - (_Gradient * _RawMinimumValue);

            ScaleConfigComponent scaleConfigComp = new ScaleConfigComponent();
            var scaleConfig = scaleConfigComp.GetScaleConfig();

            scaleConfig.Gradient = _Gradient;
            scaleConfig.YIntercept = _YIntercept;
            scaleConfig.offset = _LoadcellOffset;

            scaleConfigComp.UpdateScaleConfig(scaleConfig);

            messageCalibrationResult.Text = "Calibration Successful";
        }


    }
}
