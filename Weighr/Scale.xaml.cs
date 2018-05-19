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
        public decimal LoadcellOffset=0M, temp, _min_div = 0.010M;

        decimal _scale_gradient, _y_intercept,_calc_result,_prior_calc_result, _normal_feed_cutoff_percentage = 0.925M, _weight_display,_threshold=20,_priorWeight=0;
        decimal _minimum_division, _maximum_capacity, _resolution,_current_target_weight, _current_upper_limit, _current_lower_limit, _normal_cutoff_weight, _final_setpoint_weight;
        decimal _current_product_density;
        string _display_units, _current_product_code, _curent_product_name;
        string _current_status;
        int _decimal_position,_divider, _weight, _current_product_id, _filter_counter, _j;

        decimal[] _counter_values = new decimal[3];

        private Boolean _checkWeightProcess = false;
        private Boolean _runprocess = false, _normal_cutoff_reached = false, _final_setpoint_reached = false;
        private Boolean startProcessLocked = false;

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

            _currentProduct = productComp.GetLastAddedProduct();

            ProductsComboBox.SelectedValue = _currentProduct.ProductCode;

            tblWeigherStatus.Text = "Idle";

            _normal_cutoff_weight = (_currentProduct.TargetWeight)*Convert.ToDecimal(0.8) ;
            _final_setpoint_weight = _currentProduct.TargetWeight - Convert.ToDecimal(_currentProduct.Inflight);
            
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1); // Interval of the timer
            timer.Tick += timer_Tick;
            timer.Start();

        }

        private void ProductsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _currentProduct = (WeighrDAL.Models.Product)ProductsComboBox.SelectedItem;

            _normal_cutoff_weight = (_currentProduct.TargetWeight) * Convert.ToDecimal(0.8);
            _final_setpoint_weight = _currentProduct.TargetWeight - Convert.ToDecimal(_currentProduct.Inflight);

        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            //threshold = Convert.ToDouble(tbxTest.Text);
        }

        private void timer_Tick(object sender, object e)
        {
            timer.Stop();
            //read scale and update ui
            Int32 result = GpioUtility.ReadData();
            //_calc_result = _scaleSetting.Density * (((_scaleConfig.Gradient) * result) + _scaleConfig.YIntercept - _scaleConfig.offset);
            //_calc_result = _calc_result - (_calc_result % _scaleSetting.MinimumDivision);


            //tblWeightDisplay.Text = _calc_result.ToString("0.00"); // returns "0"  when decimalVar == 0
            //RadialGaugeControl.Unit = "%";
            //RadialGaugeControl.Value = Convert.ToInt64((_calc_result / _currentProduct.TargetWeight) * 100);

            //_prior_calc_result = _calc_result;
            decimal calc_result = _scaleSetting.Density * (((_scaleConfig.Gradient) * result) + _scaleConfig.YIntercept - LoadcellOffset);
            calc_result = calc_result - (calc_result % _min_div);

            _counter_values[_filter_counter] = calc_result;
            _filter_counter++;

            if (_filter_counter == 3)
            {
                _filter_counter = 0;
                sortValues();
                _calc_result = _counter_values[1] ;
                tblWeightDisplay.Text = _calc_result.ToString("0.000"); // returns "0"  when decimalVar == 0

                RadialGaugeControl.Unit = "%";
                RadialGaugeControl.Value = Convert.ToInt64((_calc_result / _currentProduct.TargetWeight) * 100);

            }




            // _runprocess=GpioUtility.isRunButtonPressed();

            if(GpioUtility.isEstopButtonPressed())
            {
                GpioUtility.closeNormalFeedValve();  //open normal feed valve 
                GpioUtility.closeDribbleFeedValve(); //open dribble feed valve
            }
            if (_runprocess == true || GpioUtility.isRunButtonPressed() == true)
            {

                    GpioUtility.openNormalFeedValve();  //open normal feed valve 
                    Thread.Sleep(1000);
                // GpioUtility.openDribbleFeedValve(); //open dribble feed valve
                _normal_cutoff_reached = false;
                _final_setpoint_reached = false;
                LoadcellOffset = _calc_result;
                startProcessLocked = true;
                _checkWeightProcess = true;
                _runprocess = false;
                tblWeigherStatus.Text = "button press detected";
                GpioUtility.openAirSupplyValve();
                    GpioUtility.switchOffOverweightLight();
                    GpioUtility.switchOffUnderweightLight();
                    GpioUtility.switchOffNormalweightLight();
                    

            }

            if (startProcessLocked)    //checked if starting is locked
            {
                tblWeigherStatus.Text = "process locked";
                if (_checkWeightProcess == true)  //if check weight in progress
                {
                    tblWeigherStatus.Text = "checking weight";
                    if (_normal_cutoff_reached == false)   //check if normal cuoff is reached
                    {
                        tblWeigherStatus.Text = "Filling to dribble point";
                        //display filling status
                        if (_calc_result >= _normal_cutoff_weight)  //normal feed cutoff reached?
                        {
                            GpioUtility.openDribbleFeedValve();  //close normal feed valve
                            GpioUtility.closeAirSupplyValve();
                            _normal_cutoff_reached = true;
                        }
                    }

                    else if (_final_setpoint_reached == false)
                    {
                        tblWeigherStatus.Text = "Filling to final setpoint";
                        //display filling status
                        if (_calc_result >= _final_setpoint_weight)
                        {
                            GpioUtility.closeDribbleFeedValve();  //close dribble feed valve
                            GpioUtility.closeNormalFeedValve();
                            _final_setpoint_reached = true;

                            Thread.Sleep(2000);
                            LogFinalValues();
                            ResetProcess();
                        }

                    }
                             

                }
            
            }

            timer.Start();

        }

        public void sortValues()
        {
            for(int i=0;i<=2;i++)
            {
                _j = i;

               while ((_j > 0) && (_counter_values[_j] < _counter_values[_j - 1]) )
               {
                    temp = _counter_values[_j];
                    _counter_values[_j] = _counter_values[_j - 1];
                    _counter_values[_j - 1] = temp;
                    _j = _j - 1;
                    

               }
            }


        }


        public void ResetProcess()
        {
            startProcessLocked = false;
            _normal_cutoff_reached = false;
            _final_setpoint_reached = false;
            _checkWeightProcess = false;
            _runprocess = false;
        }
       
        public void  LogFinalValues()
        {
            tblWeigherStatus.Text = "Status: Filling Complete";
            //display filling status
            _checkWeightProcess = false;

            if (_calc_result > ((_currentProduct.TargetWeight) + _currentProduct.UpperLimit))   //if overpacked
            {
                GpioUtility.switchOnOverWeightLight();
            }

            else if (_calc_result < ((_currentProduct.TargetWeight) - _currentProduct.LowerLimit))    //if underpacke
            {
                GpioUtility.switchOnUnderWeightLight();
            }

            else
            {
                GpioUtility.switchOnNormalWeightLight();
            }
            TransactionLogComponent TransactionLogComp = new TransactionLogComponent();
            TransactionLog trans_log = new TransactionLog() { ProductId = _currentProduct.ProductId, ProductCode = _currentProduct.ProductCode, ActualWeight = Convert.ToDecimal(_calc_result), TransactionDate = DateTime.Now, WeightDifference = Convert.ToDecimal(_calc_result) - _currentProduct.TargetWeight };
            TransactionLogComp.AddTransactionLog(trans_log);
            //
        }
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
            //ScaleConfigComponent ScaleConfigComp = new ScaleConfigComponent();

            //ScaleConfig scaleCon = new ScaleConfig() { offset = (_calc_result - _scaleConfig.offset) };
            //scaleCon.ScaleConfigId = 1;
            //ScaleConfigComp.UpdateScaleConfig(scaleCon);
            //GetScaleConfigurations();

            LoadcellOffset = _calc_result;


        }

        public void ZeroScale()
        {
            //ScaleConfigComponent ScaleConfigComp = new ScaleConfigComponent();

            //ScaleConfig scaleCon = new ScaleConfig() { offset = (_calc_result - _scaleConfig.offset) };
            //scaleCon.ScaleConfigId = 1;
            //ScaleConfigComp.UpdateScaleConfig(scaleCon);
            //GetScaleConfigurations();

            
                LoadcellOffset = (_calc_result);
           

            

        }

        private void btnTareScale_Click(object sender, RoutedEventArgs e)
        {
            //Calibration calibrate = new Calibration();
            //bool result = calibrate.TareScale();
        }

        private void btnNetGross_Click(object sender, RoutedEventArgs e)
        {
            //Calibration calibrate = new Calibration();
            //bool result = calibrate.NetGrossScale();
        }

        private void LoadProductByProductCode(string productCode)
        {
            ProductComponent productComp = new ProductComponent();

           _currentProduct = productComp.GetProduct(productCode);

        }


    }
}
