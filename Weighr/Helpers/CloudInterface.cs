using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WeighrDAL.Models;
using Microsoft.Azure.Devices.Client;

namespace Weighr.Helpers
{
   public static class CloudInterface
    {
        static DeviceClient deviceClient;
        //static string iotHubUri = "CugnusIotHub.azure-devices.net";

        public static async Task<bool> PushToCloud(string iotHubUri, string iotHubDeviceKey, TransactionLog transactionLog)
        {

            deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey(iotHubUri, iotHubDeviceKey), TransportType.Mqtt);
            var res= await SendDeviceToCloudMessagesAsync(transactionLog);

            return res;
        }

        private static async Task<bool> SendDeviceToCloudMessagesAsync(TransactionLog transactionLog)
        {

            try
            {
                var messageString = JsonConvert.SerializeObject(transactionLog);
                var message = new Message(Encoding.ASCII.GetBytes(messageString));

                await deviceClient.SendEventAsync(message);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
           

        }

        public static async Task<bool> PushToWebApi(string serverUrl,TransactionLog transactionLog)
        {
            try
            {

                var json = JsonConvert.SerializeObject(transactionLog);
                var url = new Uri(serverUrl);
                var client = new WebClient();

                client.Headers.Add("Content-Type", "application/json");
                await client.UploadStringTaskAsync(url,json);

                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

    }
}
