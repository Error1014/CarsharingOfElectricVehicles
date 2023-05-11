using Infrastructure.Attributes;
using Infrastructure.DTO.Rent;
using Infrastructure.Filters;
using Infrastructure.HelperModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rents.Service.Interfaces;

namespace Rents.Api.Controllers
{
    public class RentsController : BaseApiController
    {
        private readonly IRentService _rentsService;

        public RentsController(IRentService rentsService)
        {
            _rentsService = rentsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRent(Guid id)
        {
            var rent = await _rentsService.GetRent(id);
            return Ok(rent);
        }
        [HttpGet(nameof(GetRents))]
        public async Task<IEnumerable<RentDTO>> GetRents([FromQuery] PageFilter pageFilter)
        {
            var rent = await _rentsService.GetRents(pageFilter);
            return rent;
        }
        [RoleAuthorize("Client")]
        [HttpPost]
        public async Task<IActionResult> AddRent([FromQuery] AddRentDTO rentDTO)
        {
            await _rentsService.AddRent(rentDTO);
            return Ok();
        }
        [RoleAuthorize("Client")]
        [HttpPut(nameof(CancelBooking))]
        public async Task<IActionResult> CancelBooking()
        {
            await _rentsService.CancelBookingCar();
            return Ok();
        }
    }
}
