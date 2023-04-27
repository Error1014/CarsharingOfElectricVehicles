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

        [HttpGet]
        public async Task<IEnumerable<CharacteristicDTO>> GetCharacteristics()
        {
            return await _characteristicService.GetCharacteristics();
        }
        [RoleAuthorize("Admin Operator")]
        [HttpPost]
        public async Task<IActionResult> AddCharacteristics([FromQuery] CharacteristicDTO characteristicDTO)
        {
            await _characteristicService.AddCharacteristic(characteristicDTO);
            return Ok();
        }
        [RoleAuthorize("Admin Operator")]
        [HttpPut]
        public async Task<IActionResult> UpdateCharacteristics(Guid id, [FromQuery] CharacteristicDTO characteristicDTO)
        {
            await _characteristicService.UpdateCharacteristic(id, characteristicDTO);
            return Ok();
        }
        [RoleAuthorize("Admin Operator")]
        [HttpDelete]
        public async Task<IActionResult> RemoveCharacteristics(Guid id)
        {
            await _characteristicService.RemoveCharacteristic(id);
            return Ok();
        }
    }
}
