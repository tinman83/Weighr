using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WeighrDAL;
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
using Windows.Devices.Gpio;
using WeighrDAL.Models;
using Weighr.Helpers;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Weighr
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public decimal product_weight;
        public decimal current_weight;
        public decimal LoadcellOffset;

        /// <summary>
        /// Used to signal that the device is properly initialized and ready to use
        /// </summary>
        private bool available = false;

        
        
        public MainPage()
        {
            this.InitializeComponent();
            BackButton.Visibility = Visibility.Collapsed;
            
            MyFrame.Navigate(typeof(Scale));
                TitleTextBlock.Text = "Scale";
                Scale.IsSelected = true;
               

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            bool IsInitialised = GpioUtility.InitialiseGpio();
            DBHelper dbhelper = new DBHelper();
            dbhelper.InitialiseTables();
        }

        private void HamgurgerButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(Scale));
            TitleTextBlock.Text = "Scale";
            Scale.IsSelected = true;
        }



        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Scale.IsSelected)
            {
                BackButton.Visibility = Visibility.Collapsed;
                MyFrame.Navigate(typeof(Scale));
                TitleTextBlock.Text = "Scale";
            }
            else if (Product.IsSelected)
            {
                BackButton.Visibility = Visibility.Visible;
                MyFrame.Navigate(typeof(Product));
                TitleTextBlock.Text = "Product";
            }
            else if (Settings.IsSelected)
            {
                BackButton.Visibility = Visibility.Visible;
                MyFrame.Navigate(typeof(Settings));
                TitleTextBlock.Text = "Settings";
            }
            else if (Shift.IsSelected)
            {
                BackButton.Visibility = Visibility.Visible;
                MyFrame.Navigate(typeof(Shift));
                TitleTextBlock.Text = "Shift";
            }
            else if (Reports.IsSelected)
            {
                BackButton.Visibility = Visibility.Visible;
                MyFrame.Navigate(typeof(Reports));
                TitleTextBlock.Text = "Reports";
            }
            else if (Calibration.IsSelected)
            {
                BackButton.Visibility = Visibility.Visible;
                MyFrame.Navigate(typeof(Calibration));
                TitleTextBlock.Text = "Calibration";
            }
        }

       

    }
}
