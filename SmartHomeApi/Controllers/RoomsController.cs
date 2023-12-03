using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartHomeApi.Contracts.Models.Rooms;
using SmartHomeApi.Data.Models;
using SmartHomeApi.Data.Repos.Interfaces;
using System.Threading.Tasks;

namespace SmartHomeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomsController : Controller
    {
        private readonly IRoomRepository _rooms;
        private IMapper _mapper;

        public RoomsController(IRoomRepository rooms, IMapper mapper)
        {
            _rooms = rooms;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить все комнаты
        /// </summary>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var rooms = await _rooms.GetRooms();

            var resp = new GetRoomsResponse()
            {
                RoomsAmount = rooms.Length,
                Rooms = _mapper.Map<Room[], RoomView[]>(rooms)
            };

            return StatusCode(200, resp);
        }

        /// <summary>
        /// Добавление комнаты
        /// </summary>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Add(
            [FromBody]AddRoomRequest request)
        {
            var isExistedRoom = await _rooms.GetRoomByName(request.Name);

            if (isExistedRoom == null)
            {
                var room = _mapper.Map<AddRoomRequest, Room>(request);

                await _rooms.AddRoom(room);
                return StatusCode(200, $"Комната {room.Name} добавлена!");
            }
            return StatusCode(400, $"Ошибка: Комната с названием {request.Name} уже существует!");
        }

        /// <summary>
        /// Удаление комнаты
        /// </summary>
        [HttpDelete]
        [Route("")]
        public async Task<IActionResult> Delete(
            [FromBody]DeleteRoomRequest request)
        {
            var room = await _rooms.GetRoomByName(request.Name);

            #region [Проверка входных параметров]

            if (room is null)
                return StatusCode(400, $"Ошибка: Комната с названием {request.Name} не найдена!");

            if (room.Area != request.Area)
                return StatusCode(400, $"Ошибка: Неверно указана площадь комнаты!");

            if (room.GasConnected != request.GasConnected)
                return StatusCode(400, $"Ошибка: Неверно указана газовая подключенность!");

            if (room.Voltage != request.Voltage)
                return StatusCode(400, $"Ошибка: Вольтаж комнаты не совпадает!");

            #endregion

            await _rooms.DeleteRoom(room);
            return StatusCode(200, $"Комната {room.Name} удалена.");
        }
    }
}
