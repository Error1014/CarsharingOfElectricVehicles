﻿using Configuration.Repository.Entities;
using Configuration.Repository.Interfaces;
using Configuration.Service;
using Infrastructure.Attributes;
using Infrastructure.DTO;
using Infrastructure.HelperModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Configuration.Api.Controllers
{
    public class ConfigurationsController : BaseApiController
    {
        private readonly IConfigurationService _configurationService;

        public ConfigurationsController(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }
        [HttpGet]
        public async Task<IActionResult> GetConfigurationItems()
        {
            var result = await _configurationService.GetConfiguration();
            return Ok(result);
        }
        [RoleAuthorize("Admin")]
        [HttpPost]
        public async Task<IActionResult> AddConfigurationItem(ConfigurationItemDTO configurationItem)
        {
            var id = await _configurationService.AddConfiguration(configurationItem);
            return Created(new Uri("/api/Configurations", UriKind.Relative), id);
        }
        [RoleAuthorize("Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConfigurationItem(Guid id, ConfigurationItemDTO configurationItem)
        {
            await _configurationService.UpdateConfiguration(id, configurationItem);
            return NoContent();
        }
        [RoleAuthorize("Admin")]
        [HttpPut("{key}")]
        public async Task<IActionResult> UpdateConfigurationItem(string key, ConfigurationItemDTO configurationItem)
        {
            await _configurationService.UpdateConfiguration(key, configurationItem);
            return NoContent();
        }
        [RoleAuthorize("Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveConfigurationItem(Guid id)
        {
            await _configurationService.RemoveConfiguration(id);
            return NoContent();
        }
        [RoleAuthorize("Admin")]
        [HttpDelete("{key}")]
        public async Task<IActionResult> RemoveConfigurationItem(string key)
        {
            await _configurationService.RemoveConfiguration(key);
            return NoContent();
        }
    }
}
