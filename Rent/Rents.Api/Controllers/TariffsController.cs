using Infrastructure.Attributes;
using Infrastructure.DTO;
using Infrastructure.HelperModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rents.Service.Interfaces;

namespace Rents.Api.Controllers
{
    public class TariffsController : BaseApiController
    {
        private readonly ITariffService _tariffService;

        public TariffsController(ITariffService tariffService)
        {
            _tariffService = tariffService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTariff(Guid id)
        {
            var result = await _tariffService.GetTariff(id);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetTariffs()
        {
            var result = await _tariffService.GetTariffs();
            return Ok(result);
        }
        [RoleAuthorize("Admin")]
        [HttpPost]
        public async Task<IActionResult> AddTariff([FromQuery] TariffDTO tarifDTO)
        {
            await _tariffService.AddTariff(tarifDTO);
            return Ok();
        }
        [RoleAuthorize("Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTariff(Guid id, [FromQuery] TariffDTO tarifDTO)
        {
            await _tariffService.UpdateTarif(id, tarifDTO);
            return Ok();
        }
        [RoleAuthorize("Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveTariff(Guid id)
        {
            await _tariffService.RemoveTariff(id);
            return Ok();
        }
    }
}
