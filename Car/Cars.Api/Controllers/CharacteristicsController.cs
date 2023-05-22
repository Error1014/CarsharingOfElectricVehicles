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
        [HttpGet("/{id}")]
        public async Task<IActionResult> GetCharacteristic(Guid id)
        {
            var resul = await _characteristicService.GetCharacteristic(id);
            return Ok(resul);
        }
        [HttpGet]
        public async Task<IActionResult> GetCharacteristics()
        {
            var resul = await _characteristicService.GetCharacteristics();
            return Ok(resul);
        }
        [RoleAuthorize("Admin")]
        [HttpPost]
        public async Task<IActionResult> AddCharacteristics([FromQuery] CharacteristicDTO characteristicDTO)
        {
            await _characteristicService.AddCharacteristic(characteristicDTO);
            return Ok();
        }
        [RoleAuthorize("Admin")]
        [HttpPut("/{id}")]
        public async Task<IActionResult> UpdateCharacteristics(Guid id, [FromQuery] CharacteristicDTO characteristicDTO)
        {
            await _characteristicService.UpdateCharacteristic(id, characteristicDTO);
            return Ok();
        }
        [RoleAuthorize("Admin")]
        [HttpDelete("/{id}")]
        public async Task<IActionResult> RemoveCharacteristics(Guid id)
        {
            await _characteristicService.RemoveCharacteristic(id);
            return Ok();
        }
    }
}
