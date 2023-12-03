namespace SmartHomeApi.Contracts.Models.Rooms
{
    public class RoomView
    {
        public string Name { get; set; }
        public int Area { get; set; }
        public bool GasConnected { get; set; }
        public int Voltage { get; set; }
    }
}