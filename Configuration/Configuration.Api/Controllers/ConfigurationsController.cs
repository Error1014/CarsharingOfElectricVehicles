using Configuration.Repository.Entities;
using Configuration.Repository.Interfaces;
using Configuration.Service;
using Infrastructure.Attributes;
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
        [HttpGet(nameof(GetConfigurationItems))]
        public async Task<IActionResult> GetConfigurationItems()
        {
            return Ok(await _configurationService.GetConfiguration());
        }
        [RoleAuthorize("Admin")]
        [HttpPost]
        public async Task<IActionResult> AddConfigurationItem(ConfigurationItem configurationItem)
        {
            await _configurationService.AddConfiguration(configurationItem);
            return Ok();
        }
        [RoleAuthorize("Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateConfigurationItem(ConfigurationItem configurationItem)
        {
            await _configurationService.UpdateConfiguration(configurationItem);
            return Ok();
        }
    }
}
