using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WeighrDAL.Components;
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
    public sealed partial class Reports : Page
    {
        List<WeighrDAL.Models.Product> _ProductsList = new List<WeighrDAL.Models.Product>();
        WeighrDAL.Models.Product _product = new WeighrDAL.Models.Product();
        public Reports()
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

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            _product = (WeighrDAL.Models.Product)ProductsComboBox.SelectedItem;
            if (_product == null) { return; }

            string productCode = _product.ProductCode;
            var from = dtpDateFrom.Date.Value.Date + dtpTimeTo.Time;
            var to = dtpDateTo.Date.Value.Date + dtpTimeTo.Time;

            DateTime dateTimeFrom = from.ToUniversalTime();
            DateTime dateTimeTo = to.ToUniversalTime();

            ReportDataComponent reportData = new ReportDataComponent();
            var summary = reportData.GetProductionSummaryReport(productCode, dateTimeFrom, dateTimeTo);

            if (summary != null)
            {
                UnitsTextBox.Text = summary.Units.ToString();
                RequiredWeightTextBox.Text = summary.RequiredWeight.ToString();
                ActualWeightTextBox.Text = summary.ActualWeight.ToString();
                AverageWeightTextBox.Text = summary.AverageWeight.ToString();
                PercDiffTextBox.Text = summary.PercDiffWeight.ToString();
                AverageFillTimeTextBox.Text = summary.AverageFillTime.ToString();
            }
            else
            {
                UnitsTextBox.Text = "0";
                RequiredWeightTextBox.Text = "0.0";
                ActualWeightTextBox.Text = "0.00";
                AverageWeightTextBox.Text = "0.00";
                PercDiffTextBox.Text = "0";
                AverageFillTimeTextBox.Text = "0.00";
            }

            
        }
    }
}
