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
        public string DeviceKey { get; set; }
        public string SerialNumber { get; set; }
        public string  ClientId { get; set; }
        public string PlantId { get; set; }
        public string MachineName { get; set; }

    }
}
