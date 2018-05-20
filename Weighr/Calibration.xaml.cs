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
using Windows.UI.Popups;

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

        public ScaleConfig _scaleConfig = new ScaleConfig();

        public bool _step1 = false;
        public bool step2 = false;
        

        public Calibration()
        {
            this.InitializeComponent();
            //tblCalibrationSteps.Text = "";
            //tblCalibrationSteps.Text = "Step 1 - Unload Scale, Enter Minimum Value then Click CALIBRATE ZERO.";
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void SetMinimumButton_Click(object sender, RoutedEventArgs e)
        {
            double num;

            if (SetMinimumTextBox.Text == "") { errorMessageShow("Please enter minimum value, then press Enter button"); return; }
            if (!double.TryParse(SetMinimumTextBox.Text, out num))
            {
                errorMessageShow("Please enter numeric value for minimum value"); return;
            }

            _UserMinimumValue = Convert.ToDecimal(SetMinimumTextBox.Text); //Capture User Minimum
            _RawMinimumValue = GpioUtility.ReadData();
            _LoadcellOffset = 0;//set offset to zero

            string message = "Minimum value set, move to step 3.";
            successMessageShow(message);
            SetMinimumButton.IsEnabled = false;
            SetMaximumButton.IsEnabled = true;
            SetMaximumTextBox.Focus(FocusState.Programmatic);

        }

        private void SetMaximumButton_Click(object sender, RoutedEventArgs e)
        {
            double num;
            //read Loadcell to get Raw Maximum Value
            if (SetMaximumTextBox.Text == "") { errorMessageShow("Please enter weight value, then press Calibrate button"); return; }
            if (!double.TryParse(SetMaximumTextBox.Text, out num))
            {
                errorMessageShow("Please enter numeric value for weight value"); return;
            }

            _UserMaximumValue = Convert.ToDecimal(SetMaximumTextBox.Text); //Capture User Maximum
            //_MaximumValue = Convert.ToDecimal(SetMaximumTextBox.Text); //Capture User Minimum
            _RawMaximumValue = GpioUtility.ReadData();

            string message = "Wieght value set. Press Finish button to complete calibration.";
            successMessageShow(message);
            SetMaximumButton.IsEnabled = false;
            FinishButton.Focus(FocusState.Programmatic);

        }

        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {

            _Gradient = (_UserMaximumValue - _UserMinimumValue) / (_RawMaximumValue - _RawMinimumValue);


            //y = mx +c  => c = y - mx
            _YIntercept = _UserMinimumValue - (_Gradient * _RawMinimumValue);

            ScaleConfigComponent scaleConfigComp = new ScaleConfigComponent();
            var scaleConfig = scaleConfigComp.GetScaleConfig();

            if (scaleConfig != null)
            {
               scaleConfig.Gradient = _Gradient;
               scaleConfig.YIntercept = _YIntercept;
               scaleConfig.offset = _LoadcellOffset;
               scaleConfigComp.UpdateScaleConfig(scaleConfig);
            }
            else
            {
                scaleConfig = new ScaleConfig();
                scaleConfig.Gradient = _Gradient;
                scaleConfig.YIntercept = _YIntercept;
                scaleConfig.offset = _LoadcellOffset;
                scaleConfigComp.AddScaleConfig(scaleConfig);
            }

            saveSuccessfullmessage();

        }

        private async void saveSuccessfullmessage()
        {
            var dialog = new MessageDialog("Calibration Successful.", "Information");

            dialog.Commands.Clear();
            dialog.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
            var res = await dialog.ShowAsync();

        }

        private async void errorMessageShow(string message)
        {
            var dialog = new MessageDialog(message, "Validation Error");

            dialog.Commands.Clear();
            dialog.Commands.Add(new UICommand { Label = "Ok", Id = 0 });

            var res = await dialog.ShowAsync();

        }

        private async void successMessageShow(string message)
        {
            var dialog = new MessageDialog(message, "Information");

            dialog.Commands.Clear();
            dialog.Commands.Add(new UICommand { Label = "Ok", Id = 0 });

            var res = await dialog.ShowAsync();

        }


    }
}
