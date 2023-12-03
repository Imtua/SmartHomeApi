using System;

namespace SmartHomeApi.Contracts.Validation
{
    public class DeleteDeviceRequest
    {
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
    }
}
