using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client.Exceptions;

namespace Weighr.Helpers
{
    public class MyDevice
    {
        public static string device_key;
        private static Device device;
        private static RegistryManager regManager;
        private const string IOTHUBSTRING = "HostName=CloresolIotHub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=iWdoT4OSiSlV6WfSkc1FOreA/ZAueGTpnJMbyXEn0nY=";

        public async static Task AddDeviceAsync(string deviceId)
        {

            regManager = RegistryManager.CreateFromConnectionString(IOTHUBSTRING);

            try
            {
                device = await regManager.AddDeviceAsync(new Device(deviceId));
                device_key = device.Authentication.SymmetricKey.PrimaryKey;

            }
            catch (DeviceAlreadyExistsException)
            {
                device = await regManager.GetDeviceAsync(deviceId);
                device_key = null;
            }
        }

        public async static Task DeleteDeviceAsync(string deviceId)
        {

            regManager = RegistryManager.CreateFromConnectionString(IOTHUBSTRING);

            device = await regManager.GetDeviceAsync(deviceId);

            if (device != null)
                await regManager.RemoveDeviceAsync(deviceId);
        }

        public async static Task<IEnumerable<Device>> GetDevicesAsync(int deviceCount = 8)
        {

            regManager = RegistryManager.CreateFromConnectionString(IOTHUBSTRING);

            return await regManager.GetDevicesAsync(deviceCount);
        }
    }
}