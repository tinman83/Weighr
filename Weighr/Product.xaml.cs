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
using WeighrDAL.Components;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Weighr
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Product : Page
    {
        List<WeighrDAL.Models.Product> _ProductsList = new List<WeighrDAL.Models.Product>();
        WeighrDAL.Models.Product _product = new WeighrDAL.Models.Product();
        public Product()
        {
            this.InitializeComponent();
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadProductsList();
        }

        private void LoadProductsList()
        {
            ProductComponent productComp = new ProductComponent();

            _ProductsList = productComp.GetProducts();
            ProductsComboBox.ItemsSource = _ProductsList;
        }
        private void btnSaveProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateSaveProduct() == false) { return; }

            if (_product == null)
            {
                _product = new WeighrDAL.Models.Product();
                _product.ProductId = 0;
            }

            _product.ProductCode = ProductCodeTextBox.Text;
            _product.Name = ProductNameTextBox.Text;
            _product.Density =Decimal.Parse( DensityTextBox.Text);
            _product.TargetWeight = Decimal.Parse(TargetWeightTextBox.Text);
            _product.UpperLimit = Decimal.Parse(UpperLimitTextBox.Text);
            _product.LowerLimit = Decimal.Parse(LowerLimitTextBox.Text);
            _product.Inflight = 0;// Decimal.Parse(InflightTextBox.Text);
            _product.DribblePoint = Decimal.Parse(DribblePointTextBox.Text);
            _product.ExpectedFillTime = decimal.Parse(ExpectedFillTimeTextBox.Text);

            ProductComponent productComp = new ProductComponent();

            var curProduct = productComp.GetCurrentProduct();
            if (curProduct == null)
            {
                _product.isCurrent = true;
            }


            if (_product.ProductId == 0)
            {
                productComp.AddProduct(_product);
            }
            else
            {
                productComp.UpdateProduct(_product);
            }

            ClearProduct();
            LoadProductsList();
            saveSuccessfullmessage();

        }

        private bool ValidateSaveProduct()
        {
            if (ProductCodeTextBox.Text == "") { errorMessageShow("Please enter product code"); return false; }
            if (ProductNameTextBox.Text == "") { errorMessageShow("Please enter product name"); return false; }

            double num;
            if (DensityTextBox.Text == "") { errorMessageShow("Please enter product density"); return false; }
            if (!double.TryParse(DensityTextBox.Text, out num))
            {
                errorMessageShow("Please enter numeric value for product density"); return false;
            }

            if (UpperLimitTextBox.Text == "") { errorMessageShow("Please enter product upper limit."); return false; }
            if (!double.TryParse(UpperLimitTextBox.Text, out num))
            {
                errorMessageShow("Please enter numeric value for product upper limit."); return false;
            }

            if (LowerLimitTextBox.Text == "") { errorMessageShow("Please enter product lower limit."); return false; }
            if (!double.TryParse(UpperLimitTextBox.Text, out num))
            {
                errorMessageShow("Please enter numeric value for product lower limit."); return false;
            }

            if (DribblePointTextBox.Text == "") { errorMessageShow("Please enter product dribble point."); return false; }
            if (!double.TryParse(DribblePointTextBox.Text, out num))
            {
                errorMessageShow("Please enter numeric value for product dribble point."); return false;
            }

            if (ExpectedFillTimeTextBox.Text == "") { errorMessageShow("Please enter product Expected fill time."); return false; }
            if (!double.TryParse(ExpectedFillTimeTextBox.Text, out num))
            {
                errorMessageShow("Please enter numeric value for product expected fill time."); return false;
            }

            return true;
        }

        private void LoadProductByProductCode(string productCode)
        {
            ProductComponent productComp = new ProductComponent();

            _product = productComp.GetProduct(productCode);

            ProductCodeTextBox.Text = _product.ProductCode;
            ProductNameTextBox.Text = _product.Name;
            DensityTextBox.Text = _product.Density.ToString();
            TargetWeightTextBox.Text = _product.TargetWeight.ToString();
            UpperLimitTextBox.Text = _product.UpperLimit.ToString();
            LowerLimitTextBox.Text = _product.LowerLimit.ToString();
            DribblePointTextBox.Text = _product.DribblePoint.ToString();
            ExpectedFillTimeTextBox.Text = _product.ExpectedFillTime.ToString();

        }

        private void ClearProduct()
        {

            ProductCodeTextBox.Text = "";
            ProductNameTextBox.Text = "";
            DensityTextBox.Text = "";
            TargetWeightTextBox.Text = "";
            UpperLimitTextBox.Text = "";
            LowerLimitTextBox.Text = "";
            DribblePointTextBox.Text = "";
            ExpectedFillTimeTextBox.Text = "";

            _product = null; //new WeighrDAL.Models.Product();
        }

        private void btnNewProduct_Click(object sender, RoutedEventArgs e)
        {
            ClearProduct();
            LoadProductsList();
        }

        private void ProductsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _product = (WeighrDAL.Models.Product)ProductsComboBox.SelectedItem;
            if (_product != null)
            {
                ProductCodeTextBox.Text = _product.ProductCode;
                ProductNameTextBox.Text = _product.Name;
                DensityTextBox.Text = _product.Density.ToString();
                TargetWeightTextBox.Text = _product.TargetWeight.ToString();
                UpperLimitTextBox.Text = _product.UpperLimit.ToString();
                LowerLimitTextBox.Text = _product.LowerLimit.ToString();
                DribblePointTextBox.Text = _product.DribblePoint.ToString();
                ExpectedFillTimeTextBox.Text = _product.ExpectedFillTime.ToString();
            }
             

        }

        private async void saveSuccessfullmessage()
        {
            var dialog = new MessageDialog("Product successfully saved.", "Information");

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
    }
}
