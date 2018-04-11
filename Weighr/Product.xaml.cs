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
    public sealed partial class Product : Page
    {
        public Product()
        {
            this.InitializeComponent();
        }

        private void ProductComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DensityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TargetWeightComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UpperLimitComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void LowerLimitComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnUpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            //ProductDetails product = new ProductDetails();
            //product.UpdateConfiguredProduct();

/*
            configured_product.ConfigureProduct();
            
            ProductComboBox
            DensityComboBox
TargetWeightComboBox
UpperLimitComboBox
LowerLimitComboBox*/
        }
    }
}
