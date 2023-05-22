using Clients.Service.Interfaces;
using Infrastructure.Attributes;
using Infrastructure.DTO;
using Infrastructure.Filters;
using Infrastructure.HelperModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clients.Api.Controllers
{
    public class PassportsController : BaseApiController
    {
        private readonly IPassportService _passportService;

        public PassportsController(IPassportService passportService)
        {
            _passportService = passportService;
        }
        [RoleAuthorize("Admin Operator")]
        [HttpGet("/{id}")]
        public async Task<IActionResult> GetPassport(Guid id)
        {
            var passport = await _passportService.GetPassport(id);
            return Ok(passport);
        }
        [RoleAuthorize("Admin Operator")]
        [HttpGet]
        public async Task<IActionResult> GetPassports([FromQuery]PageFilter pageFilter)
        {
            var passport = await _passportService.GetPassports(pageFilter);
            return Ok(passport);
        }
        [RoleAuthorize("Admin Operator")]
        [HttpPost]
        public async Task<IActionResult> AddPassport([FromQuery] PassportDTO passportDTO)
        {
            await _passportService.AddPassport(passportDTO);
            return Ok();
        }
        [RoleAuthorize("Admin Operator")]
        [HttpPut("/{id}")]
        public async Task<IActionResult> UpdatePassport(Guid id, [FromQuery] PassportDTO passportDTO)
        {
            await _passportService.UpdatePassport(id, passportDTO);
            return Ok();
        }
        [RoleAuthorize("Admin")]
        [HttpDelete("/{id}")]
        public async Task<IActionResult> RemovePassport(Guid id)
        {
            await _passportService.RemovePassport(id);
            return Ok();
        }
    }
}
