using Configuration.Repository.Entities;
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
            await _configurationService.AddConfiguration(configurationItem);
            return Ok();
        }
        [RoleAuthorize("Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConfigurationItem(Guid id, ConfigurationItemDTO configurationItem)
        {
            await _configurationService.UpdateConfiguration(id, configurationItem);
            return Ok();
        }
    }
}
