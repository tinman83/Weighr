using System;
using System.Collections.Generic;
using System.Text;

namespace WeighrDAL.Models
{
    public class DeviceInfo
    {

        public int Id { get; set; }
        public string Model { get;  set; }
        public string Manufacturer { get;  set; }
        public string Name { get;  set; }
        public string OSName { get; set; }
        public string SerialNumber { get; set; }
        public string  ClientId { get; set; }
        public string PlantId { get; set; }
        public string MachineName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool pushToCloud { get; set; }
        public string iotHubUri { get; set; }
        public string iotHubDeviceKey { get; set; }
        public bool pushToWebApi { get; set; }
        public string ServerUrl { get; set; }

    }
}
