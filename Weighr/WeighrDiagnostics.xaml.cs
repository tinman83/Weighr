using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Weighr.Helpers;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class WeighrDiagnostics : Page
    {
        public WeighrDiagnostics()
        {
            this.InitializeComponent();

            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                IPAddressTexBox.Text = GetLocalIPAddress();
            }

            PinCheckStates();

        }

        private void PinCheckStates()
        {
            if (GpioUtility.GetNormalFeedState() == true) { btnFastFeedValve.Background = new SolidColorBrush(Colors.Green); } else { btnFastFeedValve.Background = new SolidColorBrush(Colors.Red); }
            if (GpioUtility.GetDribbleFeedState() == true) { btnDribbleFeedValve.Background = new SolidColorBrush(Colors.Green); } else { btnDribbleFeedValve.Background = new SolidColorBrush(Colors.Red); }
            if (GpioUtility.GetAirSupplyState() == true) { btnAirSupplyValve.Background = new SolidColorBrush(Colors.Green); } else { btnAirSupplyValve.Background = new SolidColorBrush(Colors.Red); }

            if (GpioUtility.GetOverWeightState() == true) { btnOverWeightLight.Background = new SolidColorBrush(Colors.Green); } else { btnOverWeightLight.Background = new SolidColorBrush(Colors.Red); }
            if (GpioUtility.GetNormalWeightState() == true) { btnNormalWeightLight.Background = new SolidColorBrush(Colors.Green); } else { btnNormalWeightLight.Background = new SolidColorBrush(Colors.Red); }
            if (GpioUtility.GetUnderWeightState() == true) { btnUnderWeightLight.Background = new SolidColorBrush(Colors.Green); } else { btnUnderWeightLight.Background = new SolidColorBrush(Colors.Red); }
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        private void btnFastFeedValve_Click(object sender, RoutedEventArgs e)
        {
            if (GpioUtility.GetNormalFeedState() == true)
            {
                GpioUtility.closeNormalFeedValve();
            }
            else
            {
                GpioUtility.openNormalFeedValve();
            }

            Task.Delay(1000);

            if (GpioUtility.GetNormalFeedState() == true) { btnFastFeedValve.Background = new SolidColorBrush(Colors.Green); } else { btnFastFeedValve.Background = new SolidColorBrush(Colors.Red); }
        }

        private void btnDribbleFeedValve_Click(object sender, RoutedEventArgs e)
        {
            if (GpioUtility.GetDribbleFeedState() == true)
            {
                GpioUtility.closeDribbleFeedValve();
            }
            else
            {
                GpioUtility.openDribbleFeedValve();
            }

            Task.Delay(1000);
            if (GpioUtility.GetDribbleFeedState() == true) { btnDribbleFeedValve.Background = new SolidColorBrush(Colors.Green); } else { btnDribbleFeedValve.Background = new SolidColorBrush(Colors.Red); }
        }

        private void btnAirSupplyValve_Click(object sender, RoutedEventArgs e)
        {
            if (GpioUtility.GetAirSupplyState() == true)
            {
                GpioUtility.closeAirSupplyValve();
            }
            else
            {
                GpioUtility.openAirSupplyValve();
            }

            Task.Delay(1000);
            if (GpioUtility.GetAirSupplyState() == true) { btnAirSupplyValve.Background = new SolidColorBrush(Colors.Green); } else { btnAirSupplyValve.Background = new SolidColorBrush(Colors.Red); }

        }

        private void btnOverWeightLight_Click(object sender, RoutedEventArgs e)
        {
            if (GpioUtility.GetOverWeightState() == true)
            {
                GpioUtility.switchOffOverweightLight();
            }
            else
            {
                GpioUtility.switchOnOverWeightLight();
            }

            Task.Delay(1000);
            if (GpioUtility.GetOverWeightState() == true) { btnOverWeightLight.Background = new SolidColorBrush(Colors.Green); } else { btnOverWeightLight.Background = new SolidColorBrush(Colors.Red); }

        }

        private void btnNormalWeightLight_Click(object sender, RoutedEventArgs e)
        {
            if (GpioUtility.GetNormalWeightState() == true)
            {
                GpioUtility.switchOffNormalweightLight();
            }
            else
            {
                GpioUtility.switchOnNormalWeightLight();
            }

            Task.Delay(1000);
            if (GpioUtility.GetNormalWeightState() == true) { btnNormalWeightLight.Background = new SolidColorBrush(Colors.Green); } else { btnNormalWeightLight.Background = new SolidColorBrush(Colors.Red); }
        }

        private void btnUnderWeightLight_Click(object sender, RoutedEventArgs e)
        {
            if (GpioUtility.GetUnderWeightState() == true)
            {
                GpioUtility.switchOffUnderweightLight();
            }
            else
            {
                GpioUtility.switchOnUnderWeightLight();
            }

            Task.Delay(1000);
            if (GpioUtility.GetUnderWeightState() == true) { btnUnderWeightLight.Background = new SolidColorBrush(Colors.Green); } else { btnUnderWeightLight.Background = new SolidColorBrush(Colors.Red); }
        }
    }


}
