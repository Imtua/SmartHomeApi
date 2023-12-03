namespace SmartHomeApi.Contracts.Models.Devices
{
    public class GetDevicesResponse
    {
        public int DeviceAmount { get; set; }
        public DeviceView[] Devices { get; set; } 
    }
}
