using Microsoft.EntityFrameworkCore;
using SmartHomeApi.Data.Models;
using SmartHomeApi.Data.Queries;
using SmartHomeApi.Data.Repos.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHomeApi.Data.Repos
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly SmartHomeApiContext _context;

        public DeviceRepository(SmartHomeApiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Удалить объект Device
        /// </summary>
        public async Task DeleteDevice(Device device)
        {
            var entry = _context.Entry(device);

            if (entry.State == EntityState.Detached)
            {
                _context.Devices.Remove(device);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Найти Device по полю Id
        /// </summary>
        public async Task<Device> GetDeviceById(Guid id)
        {
            return await _context.Devices
                .Include(d => d.Room)
                .Where(d => d.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Найти Device по полю Name
        /// </summary>
        public async Task<Device> GetDeviceByName(string name)
        {
            return await _context.Devices
                .Include(d => d.Room)
                .Where(d => d.Name == name).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Выгрузить все объекты Device
        /// </summary>
        public async Task<Device[]> GetDevices()
        {
            return await _context.Devices
                .Include(d => d.Room)
                .ToArrayAsync();
        }

        /// <summary>
        /// Добавить новый объект Device
        /// </summary>
        public async Task SaveDevice(Device device, Room room)
        {
            device.RoomId = room.Id;
            device.Room = room;

            var entry = _context.Entry(device);

            if (entry.State == EntityState.Detached)
                await _context.Devices.AddAsync(device);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateDevice(Device device, Room room, UpdateDeviceQuery query)
        {
            device.RoomId = room.Id;
            device.Room = room;

            if (!string.IsNullOrEmpty(query.NewName))
                device.Name = query.NewName;

            if (!string.IsNullOrEmpty(query.NewSerial))
                device.SerialNumber = query.NewSerial;

            var entry = _context.Entry(device);

            if (entry.State == EntityState.Detached)
                _context.Update(device);

            await _context.SaveChangesAsync();
        }
    }
}
