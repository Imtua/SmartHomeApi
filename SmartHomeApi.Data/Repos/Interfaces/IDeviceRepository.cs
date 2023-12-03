using SmartHomeApi.Data.Models;
using SmartHomeApi.Data.Queries;
using System;
using System.Threading.Tasks;

namespace SmartHomeApi.Data.Repos.Interfaces
{
    public interface IDeviceRepository
    {
        Task<Device[]> GetDevices();
        Task<Device> GetDeviceByName(string name);
        Task<Device> GetDeviceById(Guid id);
        Task SaveDevice(Device device, Room room);
        Task UpdateDevice(Device device, Room room, UpdateDeviceQuery query);
        Task DeleteDevice(Device device);
    }
}
