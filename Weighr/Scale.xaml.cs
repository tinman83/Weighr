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
using Weighr.Helpers;


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

        double _scale_gradient, _y_intercept,_calc_result, _normal_feed_cutoff_percentage = 0.8, _weight_display,_threshold=20;
        decimal _minimum_division, _maximum_capacity, _resolution,_current_target_weight, _current_upper_limit, _current_lower_limit;
        decimal _current_product_density;
        string _display_units, _current_product_code, _curent_product_name;
        string _current_status;
        int _decimal_position,_divider, _weight, _current_product_id;

        private Boolean _checkWeightProcess = false;
        private Boolean _runprocess = true, _normal_cutoff_reached = false, _final_setpoint_reached = false;

        private ScaleSetting _scaleSetting = new ScaleSetting();
        private ScaleConfig _scaleConfig = new ScaleConfig();

        List<WeighrDAL.Models.Product> _ProductsList = new List<WeighrDAL.Models.Product>();
        WeighrDAL.Models.Product _currentProduct = new WeighrDAL.Models.Product();

        private DispatcherTimer timer;

        public Scale()
        {
            this.InitializeComponent();
            
            //Thread oThread = new Thread(new ThreadStart(weightReaderHandler));
            //oThread.Start();


        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetScaleConfigurations();
            GetScaleSettings();
            ProductComponent productComp = new ProductComponent();
            _ProductsList = productComp.GetProducts();
            ProductsComboBox.ItemsSource = _ProductsList;

            

            bool IsInitialised = GpioUtility.InitialiseGpio();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1); // Interval of the timer
            timer.Tick += timer_Tick;
            //timer.Start();

        }

        private void ProductsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _currentProduct = (WeighrDAL.Models.Product)ProductsComboBox.SelectedItem;
            
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            //threshold = Convert.ToDouble(tbxTest.Text);
        }

        private void timer_Tick(object sender, object e)
        {
            //read scale and update ui
            Int32 result = GpioUtility.ReadData();
            _calc_result = _scaleSetting.Density * (((_scaleConfig.Gradient) * result) + _scaleConfig.YIntercept);

            tblWeightDisplay.Text = _calc_result.ToString("0.##"); // returns "0"  when decimalVar == 0

           // _runprocess=GpioUtility.isRunButtonPressed();
            if (_runprocess == true)
            {

                GpioUtility.openNormalFeedValve();  //open normal feed valve 
                GpioUtility.openDribbleFeedValve(); //open dribble feed valve

                _runprocess = false;
                _checkWeightProcess = true;

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

        //        GpioUtility.openNormalFeedValve();  //open normal feed valve 
        //        GpioUtility.openDribbleFeedValve(); //open dribble feed valve

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
        //                GpioUtility.closeNormalFeedValve();  //close normal feed valve
        //                normal_cutoff_reached = true;
        //              }
        //           }
        //           else if(final_setpoint_reached == false)
        //           {
        //            //display filling status
        //            if(weight_display >= current_target_weight)
        //            {
        //                GpioUtility.closeDribbleFeedValve();  //close dribble feed valve
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

            _scaleConfig = ScaleConfigComp.GetScaleConfig();
     
        }

        public void GetScaleSettings()
        {
            ScaleSettingComponent ScaleSettingComp = new ScaleSettingComponent();
            _scaleSetting = ScaleSettingComp.GetScaleSetting();

        }

        private async void UpdateWeightUI(string weightResult)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                tblWeightDisplay.Text = weightResult;
            });
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            btnEnter.IsEnabled = false;
           
            /** scale zeroing **/
            //LoadcellOffset = weight_display + LoadcellOffset;
            ScaleConfigComponent ScaleConfigComp = new ScaleConfigComponent();
            var new_settings = ScaleConfigComp.ZeroScale(LoadcellOffset);
            //  LoadcellOffset = new_settings.Offset;

            _normal_cutoff_reached = false;
            _final_setpoint_reached = false;
            _runprocess = true;
          


        }

        public void GetCurrentProductDetails()
        {
            ProductComponent ProductComp = new ProductComponent();
        
            var product = ProductComp.GetCurrentProduct();

            _current_product_id = product.ProductId;
            _current_product_code = product.ProductCode;
            _current_product_density = product.Density;
            _curent_product_name = product.Name;
            _current_target_weight = product.TargetWeight;
            _current_upper_limit = product.UpperLimit;
            _current_lower_limit = product.LowerLimit;



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

        private void LoadProductByProductCode(string productCode)
        {
            ProductComponent productComp = new ProductComponent();

           _currentProduct = productComp.GetProduct(productCode);

        }


    }
}
