using Microsoft.EntityFrameworkCore;
using SmartHomeApi.Data.Models;
using SmartHomeApi.Data.Repos.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHomeApi.Data.Repos
{
    public class RoomRepository : IRoomRepository
    {
        private readonly SmartHomeApiContext _context;

        public RoomRepository(SmartHomeApiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавить объект Room
        /// </summary>
        public async Task AddRoom(Room room)
        {
            var entry = _context.Entry(room);

            if (entry.State == EntityState.Detached)
                await _context.Rooms.AddAsync(room);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удалить объект Room
        /// </summary>
        public async Task DeleteRoom(Room room)
        {
            var entry = _context.Entry(room);

            if (entry.State == EntityState.Detached)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Взять объект Room
        /// </summary>
        public async Task<Room> GetRoomByName(string name)
        {
            return await _context.Rooms.Where(r => r.Name == name).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Выгрузить все объекты Room
        /// </summary>
        public async Task<Room[]> GetRooms()
        {
            return await _context.Rooms.ToArrayAsync();
        }
    }
}
