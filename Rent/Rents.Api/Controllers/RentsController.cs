﻿using Infrastructure.Attributes;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRent(Guid id)
        {
            var rent = await _rentsService.GetRent(id);
            return Ok(rent);
        }
        [HttpGet(nameof(GetActualRent))]
        public async Task<IActionResult> GetActualRent()
        {
            var rent = await _rentsService.GetActualRent();
            return Ok(rent);
        }
        [HttpGet]
        public async Task<IActionResult> GetRents([FromQuery] HistoryRentFilter filter)
        {
            var rent = await _rentsService.GetRents(filter);
            return Ok(rent);
        }
        [RoleAuthorize("Client")]
        [HttpPost]
        public async Task<IActionResult> AddRent([FromQuery] AddRentDTO rentDTO)
        {
            var id = await _rentsService.AddRent(rentDTO);
            return Created(new Uri("/api/Rents", UriKind.Relative), id);
        }
        [RoleAuthorize("Client")]
        [HttpPut(nameof(CancelBooking))]
        public async Task<IActionResult> CancelBooking()
        {
            await _rentsService.CancelBookingCar();
            return NoContent();
        }
        [RoleAuthorize("Client")]
        [HttpPut(nameof(StartTrip))]
        public async Task<IActionResult> StartTrip()
        {
            await _rentsService.StartTrip();
            return NoContent();
        }
        [RoleAuthorize("Client")]
        [HttpPut(nameof(EndTrip))]
        public async Task<IActionResult> EndTrip(decimal kilometers)
        {
            await _rentsService.EndTrip(kilometers);
            return NoContent();
        }
    }
}
