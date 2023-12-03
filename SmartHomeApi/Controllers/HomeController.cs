using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SmartHomeApi.Configuration;
using SmartHomeApi.Contracts.Models.Home;

namespace SmartHomeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private IOptions<HomeOptions> _options;
        private IMapper _mapper;

        public HomeController(IOptions<HomeOptions> options, IMapper mapper)
        {
            _options = options;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить информацию о доме
        /// </summary>
        [HttpGet]
        [Route("info")]
        public IActionResult Info()
        {
            #region Старый ответ
            //var pageResult = new StringBuilder();

            //pageResult.Append($"Добро пожаловать в API вашего дома! {Environment.NewLine}");
            //pageResult.Append($"Здесь вы можете посмотреть основную информацию.{Environment.NewLine}");
            //pageResult.Append($"{Environment.NewLine}");
            //pageResult.Append($"Количество этажей: {_options.Value.FloorAmount}{Environment.NewLine}");
            //pageResult.Append($"Стационарный телефон: {_options.Value.Telephone}{Environment.NewLine}");
            //pageResult.Append($"Тип отопления: {_options.Value.Heating}{Environment.NewLine}");
            //pageResult.Append($"Напряжение электросети: {_options.Value.CurrentVolts}{Environment.NewLine}");
            //pageResult.Append($"Подключен к газовой сети: {_options.Value.GasConnected}{Environment.NewLine}");
            //pageResult.Append($"Жилая площадь: {_options.Value.Area} м2{Environment.NewLine}");
            //pageResult.Append($"Материал: {_options.Value.Material}{Environment.NewLine}");
            //pageResult.Append($"{Environment.NewLine}");
            //pageResult.Append($"Адрес: {_options.Value.Address.Street} {_options.Value.Address.House}/{_options.Value.Address.Building}{Environment.NewLine}");

            //return StatusCode(200, pageResult.ToString());

            #endregion

            var infoResponse = _mapper.Map<HomeOptions, InfoResponse>(_options.Value);
            return StatusCode(200, infoResponse);
        }
    }
}
