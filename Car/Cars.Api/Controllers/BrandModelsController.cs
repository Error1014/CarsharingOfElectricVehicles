using AutoMapper;
using Cars.Repository.Interfaces;
using Cars.Service.Interfaces;
using Infrastructure.Attributes;
using Infrastructure.DTO;
using Infrastructure.HelperModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cars.Api.Controllers
{
    public class BrandModelsController : BaseApiController
    {
        private readonly IBrandModelService _brandModelService;
        private readonly IMapper _map;

        public BrandModelsController(IBrandModelService brandModelService, IMapper mapper)
        {
            _brandModelService = brandModelService;
            _map = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrandModel(Guid id)
        {
            var result = await _brandModelService.GetBrandModel(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetBrandModels()
        {
            var result = await _brandModelService.GetBrandModels();
            return Ok(result);
        }
        [RoleAuthorize("Admin Operator")]
        [HttpPost]
        public async Task<IActionResult> AddBrandModel([FromQuery]BrandModelDTO brandModel)
        {
            await _brandModelService.AddBrandModel(brandModel);
            return Ok();
        }

        [RoleAuthorize("Admin Operator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrandModel(Guid id, BrandModelDTO brandModel)
        {
            await _brandModelService.UpdateBrandModel(id, brandModel);
            return Ok();
        }

        [RoleAuthorize("Admin Operator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveBrandModel(Guid id)
        {
            await _brandModelService.RemodeBrandModel(id);
            return Ok();
        }
    }
}
