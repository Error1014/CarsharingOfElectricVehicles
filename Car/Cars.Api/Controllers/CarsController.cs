using AutoMapper;
using Cars.Service.Interfaces;
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

        [HttpGet(nameof(GetCar))]
        public async Task<CarInfoDTO> GetCar(Guid Id)
        {
           return await _carService.GetCar(Id);
        }
        [HttpGet(nameof(GetCars))]
        public async Task<IEnumerable<CarInfoDTO>> GetCars([FromQuery] PageFilter pageFilter)
        {
            return await _carService.GetCars(pageFilter);
        }
        //ec681f11-a312-48a2-af9d-07ececf30b04
        [HttpPost(nameof(AddCar))]
        public async Task<IActionResult> AddCar([FromQuery] CarAddUpdateDTO carDTO)
        {
            await _carService.AddCar(carDTO);
            return Ok();
        }
        [HttpPut(nameof(UpdateCar))]
        public async Task<IActionResult> UpdateCar(Guid id, [FromQuery] CarAddUpdateDTO carDTO)
        {
            await _carService.UpdateCar(id, carDTO);
            return Ok();
        }
        [HttpDelete(nameof(UpdateCar))]
        public async Task<IActionResult> RemoveCar(Guid id)
        {
            await _carService.RemoveCar(id);
            return Ok();
        }
    }
}
