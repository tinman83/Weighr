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
            ProductComponent productComp = new ProductComponent();

            _ProductsList = productComp.GetProducts();
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
            _product.Inflight = double.Parse(InflightTextBox.Text);

            ProductComponent productComp = new ProductComponent();

            if (_product.ProductId == 0)
            {
                productComp.AddProduct(_product);
            }
            else
            {
                productComp.UpdateProduct(_product);
            }

            ClearProduct();

        }

        private bool ValidateSaveProduct()
        {


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
            InflightTextBox.Text = _product.Inflight.ToString();
        }

        private void ProductsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string productCode = ProductsComboBox.SelectedValue.ToString();
            LoadProductByProductCode(productCode);
        }

        private void ClearProduct()
        {

            ProductCodeTextBox.Text = "";
            ProductNameTextBox.Text = "";
            DensityTextBox.Text = "";
            TargetWeightTextBox.Text = "";
            UpperLimitTextBox.Text = "";
            LowerLimitTextBox.Text = "";
            InflightTextBox.Text = "";
            _product = null; //new WeighrDAL.Models.Product();
        }

        private void btnNewProduct_Click(object sender, RoutedEventArgs e)
        {
            ClearProduct();
        }
    }
}
