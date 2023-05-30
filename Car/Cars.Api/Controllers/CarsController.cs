using AutoMapper;
using Cars.Service.Interfaces;
using Infrastructure.Attributes;
using Infrastructure.DTO;
using Infrastructure.Filters;
using Infrastructure.HelperModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cars.Api.Controllers
{
    public class CarsController : BaseApiController
    {
        private readonly ICarService _carService;
        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("{id}")]
        public async Task<CarDTO> GetCar(Guid id)
        {
            return await _carService.GetCar(id);
        }

        [HttpGet]
        public async Task<IActionResult> GetCars([FromQuery] PageFilter pageFilter)
        {
            var result = await _carService.GetCars(pageFilter);
            return Ok(result);
        }

        [RoleAuthorize("Admin")]
        [HttpPost]
        public async Task<IActionResult> AddCar([FromQuery] CarDTO carDTO)
        {
            await _carService.AddCar(carDTO);
            return Ok();
        }
        [RoleAuthorize("Admin")]
        [HttpPut(nameof(UpdateCar) + ("/{id}"))]
        public async Task<IActionResult> UpdateCar(Guid id, [FromQuery] CarDTO carDTO)
        {
            await _carService.UpdateCar(id, carDTO);
            return Ok();
        }
        [HttpPut(nameof(BookingCar) + ("/{id}"))]
        public async Task<IActionResult> BookingCar(Guid id)
        {
            await _carService.BookingCar(id);
            var car = await _carService.GetCar(id);
            return Ok(car.IsRent);
        }
        [HttpPut(nameof(CancelBookingCar) + ("/{id}"))]
        public async Task<IActionResult> CancelBookingCar(Guid id)
        {
            await _carService.CancelBookingCar(id);
            var car = await _carService.GetCar(id);
            return Ok(car.IsRent);
        }
        [RoleAuthorize("Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCar(Guid id)
        {
            await _carService.RemoveCar(id);
            return Ok();
        }
    }
}
