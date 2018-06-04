using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.System.Profile;

namespace Weighr.Helpers
{
    public sealed class DeviceInfoHelper
    {
        private static DeviceInfoHelper _Instance;
        public static DeviceInfoHelper Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new DeviceInfoHelper();
                return _Instance;
            }

        }

        public string Id { get; private set; }
        public string Model { get; private set; }
        public string Manufacturer { get; private set; }
        public string Name { get; private set; }
        public static string OSName { get; set; }

        public string ClientId { get; set; }
        public string SerialNumber { get; set; }
        public bool pushToCloud { get; set; }
        public string iotHubUri { get; set; }
        public string iotHubDeviceKey { get; set; }
        public string iotHubDeviceName { get; set; }
        public bool pushToWebApi { get; set; }
        public string ServerUrl { get; set; }

        public string PlantId { get; set; }
        public string MachineName { get; set; }

        private DeviceInfoHelper()
        {
            Id = GetId();
            var deviceInformation = new EasClientDeviceInformation();
            Model = deviceInformation.SystemProductName;
            Manufacturer = deviceInformation.SystemManufacturer;
            Name = deviceInformation.FriendlyName;
            OSName = deviceInformation.OperatingSystem;

            PlantId = "Plant1";
            MachineName= "line1";
            ClientId = "test@techcentre.cloresol.com";
            SerialNumber = "123456789";
            pushToCloud = true;
            iotHubUri = "CloresolIotHub.azure-devices.net";
            iotHubDeviceKey = "ULEN7xEO+1ahF8epUeMoRTpnqgVDVgzu0ZOhvQeW75w=";
            iotHubDeviceName = SerialNumber;
            pushToWebApi = false;
            ServerUrl = "";
        }

        private static string GetId()
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.System.Profile.HardwareIdentification"))
            {
                var token = HardwareIdentification.GetPackageSpecificToken(null);
                var hardwareId = token.Id;
                var dataReader = Windows.Storage.Streams.DataReader.FromBuffer(hardwareId);

                byte[] bytes = new byte[hardwareId.Length];
                dataReader.ReadBytes(bytes);

                return BitConverter.ToString(bytes).Replace("-", "");
            }

            throw new Exception("NO API FOR DEVICE ID PRESENT!");
        }
    }
}
