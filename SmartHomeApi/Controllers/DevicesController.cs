using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SmartHomeApi.Configuration;
using SmartHomeApi.Contracts.Models.Devices;
using SmartHomeApi.Contracts.Validation;
using SmartHomeApi.Data.Models;
using SmartHomeApi.Data.Queries;
using SmartHomeApi.Data.Repos.Interfaces;
using System;
using System.Threading.Tasks;

namespace SmartHomeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DevicesController : Controller
    {
        private readonly IDeviceRepository _devices;
        private readonly IRoomRepository _rooms;
        private IMapper _mapper;

        public DevicesController(
            IDeviceRepository devices,
            IRoomRepository rooms,
            IMapper mapper)
        {
            _devices = devices;
            _rooms = rooms;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить все устройства
        /// </summary>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetDevices()
        {
            var devices = await _devices.GetDevices();

            var resp = new GetDevicesResponse()
            {
                DeviceAmount = devices.Length,
                Devices = _mapper.Map<Device[], DeviceView[]>(devices)
            };

            return StatusCode(200, resp);
        }

        /// <summary>
        /// Добавление нового устройства
        /// </summary>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Add(AddDeviceRequest request)
        {
            var room = await _rooms.GetRoomByName(request.Location);

            if (room is null)
                return StatusCode(400, $"Ошибка: Комната {request.Location} не подключена. Сначала подключите комнату!");

            var device = await _devices.GetDeviceByName(request.Name);

            if (device is not null)
                return StatusCode(400, $"Ошибка: Устройство {request.Name} уже существует!");

            var newDevice = _mapper.Map<AddDeviceRequest, Device>(request);
            await _devices.SaveDevice(newDevice, room);

            return StatusCode(200, $"Устройство {request.Name} добавлено. ID Устройства {newDevice.Id}.");
        }

        /// <summary>
        /// Обновление существующего устройства
        /// </summary>
        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> Edit(
            [FromRoute] Guid id,
            [FromBody] EditDeviceRequest request)
        {
            var room = await _rooms.GetRoomByName(request.NewRoom);

            if (room is null)
                return StatusCode(400, $"Ошибка: Комната {request.NewRoom} не подключена. Сначала подключите комнату!");

            var device = await _devices.GetDeviceById(id);

            if (device is null)
                return StatusCode(400, $"Ошибка: Устройство {id} не существует!");

            var withSameName = await _devices.GetDeviceByName(request.NewName);

            if (withSameName is not null)
                return StatusCode(400, $"Ошибка: Устройство с названием {request.NewName} уже подключено!");

            await _devices.UpdateDevice(
                device,
                room,
                new UpdateDeviceQuery(request.NewName, request.NewSerial)
                );

            return StatusCode(200, $"Устройство обновлено!  Имя — {device.Name}, Серийный номер — {device.SerialNumber}," +
                $"  Комната подключения  —  {device.Room.Name}");
        }

        /// <summary>
        /// Удаление устройства
        /// </summary>
        [HttpDelete]
        [Route("")]
        public async Task<IActionResult> Delete(
            [FromBody] DeleteDeviceRequest request)
        {
            var device = await _devices.GetDeviceByName(request.Name);

            #region [Проверка входных параметров]

            if (device is null)
                return StatusCode(400, $"Ошибка: Устройство с названием {request.Name} не найдено!");

            if (device.SerialNumber != request.SerialNumber)
                return StatusCode(400, $"Ошибка: Неверно указан серийный номер!");

            if (device.Manufacturer != request.Manufacturer)
                return StatusCode(400, "Ошибка: Неверно указан производитель!");

            if (device.Model != request.Model)
                return StatusCode(400, $"Ошибка: Устройство модели {request.Model} не найдено!");

            #endregion

            await _devices.DeleteDevice(device);
            return StatusCode(200, $"Устройство \"{device.Name} {request.Manufacturer} {request.Model}\" удалено!");
        }
    }
}
