using Cars.Service.Interfaces;
using Infrastructure.Attributes;
using Infrastructure.DTO;
using Infrastructure.HelperModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cars.Api.Controllers
{
    public class CharacteristicsController : BaseApiController
    {
        private readonly ICharacteristicService _characteristicService;

        public CharacteristicsController(ICharacteristicService characteristicService)
        {
            _characteristicService = characteristicService;
        }
        [HttpGet("{carId}")]
        public async Task<IActionResult> GetCharacterictic(Guid carId)
        {
            var result = await _characteristicService.GetCharacteristicByCarId(carId);
            return Ok();
        }
        [HttpPost("{carId}")]
        public async Task<IActionResult> AddCharacteristic(Guid carId, [FromQuery] CharacteristicDTO characteristicDTO)
        {
            await _characteristicService.AddCharacteristicByCarId(carId, characteristicDTO);
            return Ok();
        }
        [HttpPut("{carId}")]
        public async Task<IActionResult> UpdateCharacteristic(Guid carId, [FromQuery] CharacteristicDTO characteristicDTO)
        {
            await _characteristicService.UpdateCharacteristicByCarId(carId, characteristicDTO);
            return Ok();
        }
        [HttpDelete("{carId}")]
        public async Task<IActionResult> RemoveCharacteristic(Guid carId)
        {
            await _characteristicService.RemoveCharacteristicByCarId(carId);
            return Ok();
        }

    }
}
