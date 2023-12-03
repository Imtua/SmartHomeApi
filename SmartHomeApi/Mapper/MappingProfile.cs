using AutoMapper;
using SmartHomeApi.Configuration;
using SmartHomeApi.Contracts;
using SmartHomeApi.Contracts.Models.Devices;
using SmartHomeApi.Contracts.Models.Home;
using SmartHomeApi.Contracts.Models.Rooms;
using SmartHomeApi.Contracts.Validation;
using SmartHomeApi.Data.Models;

namespace SmartHomeApi.Mapper
{
    /// <summary>
    /// Настройка маппинга для всех сущностей
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// В конструкторе настроим соответствие сущностей при маппинге
        /// </summary>
        public MappingProfile()
        {
            // Конфигурация прользователя
            CreateMap<Address, AddressInfo>();
            CreateMap<HomeOptions, InfoResponse>()
                .ForMember(m => m.AddressInfo,
                opt => opt.MapFrom(src => src.Address));

            // Валидация запросов
            CreateMap<AddDeviceRequest, Device>()
                .ForMember(d => d.Location,
                    opt => opt.MapFrom(r => r.Location));

            CreateMap<AddRoomRequest, Room>();
            CreateMap<Device, DeviceView>();
            CreateMap<Room,RoomView>();
        }
    }
}
