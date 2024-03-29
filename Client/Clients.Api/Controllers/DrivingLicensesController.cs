﻿using Clients.Service.Interfaces;
using Infrastructure.Attributes;
using Infrastructure.DTO;
using Infrastructure.Filters;
using Infrastructure.HelperModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clients.Api.Controllers
{
    public class DrivingLicensesController : BaseApiController
    {
        private readonly IDrivingLicenseService _drivingLicenseService;

        public DrivingLicensesController(IDrivingLicenseService drivingLicenseService)
        {
            _drivingLicenseService = drivingLicenseService;
        }
        [RoleAuthorize("Admin Operator")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDrivingLicense(Guid id)
        {
            var drivingLicense = await _drivingLicenseService.GetDrivingLicense(id);
            return Ok(drivingLicense);
        }
        [RoleAuthorize("Client")]
        [HttpGet("my")]
        public async Task<IActionResult> GetDrivingLicense()
        {
            var drivingLicense = await _drivingLicenseService.GetDrivingLicense();
            return Ok(drivingLicense);
        }
        [RoleAuthorize("Admin Operator")]
        [HttpGet]
        public async Task<IActionResult> GetDrivingLicenses([FromQuery] DefoltFilter pageFilter)
        {
            var drivingLicense = await _drivingLicenseService.GetDrivingLicenses(pageFilter);
            return Ok(drivingLicense);
        }
        [RoleAuthorize("Admin Operator")]
        [HttpPost]
        public async Task<IActionResult> AddDrivingLicense([FromQuery] DrivingLicenseDTO drivingLicenseDTO)
        {
            var id = await _drivingLicenseService.AddDrivingLicense(drivingLicenseDTO);
            return Created(new Uri("/api/DrivingLicenses", UriKind.Relative), id);
        }
        [RoleAuthorize("Admin Operator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDrivingLicense(Guid id, [FromQuery] DrivingLicenseDTO drivingLicenseDTO)
        {
            await _drivingLicenseService.UpdateDrivingLicense(id, drivingLicenseDTO);
            return NoContent();
        }

        [RoleAuthorize("Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveDrivingLicense(Guid id)
        {
            await _drivingLicenseService.RemoveDrivingLicense(id);
            return NoContent();
        }
    }
}
