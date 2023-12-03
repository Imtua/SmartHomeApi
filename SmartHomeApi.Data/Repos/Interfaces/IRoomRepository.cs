using SmartHomeApi.Data.Models;
using System.Threading.Tasks;

namespace SmartHomeApi.Data.Repos.Interfaces
{
    public interface IRoomRepository
    {
        Task<Room> GetRoomByName(string name);
        Task AddRoom(Room room);
        Task DeleteRoom(Room room);
        Task<Room[]> GetRooms();
    }
}
