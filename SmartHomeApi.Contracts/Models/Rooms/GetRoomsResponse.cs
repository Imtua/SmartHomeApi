namespace SmartHomeApi.Contracts.Models.Rooms
{
    public class GetRoomsResponse
    {
        public int RoomsAmount { get; set; }
        public RoomView[] Rooms { get; set; } 
    }
}
