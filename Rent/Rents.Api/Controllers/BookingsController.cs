using Infrastructure.Attributes;
using Infrastructure.DTO;
using Infrastructure.HelperModels;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rents.Service.Interfaces;

namespace Rents.Api.Controllers
{
    public class BookingsController : BaseApiController
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooking(Guid Id)
        {
            var result = await _bookingService.GetBooking(Id);
            return Ok(result);
        }

        [HttpGet(nameof(GetBookings))]
        public async Task<IEnumerable<BookingDTO>> GetBookings()
        {
            var result = await _bookingService.GetBookings();
            return result;
        }
        [RoleAuthorize("Client")]
        [HttpPost]
        public async Task<IActionResult> AddBooking([FromQuery]BookingDTO bookingDTO)
        {
            await _bookingService.AddBooking(bookingDTO);
            return Ok();
        }
        [RoleAuthorize("Client")]
        [HttpPut]
        public async Task<IActionResult> UpdateBooking(Guid Id, [FromQuery] BookingDTO bookingDTO)
        {
            await _bookingService.UpdateBooking(Id, bookingDTO);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveBooking(Guid Id)
        {
           await _bookingService.RemoveBooking(Id);
            return Ok();
        }
    }
}
